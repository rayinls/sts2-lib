using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Afflictions.Mocks
{
	// Token: 0x02000AE0 RID: 2784
	public sealed class MockSelfDamageAffliction : AfflictionModel
	{
		// Token: 0x17001FFB RID: 8187
		// (get) Token: 0x0600736E RID: 29550 RVA: 0x0026D9CF File Offset: 0x0026BBCF
		public override bool IsStackable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600736F RID: 29551 RVA: 0x0026D9D4 File Offset: 0x0026BBD4
		[NullableContext(1)]
		public override async Task OnPlay(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			await CreatureCmd.Damage(choiceContext, base.Card.Owner.Creature, base.Amount, ValueProp.Unpowered | ValueProp.Move, null, null);
		}
	}
}
