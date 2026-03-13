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
	// Token: 0x0200096C RID: 2412
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Glacier : CardModel
	{
		// Token: 0x06006B6C RID: 27500 RVA: 0x0025D14B File Offset: 0x0025B34B
		public Glacier()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x06006B6D RID: 27501 RVA: 0x0025D158 File Offset: 0x0025B358
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x06006B6E RID: 27502 RVA: 0x0025D15B File Offset: 0x0025B35B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(6m, ValueProp.Move));
			}
		}

		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x06006B6F RID: 27503 RVA: 0x0025D16E File Offset: 0x0025B36E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<FrostOrb>()
				});
			}
		}

		// Token: 0x06006B70 RID: 27504 RVA: 0x0025D194 File Offset: 0x0025B394
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			for (int i = 0; i < 2; i++)
			{
				await OrbCmd.Channel<FrostOrb>(choiceContext, base.Owner);
			}
		}

		// Token: 0x06006B71 RID: 27505 RVA: 0x0025D1E7 File Offset: 0x0025B3E7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
