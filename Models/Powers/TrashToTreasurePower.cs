using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006C9 RID: 1737
	public sealed class TrashToTreasurePower : PowerModel
	{
		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06005696 RID: 22166 RVA: 0x00228AE9 File Offset: 0x00226CE9
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06005697 RID: 22167 RVA: 0x00228AEC File Offset: 0x00226CEC
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x00228AF0 File Offset: 0x00226CF0
		[NullableContext(1)]
		public override async Task AfterCardGeneratedForCombat(CardModel card, bool addedByPlayer)
		{
			if (addedByPlayer)
			{
				if (card.Type == CardType.Status)
				{
					base.Flash();
					for (int i = 0; i < base.Amount; i++)
					{
						OrbModel orbModel = OrbModel.GetRandomOrb(base.Owner.Player.RunState.Rng.CombatOrbGeneration).ToMutable(0);
						await OrbCmd.Channel(new ThrowingPlayerChoiceContext(), orbModel, base.Owner.Player);
					}
				}
			}
		}
	}
}
