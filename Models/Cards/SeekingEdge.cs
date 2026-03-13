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
	// Token: 0x02000A3D RID: 2621
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SeekingEdge : CardModel
	{
		// Token: 0x06006FC0 RID: 28608 RVA: 0x00265D83 File Offset: 0x00263F83
		public SeekingEdge()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E5F RID: 7775
		// (get) Token: 0x06006FC1 RID: 28609 RVA: 0x00265D90 File Offset: 0x00263F90
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x17001E60 RID: 7776
		// (get) Token: 0x06006FC2 RID: 28610 RVA: 0x00265D97 File Offset: 0x00263F97
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new ForgeVar(7));
			}
		}

		// Token: 0x06006FC3 RID: 28611 RVA: 0x00265DA4 File Offset: 0x00263FA4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await ForgeCmd.Forge(base.DynamicVars.Forge.IntValue, base.Owner, this);
			await PowerCmd.Apply<SeekingEdgePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006FC4 RID: 28612 RVA: 0x00265DE7 File Offset: 0x00263FE7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Forge.UpgradeValueBy(4m);
		}
	}
}
