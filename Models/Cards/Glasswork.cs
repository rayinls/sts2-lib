using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200096D RID: 2413
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Glasswork : CardModel
	{
		// Token: 0x06006B72 RID: 27506 RVA: 0x0025D1FF File Offset: 0x0025B3FF
		public Glasswork()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x06006B73 RID: 27507 RVA: 0x0025D20C File Offset: 0x0025B40C
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C9A RID: 7322
		// (get) Token: 0x06006B74 RID: 27508 RVA: 0x0025D20F File Offset: 0x0025B40F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<GlassOrb>()
				});
			}
		}

		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x06006B75 RID: 27509 RVA: 0x0025D232 File Offset: 0x0025B432
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(5m, ValueProp.Move));
			}
		}

		// Token: 0x06006B76 RID: 27510 RVA: 0x0025D248 File Offset: 0x0025B448
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await OrbCmd.Channel<GlassOrb>(choiceContext, base.Owner);
		}

		// Token: 0x06006B77 RID: 27511 RVA: 0x0025D29B File Offset: 0x0025B49B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
