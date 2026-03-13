using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Modifiers
{
	// Token: 0x020007B0 RID: 1968
	[NullableContext(1)]
	[Nullable(0)]
	public class DeadlyEvents : ModifierModel
	{
		// Token: 0x060060CF RID: 24783 RVA: 0x002438E0 File Offset: 0x00241AE0
		protected override void AfterRunCreated(RunState runState)
		{
			foreach (Player player in runState.Players)
			{
				player.RelicGrabBag.Remove<JuzuBracelet>();
			}
			runState.SharedRelicGrabBag.Remove<JuzuBracelet>();
			runState.Odds.UnknownMapPoint.EliteOdds = 0.1f;
			runState.Odds.UnknownMapPoint.SetBaseOdds(RoomType.Elite, 0.1f);
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x00243968 File Offset: 0x00241B68
		protected override void AfterRunLoaded(RunState runState)
		{
			runState.Odds.UnknownMapPoint.SetBaseOdds(RoomType.Elite, 0.1f);
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x00243980 File Offset: 0x00241B80
		public override float ModifyOddsIncreaseForUnrolledRoomType(RoomType roomType, float oddsIncrease)
		{
			if (roomType != RoomType.Treasure)
			{
				return oddsIncrease;
			}
			return oddsIncrease * 2f;
		}
	}
}
