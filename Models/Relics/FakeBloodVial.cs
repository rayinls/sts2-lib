using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004F4 RID: 1268
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FakeBloodVial : RelicModel
	{
		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06004B49 RID: 19273 RVA: 0x00213877 File Offset: 0x00211A77
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06004B4A RID: 19274 RVA: 0x0021387A File Offset: 0x00211A7A
		public override int MerchantCost
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06004B4B RID: 19275 RVA: 0x0021387E File Offset: 0x00211A7E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HealVar(1m));
			}
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x00213890 File Offset: 0x00211A90
		public override async Task AfterPlayerTurnStartLate(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				if (player.Creature.CombatState.RoundNumber <= 1)
				{
					await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.IntValue, true);
				}
			}
		}
	}
}
