using System;
using MegaCrit.Sts2.Core.Nodes.Animation;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000746 RID: 1862
	public sealed class DecimillipedeSegmentBack : DecimillipedeSegment
	{
		// Token: 0x06005A76 RID: 23158 RVA: 0x002304A4 File Offset: 0x0022E6A4
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
