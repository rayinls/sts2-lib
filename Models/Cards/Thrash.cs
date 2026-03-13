using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A98 RID: 2712
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Thrash : CardModel
	{
		// Token: 0x060071B4 RID: 29108 RVA: 0x00269B3C File Offset: 0x00267D3C
		public Thrash()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F2A RID: 7978
		// (get) Token: 0x060071B5 RID: 29109 RVA: 0x00269B49 File Offset: 0x00267D49
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x17001F2B RID: 7979
		// (get) Token: 0x060071B6 RID: 29110 RVA: 0x00269B56 File Offset: 0x00267D56
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(4m, ValueProp.Move));
			}
		}

		// Token: 0x17001F2C RID: 7980
		// (get) Token: 0x060071B7 RID: 29111 RVA: 0x00269B69 File Offset: 0x00267D69
		// (set) Token: 0x060071B8 RID: 29112 RVA: 0x00269B71 File Offset: 0x00267D71
		private decimal ExtraDamage
		{
			get
			{
				return this._extraDamage;
			}
			set
			{
				base.AssertMutable();
				this._extraDamage = value;
			}
		}

		// Token: 0x060071B9 RID: 29113 RVA: 0x00269B80 File Offset: 0x00267D80
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(2).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_thrash", null, null)
				.Execute(choiceContext);
			CardPile pile = PileType.Hand.GetPile(base.Owner);
			CardModel cardModel = base.Owner.RunState.Rng.CombatCardSelection.NextItem<CardModel>(pile.Cards.Where((CardModel c) => c.Type == CardType.Attack));
			if (cardModel != null)
			{
				decimal num = 0m;
				if (cardModel.DynamicVars.ContainsKey("CalculatedDamage"))
				{
					num = cardModel.DynamicVars.CalculatedDamage.Calculate(null);
				}
				else if (cardModel.DynamicVars.ContainsKey("Damage"))
				{
					num = cardModel.DynamicVars.Damage.BaseValue;
				}
				else if (cardModel.DynamicVars.ContainsKey("OstyDamage"))
				{
					num = cardModel.DynamicVars.OstyDamage.BaseValue;
				}
				else
				{
					Log.Warn(base.Id.Entry + " exhausted attack card " + cardModel.Id.Entry + " that did not have an appropriate damage var!", 2);
				}
				IEnumerable<AbstractModel> enumerable;
				num = Hook.ModifyDamage(base.Owner.RunState, base.Owner.Creature.CombatState, null, base.Owner.Creature, num, ValueProp.Move, cardModel, ModifyDamageHookType.All, CardPreviewMode.None, out enumerable);
				base.DynamicVars.Damage.BaseValue += num;
				this.ExtraDamage += num;
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
			}
		}

		// Token: 0x060071BA RID: 29114 RVA: 0x00269BD3 File Offset: 0x00267DD3
		protected override void AfterDowngraded()
		{
			base.AfterDowngraded();
			base.DynamicVars.Damage.BaseValue += this.ExtraDamage;
		}

		// Token: 0x060071BB RID: 29115 RVA: 0x00269BFC File Offset: 0x00267DFC
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}

		// Token: 0x040025E0 RID: 9696
		private decimal _extraDamage;
	}
}
