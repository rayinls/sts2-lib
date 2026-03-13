using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A70 RID: 2672
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Stomp : CardModel
	{
		// Token: 0x060070E6 RID: 28902 RVA: 0x00268191 File Offset: 0x00266391
		public Stomp()
			: base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001EDC RID: 7900
		// (get) Token: 0x060070E7 RID: 28903 RVA: 0x0026819E File Offset: 0x0026639E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(12m, ValueProp.Move));
			}
		}

		// Token: 0x060070E8 RID: 28904 RVA: 0x002681B4 File Offset: 0x002663B4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);
			foreach (Creature creature in base.CombatState.HittableEnemies)
			{
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance != null)
				{
					instance.CombatVfxContainer.AddChildSafely(NSpikeSplashVfx.Create(creature, VfxColor.Red));
				}
			}
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x060070E9 RID: 28905 RVA: 0x002681FF File Offset: 0x002663FF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}

		// Token: 0x060070EA RID: 28906 RVA: 0x00268218 File Offset: 0x00266418
		public override Task AfterCardEnteredCombat(CardModel card)
		{
			if (card != this)
			{
				return Task.CompletedTask;
			}
			if (base.IsClone)
			{
				return Task.CompletedTask;
			}
			int num = CombatManager.Instance.History.CardPlaysFinished.Count((CardPlayFinishedEntry e) => e.CardPlay.Card.Type == CardType.Attack && e.CardPlay.Card.Owner == base.Owner && e.HappenedThisTurn(base.CombatState));
			this.ReduceCostBy(num);
			return Task.CompletedTask;
		}

		// Token: 0x060070EB RID: 28907 RVA: 0x0026826A File Offset: 0x0026646A
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Type != CardType.Attack)
			{
				return Task.CompletedTask;
			}
			this.ReduceCostBy(1);
			return Task.CompletedTask;
		}

		// Token: 0x060070EC RID: 28908 RVA: 0x002682A5 File Offset: 0x002664A5
		private void ReduceCostBy(int amount)
		{
			base.EnergyCost.AddThisTurn(-amount, false);
		}
	}
}
