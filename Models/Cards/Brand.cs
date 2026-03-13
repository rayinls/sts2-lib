using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008BF RID: 2239
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Brand : CardModel
	{
		// Token: 0x060067D7 RID: 26583 RVA: 0x0025630E File Offset: 0x0025450E
		public Brand()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B0B RID: 6923
		// (get) Token: 0x060067D8 RID: 26584 RVA: 0x0025631B File Offset: 0x0025451B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
					HoverTipFactory.FromPower<StrengthPower>()
				});
			}
		}

		// Token: 0x17001B0C RID: 6924
		// (get) Token: 0x060067D9 RID: 26585 RVA: 0x00256339 File Offset: 0x00254539
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HpLossVar(1m),
					new PowerVar<StrengthPower>(1m)
				});
			}
		}

		// Token: 0x060067DA RID: 26586 RVA: 0x00256360 File Offset: 0x00254560
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			VfxCmd.PlayOnCreatureCenter(base.Owner.Creature, "vfx/vfx_bloody_impact");
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, null, this);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
			}
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NGroundFireVfx.Create(base.Owner.Creature, VfxColor.Red));
			}
			SfxCmd.Play("event:/sfx/characters/attack_fire", 1f);
			await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060067DB RID: 26587 RVA: 0x002563AB File Offset: 0x002545AB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Strength.UpgradeValueBy(1m);
		}
	}
}
