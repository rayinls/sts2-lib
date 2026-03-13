using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000662 RID: 1634
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NoDrawPower : PowerModel
	{
		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x0600540C RID: 21516 RVA: 0x00223D80 File Offset: 0x00221F80
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x0600540D RID: 21517 RVA: 0x00223D83 File Offset: 0x00221F83
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x00223D86 File Offset: 0x00221F86
		public override bool ShouldDraw(Player player, bool fromHandDraw)
		{
			if (fromHandDraw)
			{
				return true;
			}
			if (player != base.Owner.Player)
			{
				return true;
			}
			base.Flash();
			return false;
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x00223DA4 File Offset: 0x00221FA4
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			await PowerCmd.Remove(this);
		}
	}
}
