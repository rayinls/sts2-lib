using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A5E RID: 2654
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SovereignBlade : CardModel
	{
		// Token: 0x06007074 RID: 28788 RVA: 0x0026728A File Offset: 0x0026548A
		public SovereignBlade()
			: base(2, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EB0 RID: 7856
		// (get) Token: 0x06007075 RID: 28789 RVA: 0x002672AF File Offset: 0x002654AF
		public override TargetType TargetType
		{
			get
			{
				if (!this.HasSeekingEdge)
				{
					return TargetType.AnyEnemy;
				}
				return TargetType.AllEnemies;
			}
		}

		// Token: 0x17001EB1 RID: 7857
		// (get) Token: 0x06007076 RID: 28790 RVA: 0x002672BC File Offset: 0x002654BC
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NSovereignBladeVfx.AssetPaths;
			}
		}

		// Token: 0x17001EB2 RID: 7858
		// (get) Token: 0x06007077 RID: 28791 RVA: 0x002672C3 File Offset: 0x002654C3
		// (set) Token: 0x06007078 RID: 28792 RVA: 0x002672CB File Offset: 0x002654CB
		private decimal CurrentDamage
		{
			get
			{
				return this._currentDamage;
			}
			set
			{
				base.AssertMutable();
				this._currentDamage = value;
			}
		}

		// Token: 0x17001EB3 RID: 7859
		// (get) Token: 0x06007079 RID: 28793 RVA: 0x002672DA File Offset: 0x002654DA
		// (set) Token: 0x0600707A RID: 28794 RVA: 0x002672E2 File Offset: 0x002654E2
		private decimal CurrentRepeats
		{
			get
			{
				return this._currentRepeats;
			}
			set
			{
				base.AssertMutable();
				this._currentRepeats = value;
			}
		}

		// Token: 0x17001EB4 RID: 7860
		// (get) Token: 0x0600707B RID: 28795 RVA: 0x002672F1 File Offset: 0x002654F1
		// (set) Token: 0x0600707C RID: 28796 RVA: 0x002672F9 File Offset: 0x002654F9
		public bool CreatedThroughForge
		{
			get
			{
				return this._createdThroughForge;
			}
			set
			{
				base.AssertMutable();
				this._createdThroughForge = value;
			}
		}

		// Token: 0x17001EB5 RID: 7861
		// (get) Token: 0x0600707D RID: 28797 RVA: 0x00267308 File Offset: 0x00265508
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Retain);
			}
		}

		// Token: 0x17001EB6 RID: 7862
		// (get) Token: 0x0600707E RID: 28798 RVA: 0x00267310 File Offset: 0x00265510
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[5];
				array[0] = new DamageVar(10m, ValueProp.Move);
				array[1] = new CalculationBaseVar(0m);
				array[2] = new CalculationExtraVar(1m);
				array[3] = new CalculatedVar("SeekingEdgeAmount").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => (card != null && card.IsMutable && card.Owner != null) ? card.Owner.Creature.GetPowerAmount<SeekingEdgePower>() : 0);
				array[4] = new RepeatVar(1);
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x0600707F RID: 28799 RVA: 0x0026738C File Offset: 0x0026558C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			AttackCommand attack = DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).WithHitCount(base.DynamicVars.Repeat.IntValue)
				.WithAttackerAnim("Cast", base.Owner.Character.AttackAnimDelay, null)
				.WithAttackerFx(null, "event:/sfx/characters/regent/regent_sovereign_blade", null);
			if (this.HasSeekingEdge)
			{
				attack = attack.TargetingAllOpponents(base.CombatState).BeforeDamage(delegate
				{
					NSovereignBladeVfx vfxNode = SovereignBlade.GetVfxNode(this.Owner, this);
					IReadOnlyList<Creature> hittableEnemies = this.CombatState.HittableEnemies;
					if (hittableEnemies.Count > 0)
					{
						NCombatRoom instance = NCombatRoom.Instance;
						NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(hittableEnemies[0]) : null);
						if (vfxNode != null && ncreature != null)
						{
							vfxNode.Attack(ncreature.VfxSpawnPosition);
						}
					}
					return Task.CompletedTask;
				}).WithHitFx("vfx/vfx_giant_horizontal_slash", null, "slash_attack.mp3");
			}
			else
			{
				ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
				attack = attack.Targeting(cardPlay.Target).BeforeDamage(delegate
				{
					NSovereignBladeVfx vfxNode2 = SovereignBlade.GetVfxNode(this.Owner, this);
					NCombatRoom instance2 = NCombatRoom.Instance;
					NCreature ncreature2 = ((instance2 != null) ? instance2.GetCreatureNode(cardPlay.Target) : null);
					if (vfxNode2 != null && ncreature2 != null)
					{
						vfxNode2.Attack(ncreature2.VfxSpawnPosition);
					}
					return Task.CompletedTask;
				}).WithHitVfxNode((Creature t) => NBigSlashVfx.Create(t))
					.WithHitVfxNode((Creature t) => NBigSlashImpactVfx.Create(t));
			}
			await attack.Execute(choiceContext);
			ParryPower power = base.Owner.Creature.GetPower<ParryPower>();
			if (power != null)
			{
				await power.AfterSovereignBladePlayed(base.Owner.Creature, attack.Results);
			}
		}

		// Token: 0x06007080 RID: 28800 RVA: 0x002673DF File Offset: 0x002655DF
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}

		// Token: 0x06007081 RID: 28801 RVA: 0x002673ED File Offset: 0x002655ED
		protected override void AfterCloned()
		{
			base.AfterCloned();
			this.CreatedThroughForge = false;
		}

		// Token: 0x06007082 RID: 28802 RVA: 0x002673FC File Offset: 0x002655FC
		protected override void AfterDowngraded()
		{
			base.AfterDowngraded();
			base.DynamicVars.Damage.BaseValue = this.CurrentDamage;
			base.DynamicVars.Repeat.BaseValue = this.CurrentRepeats;
		}

		// Token: 0x06007083 RID: 28803 RVA: 0x00267430 File Offset: 0x00265630
		public override void AfterTransformedFrom()
		{
			this.RemoveSovereignBladeNode();
		}

		// Token: 0x06007084 RID: 28804 RVA: 0x00267438 File Offset: 0x00265638
		public override Task AfterCardChangedPiles(CardModel card, PileType oldPileType, [Nullable(2)] AbstractModel source)
		{
			if (card != this)
			{
				return Task.CompletedTask;
			}
			if ((!this.CreatedThroughForge && oldPileType == PileType.None) || oldPileType == PileType.Exhaust)
			{
				ForgeCmd.PlayCombatRoomForgeVfx(base.Owner, this);
			}
			if (card.Pile.Type == PileType.Exhaust)
			{
				this.RemoveSovereignBladeNode();
			}
			return Task.CompletedTask;
		}

		// Token: 0x06007085 RID: 28805 RVA: 0x00267478 File Offset: 0x00265678
		public void AddDamage(decimal amount)
		{
			base.DynamicVars.Damage.BaseValue += amount;
			this.CurrentDamage = base.DynamicVars.Damage.BaseValue;
		}

		// Token: 0x06007086 RID: 28806 RVA: 0x002674AC File Offset: 0x002656AC
		public void SetRepeats(decimal amount)
		{
			base.DynamicVars.Repeat.BaseValue = amount;
			this.CurrentRepeats = base.DynamicVars.Repeat.BaseValue;
		}

		// Token: 0x06007087 RID: 28807 RVA: 0x002674D8 File Offset: 0x002656D8
		[return: Nullable(2)]
		public static NSovereignBladeVfx GetVfxNode(Player player, CardModel card)
		{
			CardModel originalCard = card.DupeOf ?? card;
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(player.Creature) : null);
			if (ncreature == null)
			{
				return null;
			}
			return ncreature.GetChildren(false).OfType<NSovereignBladeVfx>().FirstOrDefault((NSovereignBladeVfx b) => b.Card == originalCard);
		}

		// Token: 0x06007088 RID: 28808 RVA: 0x00267536 File Offset: 0x00265736
		private void RemoveSovereignBladeNode()
		{
			NSovereignBladeVfx vfxNode = SovereignBlade.GetVfxNode(base.Owner, this);
			if (vfxNode == null)
			{
				return;
			}
			vfxNode.RemoveSovereignBlade();
		}

		// Token: 0x17001EB7 RID: 7863
		// (get) Token: 0x06007089 RID: 28809 RVA: 0x0026754E File Offset: 0x0026574E
		private bool HasSeekingEdge
		{
			get
			{
				return CombatManager.Instance.IsInProgress && base.Owner.Creature.HasPower<SeekingEdgePower>();
			}
		}

		// Token: 0x040025CE RID: 9678
		private const int _baseDamage = 10;

		// Token: 0x040025CF RID: 9679
		private const string _sovereignBladeSfx = "event:/sfx/characters/regent/regent_sovereign_blade";

		// Token: 0x040025D0 RID: 9680
		private bool _createdThroughForge;

		// Token: 0x040025D1 RID: 9681
		private decimal _currentDamage = 10m;

		// Token: 0x040025D2 RID: 9682
		private decimal _currentRepeats = 1m;
	}
}
