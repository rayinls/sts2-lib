using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200092C RID: 2348
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Dualcast : CardModel
	{
		// Token: 0x06006A05 RID: 27141 RVA: 0x0025A4EA File Offset: 0x002586EA
		public Dualcast()
			: base(1, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
		{
		}

		// Token: 0x17001BFC RID: 7164
		// (get) Token: 0x06006A06 RID: 27142 RVA: 0x0025A4F7 File Offset: 0x002586F7
		public override OrbEvokeType OrbEvokeType
		{
			get
			{
				return OrbEvokeType.Front;
			}
		}

		// Token: 0x17001BFD RID: 7165
		// (get) Token: 0x06006A07 RID: 27143 RVA: 0x0025A4FA File Offset: 0x002586FA
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Evoke, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06006A08 RID: 27144 RVA: 0x0025A50C File Offset: 0x0025870C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (base.Owner.PlayerCombatState.OrbQueue.Orbs.Count > 0)
			{
				await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
				await OrbCmd.EvokeNext(choiceContext, base.Owner, false);
				await Cmd.CustomScaledWait(0.1f, 0.25f, false, default(CancellationToken));
				await OrbCmd.EvokeNext(choiceContext, base.Owner, true);
			}
		}

		// Token: 0x06006A09 RID: 27145 RVA: 0x0025A557 File Offset: 0x00258757
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
