using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Random;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005C2 RID: 1474
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WhisperingEarring : RelicModel
	{
		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x0600509C RID: 20636 RVA: 0x0021D560 File Offset: 0x0021B760
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x0600509D RID: 20637 RVA: 0x0021D563 File Offset: 0x0021B763
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x0600509E RID: 20638 RVA: 0x0021D570 File Offset: 0x0021B770
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x0600509F RID: 20639 RVA: 0x0021D57D File Offset: 0x0021B77D
		public override decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			if (player != base.Owner)
			{
				return amount;
			}
			return amount + base.DynamicVars.Energy.BaseValue;
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x0021D5A0 File Offset: 0x0021B7A0
		public override async Task BeforePlayPhaseStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				CombatState combatState = player.Creature.CombatState;
				if (combatState.RoundNumber <= 1)
				{
					base.Flash();
					bool flag;
					using (CardSelectCmd.PushSelector(new VakuuCardSelector()))
					{
						int cardsPlayed = 0;
						while (cardsPlayed < 13 && !CombatManager.Instance.IsOverOrEnding)
						{
							CardPile pile = PileType.Hand.GetPile(base.Owner);
							CardModel card = pile.Cards.FirstOrDefault((CardModel c) => c.CanPlay());
							if (card == null)
							{
								break;
							}
							Creature target = this.GetTarget(card, combatState);
							await card.SpendResources();
							await CardCmd.AutoPlay(choiceContext, card, target, AutoPlayType.Default, true, false);
							cardsPlayed++;
							card = null;
							target = null;
						}
						flag = cardsPlayed >= 13;
						if (cardsPlayed == 0)
						{
							return;
						}
					}
					IDisposable disposable = null;
					LocString locString = (flag ? new LocString("relics", "WHISPERING_EARRING.warning") : new LocString("relics", "WHISPERING_EARRING.approval"));
					TalkCmd.Play(locString, base.Owner.Creature, -1.0, VfxColor.White);
				}
			}
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x0021D5F4 File Offset: 0x0021B7F4
		[return: Nullable(2)]
		private Creature GetTarget(CardModel card, CombatState combatState)
		{
			Rng combatTargets = base.Owner.RunState.Rng.CombatTargets;
			switch (card.TargetType)
			{
			case TargetType.AnyEnemy:
				return combatState.HittableEnemies.FirstOrDefault<Creature>();
			case TargetType.AnyPlayer:
				return base.Owner.Creature;
			case TargetType.AnyAlly:
				return combatTargets.NextItem<Creature>(combatState.Allies.Where((Creature c) => c != null && c.IsAlive && c.IsPlayer && c != base.Owner.Creature));
			}
			return null;
		}

		// Token: 0x04002240 RID: 8768
		private const int _maxCardsToPlay = 13;
	}
}
