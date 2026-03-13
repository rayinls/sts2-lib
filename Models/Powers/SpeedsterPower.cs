using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006A4 RID: 1700
	public sealed class SpeedsterPower : PowerModel
	{
		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x060055A5 RID: 21925 RVA: 0x00226D7E File Offset: 0x00224F7E
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x060055A6 RID: 21926 RVA: 0x00226D81 File Offset: 0x00224F81
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x00226D84 File Offset: 0x00224F84
		[NullableContext(1)]
		public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (!fromHandDraw)
			{
				if (card.Owner.Creature == base.Owner)
				{
					if (card.Owner.Creature.CombatState.CurrentSide == card.Owner.Creature.Side)
					{
						VfxCmd.PlayOnCreatureCenters(base.CombatState.HittableEnemies, "vfx/vfx_attack_slash");
						SfxCmd.Play("slash_attack.mp3", 1f);
						await CreatureCmd.Damage(choiceContext, base.CombatState.HittableEnemies, base.Amount, ValueProp.Unpowered, base.Owner, null);
					}
				}
			}
		}
	}
}
