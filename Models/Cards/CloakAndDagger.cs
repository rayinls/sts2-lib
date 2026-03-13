using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008E3 RID: 2275
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CloakAndDagger : CardModel
	{
		// Token: 0x06006885 RID: 26757 RVA: 0x002578AB File Offset: 0x00255AAB
		public CloakAndDagger()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001B4E RID: 6990
		// (get) Token: 0x06006886 RID: 26758 RVA: 0x002578B8 File Offset: 0x00255AB8
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B4F RID: 6991
		// (get) Token: 0x06006887 RID: 26759 RVA: 0x002578BB File Offset: 0x00255ABB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(6m, ValueProp.Move),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x17001B50 RID: 6992
		// (get) Token: 0x06006888 RID: 26760 RVA: 0x002578E0 File Offset: 0x00255AE0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(false));
			}
		}

		// Token: 0x06006889 RID: 26761 RVA: 0x002578F0 File Offset: 0x00255AF0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++)
			{
				await Shiv.CreateInHand(base.Owner, base.CombatState);
				await Cmd.Wait(0.25f, false);
			}
		}

		// Token: 0x0600688A RID: 26762 RVA: 0x0025793B File Offset: 0x00255B3B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
