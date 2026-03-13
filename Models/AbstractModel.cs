using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.CardRewardAlternatives;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Merchant;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Models.Exceptions;
using MegaCrit.Sts2.Core.Multiplayer.Serialization;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.TestSupport;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.SourceGeneration;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x0200048C RID: 1164
	[NullableContext(1)]
	[Nullable(0)]
	[GenerateSubtypes(DynamicallyAccessedMemberTypes = DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
	public abstract class AbstractModel : IComparable<AbstractModel>
	{
		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06004478 RID: 17528 RVA: 0x001FB834 File Offset: 0x001F9A34
		// (remove) Token: 0x06004479 RID: 17529 RVA: 0x001FB86C File Offset: 0x001F9A6C
		[Nullable(new byte[] { 2, 1 })]
		[field: Nullable(new byte[] { 2, 1 })]
		public event Action<AbstractModel> ExecutionFinished;

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x0600447A RID: 17530 RVA: 0x001FB8A1 File Offset: 0x001F9AA1
		public ModelId Id { get; }

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x0600447B RID: 17531 RVA: 0x001FB8A9 File Offset: 0x001F9AA9
		// (set) Token: 0x0600447C RID: 17532 RVA: 0x001FB8B1 File Offset: 0x001F9AB1
		public bool IsMutable { get; private set; }

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x0600447D RID: 17533 RVA: 0x001FB8BA File Offset: 0x001F9ABA
		public bool IsCanonical
		{
			get
			{
				return !this.IsMutable;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x0600447E RID: 17534 RVA: 0x001FB8C5 File Offset: 0x001F9AC5
		// (set) Token: 0x0600447F RID: 17535 RVA: 0x001FB8CD File Offset: 0x001F9ACD
		public int CategorySortingId { get; private set; }

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x001FB8D6 File Offset: 0x001F9AD6
		// (set) Token: 0x06004481 RID: 17537 RVA: 0x001FB8DE File Offset: 0x001F9ADE
		public int EntrySortingId { get; private set; }

		// Token: 0x06004482 RID: 17538 RVA: 0x001FB8E8 File Offset: 0x001F9AE8
		protected AbstractModel()
		{
			Type type = base.GetType();
			if (ModelDb.Contains(type))
			{
				throw new DuplicateModelException(type);
			}
			this.Id = ModelDb.GetId(type);
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x001FB91D File Offset: 0x001F9B1D
		public void InitId(ModelId id)
		{
			this.AssertCanonical();
			this.CategorySortingId = ModelIdSerializationCache.GetNetIdForCategory(this.Id.Category);
			this.EntrySortingId = ModelIdSerializationCache.GetNetIdForEntry(this.Id.Entry);
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x001FB951 File Offset: 0x001F9B51
		[NullableContext(2)]
		public virtual int CompareTo(AbstractModel other)
		{
			if (this == other)
			{
				return 0;
			}
			if (other == null)
			{
				return 1;
			}
			return this.Id.CompareTo(other.Id);
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x001FB96F File Offset: 0x001F9B6F
		public void AssertMutable()
		{
			if (!this.IsMutable)
			{
				throw new CanonicalModelException(base.GetType());
			}
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x001FB985 File Offset: 0x001F9B85
		public void AssertCanonical()
		{
			if (this.IsMutable)
			{
				throw new MutableModelException(base.GetType());
			}
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x001FB99B File Offset: 0x001F9B9B
		public AbstractModel ClonePreservingMutability()
		{
			if (!this.IsMutable)
			{
				return this;
			}
			return this.MutableClone();
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x001FB9B0 File Offset: 0x001F9BB0
		public AbstractModel MutableClone()
		{
			AbstractModel abstractModel = (AbstractModel)base.MemberwiseClone();
			abstractModel.IsMutable = true;
			abstractModel.DeepCloneFields();
			abstractModel.AfterCloned();
			return abstractModel;
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x001FB9DD File Offset: 0x001F9BDD
		protected virtual void DeepCloneFields()
		{
			this.AssertMutable();
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x001FB9E5 File Offset: 0x001F9BE5
		protected virtual void AfterCloned()
		{
			this.ExecutionFinished = null;
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x001FB9EE File Offset: 0x001F9BEE
		public void InvokeExecutionFinished()
		{
			Action<AbstractModel> executionFinished = this.ExecutionFinished;
			if (executionFinished == null)
			{
				return;
			}
			executionFinished(this);
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x0600448C RID: 17548 RVA: 0x001FBA01 File Offset: 0x001F9C01
		public virtual bool PreviewOutsideOfCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x0600448D RID: 17549
		public abstract bool ShouldReceiveCombatHooks { get; }

		// Token: 0x0600448E RID: 17550 RVA: 0x001FBA04 File Offset: 0x001F9C04
		public virtual Task AfterActEntered()
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x001FBA0B File Offset: 0x001F9C0B
		public virtual Task AfterAddToDeckPrevented(CardModel card)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x001FBA12 File Offset: 0x001F9C12
		public virtual Task BeforeAttack(AttackCommand command)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x001FBA19 File Offset: 0x001F9C19
		public virtual Task AfterAttack(AttackCommand command)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x001FBA20 File Offset: 0x001F9C20
		public virtual Task AfterBlockCleared(Creature creature)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x001FBA27 File Offset: 0x001F9C27
		public virtual Task BeforeBlockGained(Creature creature, decimal amount, ValueProp props, [Nullable(2)] CardModel cardSource)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x001FBA2E File Offset: 0x001F9C2E
		public virtual Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, [Nullable(2)] CardModel cardSource)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x001FBA35 File Offset: 0x001F9C35
		public virtual Task AfterBlockBroken(Creature creature)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x001FBA3C File Offset: 0x001F9C3C
		public virtual Task AfterCardChangedPiles(CardModel card, PileType oldPileType, [Nullable(2)] AbstractModel source)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x001FBA43 File Offset: 0x001F9C43
		public virtual Task AfterCardChangedPilesLate(CardModel card, PileType oldPileType, [Nullable(2)] AbstractModel source)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x001FBA4A File Offset: 0x001F9C4A
		public virtual Task AfterCardDiscarded(PlayerChoiceContext choiceContext, CardModel card)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x001FBA51 File Offset: 0x001F9C51
		public virtual Task AfterCardDrawnEarly(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x001FBA58 File Offset: 0x001F9C58
		public virtual Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x001FBA5F File Offset: 0x001F9C5F
		public virtual Task AfterCardEnteredCombat(CardModel card)
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x001FBA66 File Offset: 0x001F9C66
		public virtual Task AfterCardGeneratedForCombat(CardModel card, bool addedByPlayer)
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x001FBA6D File Offset: 0x001F9C6D
		public virtual Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x001FBA74 File Offset: 0x001F9C74
		public virtual Task BeforeCardAutoPlayed(CardModel card, [Nullable(2)] Creature target, AutoPlayType type)
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x001FBA7B File Offset: 0x001F9C7B
		public virtual Task BeforeCardPlayed(CardPlay cardPlay)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x001FBA82 File Offset: 0x001F9C82
		public virtual Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x001FBA89 File Offset: 0x001F9C89
		public virtual Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x001FBA90 File Offset: 0x001F9C90
		public virtual Task AfterCardRetained(CardModel card)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x001FBA97 File Offset: 0x001F9C97
		public virtual Task BeforeCombatStart()
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x001FBA9E File Offset: 0x001F9C9E
		public virtual Task BeforeCombatStartLate()
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x001FBAA5 File Offset: 0x001F9CA5
		public virtual Task AfterCombatEnd(CombatRoom room)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x001FBAAC File Offset: 0x001F9CAC
		public virtual Task AfterCombatVictoryEarly(CombatRoom room)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x001FBAB3 File Offset: 0x001F9CB3
		public virtual Task AfterCombatVictory(CombatRoom room)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x001FBABA File Offset: 0x001F9CBA
		public virtual Task AfterCreatureAddedToCombat(Creature creature)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x001FBAC1 File Offset: 0x001F9CC1
		public virtual Task AfterCurrentHpChanged(Creature creature, decimal delta)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x001FBAC8 File Offset: 0x001F9CC8
		public virtual Task AfterDamageGiven(PlayerChoiceContext choiceContext, [Nullable(2)] Creature dealer, DamageResult result, ValueProp props, Creature target, [Nullable(2)] CardModel cardSource)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x001FBACF File Offset: 0x001F9CCF
		public virtual Task BeforeDamageReceived(PlayerChoiceContext choiceContext, Creature target, decimal amount, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x001FBAD6 File Offset: 0x001F9CD6
		public virtual Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x001FBADD File Offset: 0x001F9CDD
		public virtual Task AfterDamageReceivedLate(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x001FBAE4 File Offset: 0x001F9CE4
		public virtual Task BeforeDeath(Creature creature)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x001FBAEB File Offset: 0x001F9CEB
		public virtual Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x001FBAF2 File Offset: 0x001F9CF2
		public virtual Task AfterDiedToDoom(PlayerChoiceContext choiceContext, IReadOnlyList<Creature> creatures)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x001FBAF9 File Offset: 0x001F9CF9
		public virtual Task AfterEnergyReset(Player player)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x001FBB00 File Offset: 0x001F9D00
		public virtual Task AfterEnergyResetLate(Player player)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x001FBB07 File Offset: 0x001F9D07
		public virtual Task AfterEnergySpent(CardModel card, int amount)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x001FBB0E File Offset: 0x001F9D0E
		public virtual Task BeforeCardRemoved(CardModel card)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x001FBB15 File Offset: 0x001F9D15
		public virtual Task BeforeFlush(PlayerChoiceContext choiceContext, Player player)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x001FBB1C File Offset: 0x001F9D1C
		public virtual Task BeforeFlushLate(PlayerChoiceContext choiceContext, Player player)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x001FBB23 File Offset: 0x001F9D23
		public virtual Task AfterGoldGained(Player player)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x001FBB2A File Offset: 0x001F9D2A
		public virtual Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x001FBB31 File Offset: 0x001F9D31
		public virtual Task BeforeHandDrawLate(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x001FBB38 File Offset: 0x001F9D38
		public virtual Task AfterHandEmptied(PlayerChoiceContext choiceContext, Player player)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x001FBB3F File Offset: 0x001F9D3F
		public virtual Task AfterItemPurchased(Player player, MerchantEntry itemPurchased, int goldSpent)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x001FBB46 File Offset: 0x001F9D46
		public virtual Task AfterMapGenerated(ActMap map, int actIndex)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x001FBB4D File Offset: 0x001F9D4D
		[NullableContext(2)]
		[return: Nullable(1)]
		public virtual Task AfterModifyingBlockAmount(decimal modifiedAmount, CardModel cardSource, CardPlay cardPlay)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x001FBB54 File Offset: 0x001F9D54
		public virtual Task AfterModifyingCardPlayCount(CardModel card)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x001FBB5B File Offset: 0x001F9D5B
		public virtual Task AfterModifyingCardPlayResultPileOrPosition(CardModel card, PileType pileType, CardPilePosition position)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x001FBB62 File Offset: 0x001F9D62
		public virtual Task AfterModifyingOrbPassiveTriggerCount(OrbModel orb)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x001FBB69 File Offset: 0x001F9D69
		public virtual Task AfterModifyingCardRewardOptions()
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x001FBB70 File Offset: 0x001F9D70
		public virtual Task AfterModifyingDamageAmount([Nullable(2)] CardModel cardSource)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x001FBB77 File Offset: 0x001F9D77
		public virtual Task AfterModifyingHandDraw()
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x001FBB7E File Offset: 0x001F9D7E
		public virtual Task AfterPreventingDraw()
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x001FBB85 File Offset: 0x001F9D85
		public virtual Task AfterModifyingHpLostBeforeOsty()
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x001FBB8C File Offset: 0x001F9D8C
		public virtual Task AfterModifyingHpLostAfterOsty()
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x001FBB93 File Offset: 0x001F9D93
		public virtual Task AfterModifyingPowerAmountReceived(PowerModel power)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x001FBB9A File Offset: 0x001F9D9A
		public virtual Task AfterModifyingPowerAmountGiven(PowerModel power)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x001FBBA1 File Offset: 0x001F9DA1
		public virtual Task AfterModifyingRewards()
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x001FBBA8 File Offset: 0x001F9DA8
		public virtual Task BeforeRewardsOffered(Player player, IReadOnlyList<Reward> rewards)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x001FBBAF File Offset: 0x001F9DAF
		public virtual Task AfterOrbChanneled(PlayerChoiceContext choiceContext, Player player, OrbModel orb)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x001FBBB6 File Offset: 0x001F9DB6
		public virtual Task AfterOrbEvoked(PlayerChoiceContext choiceContext, OrbModel orb, IEnumerable<Creature> targets)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x001FBBBD File Offset: 0x001F9DBD
		public virtual Task AfterOstyRevived(Creature osty)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x001FBBC4 File Offset: 0x001F9DC4
		public virtual Task BeforePotionUsed(PotionModel potion, [Nullable(2)] Creature target)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x001FBBCB File Offset: 0x001F9DCB
		public virtual Task AfterPotionUsed(PotionModel potion, [Nullable(2)] Creature target)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x001FBBD2 File Offset: 0x001F9DD2
		public virtual Task AfterPotionDiscarded(PotionModel potion)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x001FBBD9 File Offset: 0x001F9DD9
		public virtual Task AfterPotionProcured(PotionModel potion)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x001FBBE0 File Offset: 0x001F9DE0
		public virtual Task BeforePowerAmountChanged(PowerModel power, decimal amount, Creature target, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x001FBBE7 File Offset: 0x001F9DE7
		public virtual Task AfterPowerAmountChanged(PowerModel power, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x001FBBEE File Offset: 0x001F9DEE
		public virtual Task AfterPreventingBlockClear(AbstractModel preventer, Creature creature)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x001FBBF5 File Offset: 0x001F9DF5
		public virtual Task AfterPreventingDeath(Creature creature)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x001FBBFC File Offset: 0x001F9DFC
		public virtual Task AfterRestSiteHeal(Player player, bool isMimicked)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x001FBC03 File Offset: 0x001F9E03
		public virtual Task AfterRestSiteSmith(Player player)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x001FBC0A File Offset: 0x001F9E0A
		public virtual Task AfterRewardTaken(Player player, Reward reward)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x001FBC11 File Offset: 0x001F9E11
		public virtual Task BeforeRoomEntered(AbstractRoom room)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x001FBC18 File Offset: 0x001F9E18
		public virtual Task AfterRoomEntered(AbstractRoom room)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x001FBC1F File Offset: 0x001F9E1F
		public virtual Task AfterShuffle(PlayerChoiceContext choiceContext, Player shuffler)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x001FBC26 File Offset: 0x001F9E26
		public virtual Task AfterStarsSpent(int amount, Player spender)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x001FBC2D File Offset: 0x001F9E2D
		public virtual Task AfterStarsGained(int amount, Player gainer)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x001FBC34 File Offset: 0x001F9E34
		public virtual Task AfterForge(decimal amount, Player forger, [Nullable(2)] AbstractModel source)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x001FBC3B File Offset: 0x001F9E3B
		public virtual Task AfterSummon(PlayerChoiceContext choiceContext, Player summoner, decimal amount)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x001FBC42 File Offset: 0x001F9E42
		public virtual Task AfterTakingExtraTurn(Player player)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x001FBC49 File Offset: 0x001F9E49
		public virtual Task AfterTargetingBlockedVfx(Creature blocker)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x001FBC50 File Offset: 0x001F9E50
		public virtual Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x001FBC57 File Offset: 0x001F9E57
		public virtual Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x001FBC5E File Offset: 0x001F9E5E
		public virtual Task AfterPlayerTurnStartEarly(PlayerChoiceContext choiceContext, Player player)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x001FBC65 File Offset: 0x001F9E65
		public virtual Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x001FBC6C File Offset: 0x001F9E6C
		public virtual Task AfterPlayerTurnStartLate(PlayerChoiceContext choiceContext, Player player)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x001FBC73 File Offset: 0x001F9E73
		public virtual Task BeforePlayPhaseStart(PlayerChoiceContext choiceContext, Player player)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x001FBC7A File Offset: 0x001F9E7A
		public virtual Task BeforeTurnEndVeryEarly(PlayerChoiceContext choiceContext, CombatSide side)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x001FBC81 File Offset: 0x001F9E81
		public virtual Task BeforeTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x001FBC88 File Offset: 0x001F9E88
		public virtual Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x001FBC8F File Offset: 0x001F9E8F
		public virtual Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x001FBC96 File Offset: 0x001F9E96
		public virtual Task AfterTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x001FBC9D File Offset: 0x001F9E9D
		public virtual int ModifyAttackHitCount(AttackCommand attack, int hitCount)
		{
			return hitCount;
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x001FBCA0 File Offset: 0x001F9EA0
		[NullableContext(2)]
		public virtual decimal ModifyBlockAdditive([Nullable(1)] Creature target, decimal block, ValueProp props, CardModel cardSource, CardPlay cardPlay)
		{
			return 0m;
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x001FBCA7 File Offset: 0x001F9EA7
		[NullableContext(2)]
		public virtual decimal ModifyBlockMultiplicative([Nullable(1)] Creature target, decimal block, ValueProp props, CardModel cardSource, CardPlay cardPlay)
		{
			return 1m;
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x001FBCAE File Offset: 0x001F9EAE
		public virtual int ModifyCardPlayCount(CardModel card, [Nullable(2)] Creature target, int playCount)
		{
			return playCount;
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x001FBCB1 File Offset: 0x001F9EB1
		[NullableContext(0)]
		public virtual ValueTuple<PileType, CardPilePosition> ModifyCardPlayResultPileTypeAndPosition([Nullable(1)] CardModel card, bool isAutoPlay, ResourceInfo resources, PileType pileType, CardPilePosition position)
		{
			return new ValueTuple<PileType, CardPilePosition>(pileType, position);
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x001FBCBC File Offset: 0x001F9EBC
		public virtual int ModifyOrbPassiveTriggerCounts(OrbModel orb, int triggerCount)
		{
			return triggerCount;
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x001FBCBF File Offset: 0x001F9EBF
		public virtual CardCreationOptions ModifyCardRewardCreationOptions(Player player, CardCreationOptions options)
		{
			return options;
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x001FBCC2 File Offset: 0x001F9EC2
		public virtual CardCreationOptions ModifyCardRewardCreationOptionsLate(Player player, CardCreationOptions options)
		{
			return options;
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x001FBCC5 File Offset: 0x001F9EC5
		public virtual decimal ModifyCardRewardUpgradeOdds(Player player, CardModel card, decimal odds)
		{
			return odds;
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x001FBCC8 File Offset: 0x001F9EC8
		[NullableContext(2)]
		public virtual decimal ModifyDamageAdditive(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			return 0m;
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x001FBCCF File Offset: 0x001F9ECF
		[NullableContext(2)]
		public virtual decimal ModifyDamageCap(Creature target, ValueProp props, Creature dealer, CardModel cardSource)
		{
			return decimal.MaxValue;
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x001FBCDB File Offset: 0x001F9EDB
		[NullableContext(2)]
		public virtual decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			return 1m;
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x001FBCE2 File Offset: 0x001F9EE2
		public virtual ActMap ModifyGeneratedMap(IRunState runState, ActMap map, int actIndex)
		{
			return map;
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x001FBCE5 File Offset: 0x001F9EE5
		public virtual ActMap ModifyGeneratedMapLate(IRunState runState, ActMap map, int actIndex)
		{
			return map;
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x001FBCE8 File Offset: 0x001F9EE8
		public virtual decimal ModifyHandDraw(Player player, decimal count)
		{
			return count;
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x001FBCEB File Offset: 0x001F9EEB
		public virtual decimal ModifyHandDrawLate(Player player, decimal count)
		{
			return count;
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x001FBCEE File Offset: 0x001F9EEE
		public virtual decimal ModifyHealAmount(Creature creature, decimal amount)
		{
			return amount;
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x001FBCF1 File Offset: 0x001F9EF1
		[NullableContext(2)]
		public virtual decimal ModifyHpLostBeforeOsty([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			return amount;
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x001FBCF4 File Offset: 0x001F9EF4
		[NullableContext(2)]
		public virtual decimal ModifyHpLostBeforeOstyLate([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			return amount;
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x001FBCF7 File Offset: 0x001F9EF7
		[NullableContext(2)]
		public virtual decimal ModifyHpLostAfterOsty([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			return amount;
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x001FBCFA File Offset: 0x001F9EFA
		[NullableContext(2)]
		public virtual decimal ModifyHpLostAfterOstyLate([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			return amount;
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x001FBCFD File Offset: 0x001F9EFD
		public virtual decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			return amount;
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x001FBD00 File Offset: 0x001F9F00
		public virtual IEnumerable<CardModel> ModifyMerchantCardPool(Player player, IEnumerable<CardModel> options)
		{
			return options;
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x001FBD03 File Offset: 0x001F9F03
		public virtual CardRarity ModifyMerchantCardRarity(Player player, CardRarity rarity)
		{
			return rarity;
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x001FBD06 File Offset: 0x001F9F06
		public virtual void ModifyMerchantCardCreationResults(Player player, List<CardCreationResult> cards)
		{
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x001FBD08 File Offset: 0x001F9F08
		public virtual decimal ModifyMerchantPrice(Player player, MerchantEntry entry, decimal cost)
		{
			return cost;
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x001FBD0B File Offset: 0x001F9F0B
		public virtual decimal ModifyOrbValue(Player player, decimal value)
		{
			return value;
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x001FBD0E File Offset: 0x001F9F0E
		public virtual decimal ModifyPowerAmountGiven(PowerModel power, Creature giver, decimal amount, [Nullable(2)] Creature target, [Nullable(2)] CardModel cardSource)
		{
			return amount;
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x001FBD11 File Offset: 0x001F9F11
		public virtual decimal ModifyRestSiteHealAmount(Creature creature, decimal amount)
		{
			return amount;
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x001FBD14 File Offset: 0x001F9F14
		public virtual void ModifyShuffleOrder(Player player, List<CardModel> cards, bool isInitialShuffle)
		{
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x001FBD16 File Offset: 0x001F9F16
		public virtual decimal ModifySummonAmount(Player summoner, decimal amount, [Nullable(2)] AbstractModel source)
		{
			return amount;
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x001FBD19 File Offset: 0x001F9F19
		public virtual Creature ModifyUnblockedDamageTarget(Creature target, decimal amount, ValueProp props, [Nullable(2)] Creature dealer)
		{
			return target;
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x001FBD1C File Offset: 0x001F9F1C
		public virtual EventModel ModifyNextEvent(EventModel currentEvent)
		{
			return currentEvent;
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x001FBD1F File Offset: 0x001F9F1F
		public virtual IReadOnlySet<RoomType> ModifyUnknownMapPointRoomTypes(IReadOnlySet<RoomType> roomTypes)
		{
			return roomTypes;
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x001FBD22 File Offset: 0x001F9F22
		public virtual float ModifyOddsIncreaseForUnrolledRoomType(RoomType roomType, float oddsIncrease)
		{
			return oddsIncrease;
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x001FBD25 File Offset: 0x001F9F25
		public virtual int ModifyXValue(CardModel card, int originalValue)
		{
			return originalValue;
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x001FBD28 File Offset: 0x001F9F28
		public virtual bool TryModifyCardBeingAddedToDeck(CardModel card, [Nullable(2)] out CardModel newCard)
		{
			newCard = null;
			return false;
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x001FBD2E File Offset: 0x001F9F2E
		public virtual bool TryModifyCardBeingAddedToDeckLate(CardModel card, [Nullable(2)] out CardModel newCard)
		{
			newCard = null;
			return false;
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x001FBD34 File Offset: 0x001F9F34
		public virtual bool TryModifyCardRewardAlternatives(Player player, CardReward cardReward, List<CardRewardAlternative> alternatives)
		{
			return false;
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x001FBD37 File Offset: 0x001F9F37
		public virtual bool TryModifyCardRewardOptions(Player player, List<CardCreationResult> cardRewardOptions, CardCreationOptions creationOptions)
		{
			return false;
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x001FBD3A File Offset: 0x001F9F3A
		public virtual bool TryModifyCardRewardOptionsLate(Player player, List<CardCreationResult> cardRewardOptions, CardCreationOptions creationOptions)
		{
			return false;
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x001FBD3D File Offset: 0x001F9F3D
		public virtual bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			return false;
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x001FBD47 File Offset: 0x001F9F47
		public virtual bool TryModifyStarCost(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			return false;
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x001FBD51 File Offset: 0x001F9F51
		public virtual bool TryModifyPowerAmountReceived(PowerModel canonicalPower, Creature target, decimal amount, [Nullable(2)] Creature applier, out decimal modifiedAmount)
		{
			modifiedAmount = amount;
			return false;
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x001FBD5C File Offset: 0x001F9F5C
		public virtual bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
		{
			return false;
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x001FBD5F File Offset: 0x001F9F5F
		public virtual bool TryModifyRestSiteHealRewards(Player player, List<Reward> rewards, bool isMimicked)
		{
			return false;
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x001FBD62 File Offset: 0x001F9F62
		public virtual bool TryModifyRewards(Player player, List<Reward> rewards, [Nullable(2)] AbstractRoom room)
		{
			return false;
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x001FBD65 File Offset: 0x001F9F65
		public virtual bool TryModifyRewardsLate(Player player, List<Reward> rewards, [Nullable(2)] AbstractRoom room)
		{
			return false;
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x001FBD68 File Offset: 0x001F9F68
		public virtual IReadOnlyList<LocString> ModifyExtraRestSiteHealText(Player player, IReadOnlyList<LocString> currentExtraText)
		{
			return currentExtraText;
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x001FBD6B File Offset: 0x001F9F6B
		public virtual bool ShouldAddToDeck(CardModel card)
		{
			return true;
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x001FBD6E File Offset: 0x001F9F6E
		public virtual bool ShouldAfflict(CardModel card, AfflictionModel affliction)
		{
			return true;
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x001FBD71 File Offset: 0x001F9F71
		public virtual bool ShouldAllowAncient(Player player, AncientEventModel ancient)
		{
			return true;
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x001FBD74 File Offset: 0x001F9F74
		public virtual bool ShouldAllowHitting(Creature creature)
		{
			return true;
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x001FBD77 File Offset: 0x001F9F77
		public virtual bool ShouldAllowTargeting(Creature target)
		{
			return true;
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x001FBD7A File Offset: 0x001F9F7A
		public virtual bool ShouldAllowSelectingMoreCardRewards(Player player, CardReward cardReward)
		{
			return false;
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x001FBD7D File Offset: 0x001F9F7D
		public virtual bool ShouldClearBlock(Creature creature)
		{
			return true;
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x001FBD80 File Offset: 0x001F9F80
		public virtual bool ShouldDie(Creature creature)
		{
			return true;
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x001FBD83 File Offset: 0x001F9F83
		public virtual bool ShouldDieLate(Creature creature)
		{
			return true;
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x001FBD86 File Offset: 0x001F9F86
		public virtual bool ShouldDisableRemainingRestSiteOptions(Player player)
		{
			return true;
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x001FBD89 File Offset: 0x001F9F89
		public virtual bool ShouldDraw(Player player, bool fromHandDraw)
		{
			return true;
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x001FBD8C File Offset: 0x001F9F8C
		public virtual bool ShouldEtherealTrigger(CardModel card)
		{
			return true;
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x001FBD8F File Offset: 0x001F9F8F
		public virtual bool ShouldFlush(Player player)
		{
			return true;
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x001FBD92 File Offset: 0x001F9F92
		public virtual bool ShouldGainGold(decimal amount, Player player)
		{
			return true;
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x001FBD95 File Offset: 0x001F9F95
		public virtual bool ShouldGainStars(decimal amount, Player player)
		{
			return true;
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x001FBD98 File Offset: 0x001F9F98
		public virtual bool ShouldGenerateTreasure(Player player)
		{
			return true;
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x001FBD9B File Offset: 0x001F9F9B
		public virtual bool ShouldPayExcessEnergyCostWithStars(Player player)
		{
			return false;
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x001FBD9E File Offset: 0x001F9F9E
		public virtual bool ShouldPlay(CardModel card, AutoPlayType autoPlayType)
		{
			return true;
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x001FBDA1 File Offset: 0x001F9FA1
		public virtual bool ShouldPlayerResetEnergy(Player player)
		{
			return true;
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x001FBDA4 File Offset: 0x001F9FA4
		public virtual bool ShouldProceedToNextMapPoint()
		{
			return true;
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x001FBDA7 File Offset: 0x001F9FA7
		public virtual bool ShouldProcurePotion(PotionModel potion, Player player)
		{
			return true;
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x001FBDAA File Offset: 0x001F9FAA
		public virtual bool ShouldPowerBeRemovedOnDeath(PowerModel power)
		{
			return true;
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x001FBDAD File Offset: 0x001F9FAD
		public virtual bool ShouldRefillMerchantEntry(MerchantEntry entry, Player player)
		{
			return false;
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x001FBDB0 File Offset: 0x001F9FB0
		public virtual bool ShouldAllowMerchantCardRemoval(Player player)
		{
			return true;
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x001FBDB3 File Offset: 0x001F9FB3
		public virtual bool ShouldCreatureBeRemovedFromCombatAfterDeath(Creature creature)
		{
			return true;
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x001FBDB6 File Offset: 0x001F9FB6
		public virtual bool ShouldStopCombatFromEnding()
		{
			return false;
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x001FBDB9 File Offset: 0x001F9FB9
		public virtual bool ShouldTakeExtraTurn(Player player)
		{
			return false;
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x001FBDBC File Offset: 0x001F9FBC
		public virtual bool ShouldForcePotionReward(Player player, RoomType roomType)
		{
			return false;
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x001FBDC0 File Offset: 0x001F9FC0
		public override string ToString()
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
			defaultInterpolatedStringHandler.AppendFormatted<ModelId>(this.Id);
			defaultInterpolatedStringHandler.AppendLiteral(" (");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RuntimeHelpers.GetHashCode(this));
			defaultInterpolatedStringHandler.AppendLiteral(")");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x001FBE0F File Offset: 0x001FA00F
		protected void NeverEverCallThisOutsideOfTests_SetIsMutable(bool isMutable)
		{
			if (TestMode.IsOff)
			{
				throw new InvalidOperationException("You monster!");
			}
			this.IsMutable = isMutable;
		}
	}
}
