using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008A0 RID: 2208
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BansheesCry : CardModel
	{
		// Token: 0x06006732 RID: 26418 RVA: 0x00254D57 File Offset: 0x00252F57
		public BansheesCry()
			: base(6, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001AC8 RID: 6856
		// (get) Token: 0x06006733 RID: 26419 RVA: 0x00254D64 File Offset: 0x00252F64
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Ethereal));
			}
		}

		// Token: 0x17001AC9 RID: 6857
		// (get) Token: 0x06006734 RID: 26420 RVA: 0x00254D71 File Offset: 0x00252F71
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(33m, ValueProp.Move),
					new EnergyVar(2)
				});
			}
		}

		// Token: 0x06006735 RID: 26421 RVA: 0x00254D98 File Offset: 0x00252F98
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006736 RID: 26422 RVA: 0x00254DE3 File Offset: 0x00252FE3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(6m);
		}

		// Token: 0x06006737 RID: 26423 RVA: 0x00254DFC File Offset: 0x00252FFC
		public override Task AfterCardEnteredCombat(CardModel card)
		{
			if (card != this)
			{
				return Task.CompletedTask;
			}
			if (base.IsClone)
			{
				return Task.CompletedTask;
			}
			int num = CombatManager.Instance.History.CardPlaysFinished.Count((CardPlayFinishedEntry e) => e.WasEthereal && e.CardPlay.Card.Owner == base.Owner);
			base.EnergyCost.AddThisCombat(-num * base.DynamicVars.Energy.IntValue, false);
			return Task.CompletedTask;
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x00254E68 File Offset: 0x00253068
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (!cardPlay.Card.Keywords.Contains(CardKeyword.Ethereal))
			{
				return Task.CompletedTask;
			}
			base.EnergyCost.AddThisCombat(-base.DynamicVars.Energy.IntValue, false);
			return Task.CompletedTask;
		}
	}
}
