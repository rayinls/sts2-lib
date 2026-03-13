using System;
using MegaCrit.Sts2.Core.Nodes.Animation;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000747 RID: 1863
	public sealed class DecimillipedeSegmentFront : DecimillipedeSegment
	{
		// Token: 0x06005A78 RID: 23160 RVA: 0x002304EC File Offset: 0x0022E6EC
		protected override void SegmentAttack()
		{
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
			if (ncreature != null)
			{
				NDecimillipedeSegmentDriver specialNode = ncreature.GetSpecialNode<NDecimillipedeSegmentDriver>("%Visuals/SegmentDriver");
				if (specialNode == null)
				{
					return;
				}
				specialNode.AttackShake();
			}
		}
	}
}
