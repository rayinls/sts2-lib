using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A4A RID: 2634
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Shiv : CardModel
	{
		// Token: 0x06007005 RID: 28677 RVA: 0x0026652F File Offset: 0x0026472F
		public Shiv()
			: base(0, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E7E RID: 7806
		// (get) Token: 0x06007006 RID: 28678 RVA: 0x0026653C File Offset: 0x0026473C
		public override TargetType TargetType
		{
			get
			{
				if (!this.HasFanOfKnives)
				{
					return TargetType.AnyEnemy;
				}
				return TargetType.AllEnemies;
			}
		}

		// Token: 0x17001E7F RID: 7807
		// (get) Token: 0x06007007 RID: 28679 RVA: 0x00266549 File Offset: 0x00264749
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Shiv };
			}
		}

		// Token: 0x17001E80 RID: 7808
		// (get) Token: 0x06007008 RID: 28680 RVA: 0x00266558 File Offset: 0x00264758
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[4];
				array[0] = new DamageVar(4m, ValueProp.Move);
				array[1] = new CalculationBaseVar(0m);
				array[2] = new CalculationExtraVar(1m);
				array[3] = new CalculatedVar("FanOfKnivesAmount").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => (card != null && card.IsMutable && card.Owner != null) ? card.Owner.Creature.GetPowerAmount<FanOfKnivesPower>() : 0);
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x17001E81 RID: 7809
		// (get) Token: 0x06007009 RID: 28681 RVA: 0x002665CA File Offset: 0x002647CA
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x0600700A RID: 28682 RVA: 0x002665D4 File Offset: 0x002647D4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			AttackCommand attackCommand = DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this);
			if (this.HasFanOfKnives)
			{
				Creature lastEnemy = base.CombatState.HittableEnemies.LastOrDefault<Creature>();
				attackCommand = attackCommand.TargetingAllOpponents(base.CombatState).WithHitVfxNode((Creature _) => NShivThrowVfx.Create(this.Owner.Creature, lastEnemy, Colors.Green));
			}
			else
			{
				ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
				attackCommand = attackCommand.Targeting(cardPlay.Target).WithHitVfxNode((Creature t) => NShivThrowVfx.Create(base.Owner.Creature, t, Colors.Green));
			}
			if (base.Owner.Character is Silent)
			{
				attackCommand.WithAttackerAnim("Shiv", 0.2f, null);
			}
			await attackCommand.Execute(choiceContext);
		}

		// Token: 0x0600700B RID: 28683 RVA: 0x00266627 File Offset: 0x00264827
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}

		// Token: 0x0600700C RID: 28684 RVA: 0x00266640 File Offset: 0x00264840
		[return: Nullable(new byte[] { 1, 2 })]
		public static async Task<CardModel> CreateInHand(Player owner, CombatState combatState)
		{
			IEnumerable<CardModel> enumerable = await Shiv.CreateInHand(owner, 1, combatState);
			return enumerable.FirstOrDefault<CardModel>();
		}

		// Token: 0x0600700D RID: 28685 RVA: 0x0026668C File Offset: 0x0026488C
		public static async Task<IEnumerable<CardModel>> CreateInHand(Player owner, int count, CombatState combatState)
		{
			IEnumerable<CardModel> enumerable;
			if (count == 0)
			{
				enumerable = Array.Empty<CardModel>();
			}
			else if (CombatManager.Instance.IsOverOrEnding)
			{
				enumerable = Array.Empty<CardModel>();
			}
			else
			{
				List<CardModel> shivs = new List<CardModel>();
				for (int i = 0; i < count; i++)
				{
					shivs.Add(combatState.CreateCard<Shiv>(owner));
				}
				await CardPileCmd.AddGeneratedCardsToCombat(shivs, PileType.Hand, true, CardPilePosition.Bottom);
				enumerable = shivs;
			}
			return enumerable;
		}

		// Token: 0x17001E82 RID: 7810
		// (get) Token: 0x0600700E RID: 28686 RVA: 0x002666DF File Offset: 0x002648DF
		private bool HasFanOfKnives
		{
			get
			{
				return CombatManager.Instance.IsInProgress && base.Owner.Creature.HasPower<FanOfKnivesPower>();
			}
		}
	}
}
