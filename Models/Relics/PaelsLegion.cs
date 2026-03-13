using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000557 RID: 1367
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaelsLegion : RelicModel
	{
		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06004DD6 RID: 19926 RVA: 0x0021817F File Offset: 0x0021637F
		public override bool AddsPet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06004DD7 RID: 19927 RVA: 0x00218182 File Offset: 0x00216382
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06004DD8 RID: 19928 RVA: 0x00218185 File Offset: 0x00216385
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06004DD9 RID: 19929 RVA: 0x00218197 File Offset: 0x00216397
		public override bool ShowCounter
		{
			get
			{
				return this.DisplayAmount > 0;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06004DDA RID: 19930 RVA: 0x002181A2 File Offset: 0x002163A2
		public static string[] SkinOptions
		{
			get
			{
				return new string[] { "eyes", "horns", "spikes", "wings" };
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06004DDB RID: 19931 RVA: 0x002181CA File Offset: 0x002163CA
		// (set) Token: 0x06004DDC RID: 19932 RVA: 0x002181D2 File Offset: 0x002163D2
		[SavedProperty]
		public string Skin
		{
			get
			{
				return this._skin;
			}
			set
			{
				base.AssertMutable();
				this._skin = value;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06004DDD RID: 19933 RVA: 0x002181E1 File Offset: 0x002163E1
		public override int DisplayAmount
		{
			get
			{
				if (!CombatManager.Instance.IsInProgress)
				{
					return -1;
				}
				if (base.IsCanonical)
				{
					return -1;
				}
				if (this._cooldown <= 0)
				{
					return -1;
				}
				return this._cooldown;
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06004DDE RID: 19934 RVA: 0x0021820C File Offset: 0x0021640C
		// (set) Token: 0x06004DDF RID: 19935 RVA: 0x00218214 File Offset: 0x00216414
		private int Cooldown
		{
			get
			{
				return this._cooldown;
			}
			set
			{
				base.AssertMutable();
				this._cooldown = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06004DE0 RID: 19936 RVA: 0x00218229 File Offset: 0x00216429
		// (set) Token: 0x06004DE1 RID: 19937 RVA: 0x00218231 File Offset: 0x00216431
		private bool TriggeredBlockLastTurn
		{
			get
			{
				return this._triggeredBlockLastTurn;
			}
			set
			{
				base.AssertMutable();
				this._triggeredBlockLastTurn = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06004DE2 RID: 19938 RVA: 0x00218246 File Offset: 0x00216446
		// (set) Token: 0x06004DE3 RID: 19939 RVA: 0x0021824E File Offset: 0x0021644E
		[Nullable(2)]
		private CardPlay AffectedCardPlay
		{
			[NullableContext(2)]
			get
			{
				return this._affectedCardPlay;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._affectedCardPlay = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06004DE4 RID: 19940 RVA: 0x00218263 File Offset: 0x00216463
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Turns", 2m));
			}
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x0021827C File Offset: 0x0021647C
		public override async Task AfterObtained()
		{
			this.Skin = new Rng((uint)(base.Owner.NetId + (ulong)base.Owner.RunState.Rng.Seed), 0).NextItem<string>(PaelsLegion.SkinOptions);
			if (CombatManager.Instance.IsInProgress)
			{
				await this.SummonPet();
			}
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x002182C0 File Offset: 0x002164C0
		public override async Task BeforeCombatStart()
		{
			await this.SummonPet();
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x00218304 File Offset: 0x00216504
		[NullableContext(2)]
		public override decimal ModifyBlockMultiplicative([Nullable(1)] Creature target, decimal block, ValueProp props, CardModel cardSource, CardPlay cardPlay)
		{
			if (!props.IsCardOrMonsterMove())
			{
				return 1m;
			}
			if (cardSource == null)
			{
				return 1m;
			}
			if (target != base.Owner.Creature)
			{
				return 1m;
			}
			if (this.Cooldown > 0)
			{
				return 1m;
			}
			return 2m;
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x00218354 File Offset: 0x00216554
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterModifyingBlockAmount(decimal modifiedAmount, CardModel cardSource, CardPlay cardPlay)
		{
			if (modifiedAmount <= 0m)
			{
				return Task.CompletedTask;
			}
			if (cardPlay == null)
			{
				return Task.CompletedTask;
			}
			if (this.AffectedCardPlay != null && this.AffectedCardPlay != cardPlay)
			{
				return Task.CompletedTask;
			}
			this.AffectedCardPlay = cardPlay;
			return Task.CompletedTask;
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x002183A0 File Offset: 0x002165A0
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (this.AffectedCardPlay != null)
			{
				if (this.AffectedCardPlay == cardPlay)
				{
					base.Flash();
					this.AffectedCardPlay = null;
					this.Cooldown = base.DynamicVars["Turns"].IntValue;
					base.Status = RelicStatus.Normal;
					PaelsLegion paelsLegion = (PaelsLegion)base.Owner.PlayerCombatState.GetPet<PaelsLegion>().Monster;
					await CreatureCmd.TriggerAnim(paelsLegion.Creature, "BlockTrigger", 0.15f);
					this.TriggeredBlockLastTurn = true;
				}
			}
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x002183EC File Offset: 0x002165EC
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				bool flag = this.Cooldown > 0;
				int cooldown = this.Cooldown;
				this.Cooldown = cooldown - 1;
				if (this.Cooldown <= 0)
				{
					base.Status = RelicStatus.Active;
					base.InvokeDisplayAmountChanged();
				}
				PaelsLegion paelsLegion = (PaelsLegion)base.Owner.PlayerCombatState.GetPet<PaelsLegion>().Monster;
				if (this.Cooldown > 0 && this.TriggeredBlockLastTurn)
				{
					await CreatureCmd.TriggerAnim(paelsLegion.Creature, "SleepTrigger", 0.15f);
				}
				else if (this.Cooldown <= 0 && flag)
				{
					await CreatureCmd.TriggerAnim(paelsLegion.Creature, "WakeUpTrigger", 0.15f);
				}
				this.TriggeredBlockLastTurn = false;
			}
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x00218437 File Offset: 0x00216637
		public override Task AfterCombatEnd(CombatRoom _)
		{
			base.Status = RelicStatus.Normal;
			this.Cooldown = 0;
			this.TriggeredBlockLastTurn = false;
			this.AffectedCardPlay = null;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x00218460 File Offset: 0x00216660
		private async Task SummonPet()
		{
			await PlayerCmd.AddPet<PaelsLegion>(base.Owner);
		}

		// Token: 0x040021ED RID: 8685
		private const string _turnsKey = "Turns";

		// Token: 0x040021EE RID: 8686
		private string _skin = PaelsLegion.SkinOptions[0];

		// Token: 0x040021EF RID: 8687
		private int _cooldown;

		// Token: 0x040021F0 RID: 8688
		private bool _triggeredBlockLastTurn;

		// Token: 0x040021F1 RID: 8689
		[Nullable(2)]
		private CardPlay _affectedCardPlay;
	}
}
