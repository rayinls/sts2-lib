using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009D7 RID: 2519
	public sealed class MultiCast : CardModel
	{
		// Token: 0x06006DAF RID: 28079 RVA: 0x00261AEC File Offset: 0x0025FCEC
		public MultiCast()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001D87 RID: 7559
		// (get) Token: 0x06006DB0 RID: 28080 RVA: 0x00261AF9 File Offset: 0x0025FCF9
		protected override bool HasEnergyCostX
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D88 RID: 7560
		// (get) Token: 0x06006DB1 RID: 28081 RVA: 0x00261AFC File Offset: 0x0025FCFC
		public override OrbEvokeType OrbEvokeType
		{
			get
			{
				return OrbEvokeType.All;
			}
		}

		// Token: 0x06006DB2 RID: 28082 RVA: 0x00261B00 File Offset: 0x0025FD00
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			int evokeCount = base.ResolveEnergyXValue();
			if (base.IsUpgraded)
			{
				evokeCount++;
			}
			for (int i = 0; i < evokeCount; i++)
			{
				await OrbCmd.EvokeNext(choiceContext, base.Owner, i == evokeCount - 1);
				await Cmd.Wait(0.25f, false);
			}
		}
	}
}
