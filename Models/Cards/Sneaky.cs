using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A59 RID: 2649
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Sneaky : CardModel
	{
		// Token: 0x06007058 RID: 28760 RVA: 0x00266F23 File Offset: 0x00265123
		public Sneaky()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001EA3 RID: 7843
		// (get) Token: 0x06007059 RID: 28761 RVA: 0x00266F30 File Offset: 0x00265130
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Sly);
			}
		}

		// Token: 0x17001EA4 RID: 7844
		// (get) Token: 0x0600705A RID: 28762 RVA: 0x00266F38 File Offset: 0x00265138
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001EA5 RID: 7845
		// (get) Token: 0x0600705B RID: 28763 RVA: 0x00266F4A File Offset: 0x0026514A
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001EA6 RID: 7846
		// (get) Token: 0x0600705C RID: 28764 RVA: 0x00266F4D File Offset: 0x0026514D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<SneakyPower>(1m));
			}
		}

		// Token: 0x0600705D RID: 28765 RVA: 0x00266F60 File Offset: 0x00265160
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<SneakyPower>(base.Owner.Creature, base.DynamicVars["SneakyPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600705E RID: 28766 RVA: 0x00266FA3 File Offset: 0x002651A3
		protected override void OnUpgrade()
		{
			base.DynamicVars["SneakyPower"].UpgradeValueBy(1m);
		}
	}
}
