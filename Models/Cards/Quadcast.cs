using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A12 RID: 2578
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Quadcast : CardModel
	{
		// Token: 0x06006EDB RID: 28379 RVA: 0x002641A2 File Offset: 0x002623A2
		public Quadcast()
			: base(1, CardType.Skill, CardRarity.Ancient, TargetType.Self, true)
		{
		}

		// Token: 0x17001E00 RID: 7680
		// (get) Token: 0x06006EDC RID: 28380 RVA: 0x002641AF File Offset: 0x002623AF
		public override OrbEvokeType OrbEvokeType
		{
			get
			{
				return OrbEvokeType.Front;
			}
		}

		// Token: 0x17001E01 RID: 7681
		// (get) Token: 0x06006EDD RID: 28381 RVA: 0x002641B2 File Offset: 0x002623B2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new RepeatVar(4));
			}
		}

		// Token: 0x06006EDE RID: 28382 RVA: 0x002641C0 File Offset: 0x002623C0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (base.Owner.PlayerCombatState.OrbQueue.Orbs.Count > 0)
			{
				await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
				for (int i = 0; i < base.DynamicVars.Repeat.IntValue; i++)
				{
					await OrbCmd.EvokeNext(choiceContext, base.Owner, i == base.DynamicVars.Repeat.IntValue - 1);
					if (i != base.DynamicVars.Repeat.IntValue - 1)
					{
						await Cmd.CustomScaledWait(0.15f, 0.25f, false, default(CancellationToken));
					}
				}
			}
		}

		// Token: 0x06006EDF RID: 28383 RVA: 0x0026420B File Offset: 0x0026240B
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
