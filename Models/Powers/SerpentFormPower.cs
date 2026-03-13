using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200068E RID: 1678
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SerpentFormPower : PowerModel
	{
		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06005525 RID: 21797 RVA: 0x00225FCF File Offset: 0x002241CF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06005526 RID: 21798 RVA: 0x00225FD2 File Offset: 0x002241D2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x00225FD5 File Offset: 0x002241D5
		protected override object InitInternalData()
		{
			return new SerpentFormPower.Data();
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x00225FDC File Offset: 0x002241DC
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner.Player)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<SerpentFormPower.Data>().amountsForPlayedCards.Add(cardPlay.Card, base.Amount);
			return Task.CompletedTask;
		}

		// Token: 0x06005529 RID: 21801 RVA: 0x00226028 File Offset: 0x00224228
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				int damage;
				if (base.GetInternalData<SerpentFormPower.Data>().amountsForPlayedCards.Remove(cardPlay.Card, out damage))
				{
					if (damage > 0)
					{
						await Cmd.CustomScaledWait(0.1f, 0.2f, false, default(CancellationToken));
						Creature creature = base.Owner.Player.RunState.Rng.CombatTargets.NextItem<Creature>(base.Owner.CombatState.HittableEnemies);
						if (creature != null)
						{
							VfxCmd.PlayOnCreatureCenter(creature, "vfx/vfx_attack_blunt");
							await CreatureCmd.Damage(context, creature, damage, ValueProp.Unpowered, base.Owner);
						}
					}
				}
			}
		}

		// Token: 0x02001A81 RID: 6785
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x0400684A RID: 26698
			[Nullable(1)]
			public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
		}
	}
}
