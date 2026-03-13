using System;
using MegaCrit.Sts2.Core.Nodes.Animation;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000748 RID: 1864
	public sealed class DecimillipedeSegmentMiddle : DecimillipedeSegment
	{
		// Token: 0x06005A7A RID: 23162 RVA: 0x00230534 File Offset: 0x0022E734
		protected override void SegmentAttack()
		{
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
			if (ncreature != null)
			{
				NDecimillipedeSegmentDriver specialNode = ncreature.GetSpecialNode<NDecimillipedeSegmentDriver>("%Visuals/RightSegmentDriver");
				if (specialNode != null)
				{
					specialNode.AttackShake();
				}
			}
			if (ncreature != null)
			{
				NDecimillipedeSegmentDriver specialNode2 = ncreature.GetSpecialNode<NDecimillipedeSegmentDriver>("%Visuals/LeftSegmentDriver");
				if (specialNode2 == null)
				{
					return;
				}
				specialNode2.AttackShake();
			}
		}
	}
}
