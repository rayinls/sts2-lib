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
	// Token: 0x020009D5 RID: 2517
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MonarchsGaze : CardModel
	{
		// Token: 0x06006DA5 RID: 28069 RVA: 0x002619EF File Offset: 0x0025FBEF
		public MonarchsGaze()
			: base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001D83 RID: 7555
		// (get) Token: 0x06006DA6 RID: 28070 RVA: 0x002619FC File Offset: 0x0025FBFC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("StrengthLoss", 1m));
			}
		}

		// Token: 0x17001D84 RID: 7556
		// (get) Token: 0x06006DA7 RID: 28071 RVA: 0x00261A12 File Offset: 0x0025FC12
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06006DA8 RID: 28072 RVA: 0x00261A20 File Offset: 0x0025FC20
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<MonarchsGazePower>(base.Owner.Creature, base.DynamicVars["StrengthLoss"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006DA9 RID: 28073 RVA: 0x00261A63 File Offset: 0x0025FC63
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}

		// Token: 0x040025B3 RID: 9651
		private const string _strengthLossKey = "StrengthLoss";
	}
}
