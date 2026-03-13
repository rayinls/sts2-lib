using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000562 RID: 1378
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class PenNib : RelicModel
	{
		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06004E2F RID: 20015 RVA: 0x00218BAB File Offset: 0x00216DAB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06004E30 RID: 20016 RVA: 0x00218BAE File Offset: 0x00216DAE
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06004E31 RID: 20017 RVA: 0x00218BB1 File Offset: 0x00216DB1
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.AttacksPlayed % 10;
				}
				return 10;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06004E32 RID: 20018 RVA: 0x00218BC7 File Offset: 0x00216DC7
		// (set) Token: 0x06004E33 RID: 20019 RVA: 0x00218BCF File Offset: 0x00216DCF
		private bool IsActivating
		{
			get
			{
				return this._isActivating;
			}
			set
			{
				base.AssertMutable();
				this._isActivating = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06004E34 RID: 20020 RVA: 0x00218BE4 File Offset: 0x00216DE4
		// (set) Token: 0x06004E35 RID: 20021 RVA: 0x00218BEC File Offset: 0x00216DEC
		[SavedProperty]
		public int AttacksPlayed
		{
			get
			{
				return this._attacksPlayed;
			}
			private set
			{
				base.AssertMutable();
				this._attacksPlayed = value % 10;
				this.UpdateDisplay();
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06004E36 RID: 20022 RVA: 0x00218C04 File Offset: 0x00216E04
		// (set) Token: 0x06004E37 RID: 20023 RVA: 0x00218C0C File Offset: 0x00216E0C
		private CardModel AttackToDouble
		{
			get
			{
				return this._attackToDouble;
			}
			set
			{
				base.AssertMutable();
				this._attackToDouble = value;
			}
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x00218C1B File Offset: 0x00216E1B
		private void UpdateDisplay()
		{
			if (this.IsActivating)
			{
				base.Status = RelicStatus.Normal;
			}
			else
			{
				base.Status = ((this.AttacksPlayed == 9) ? RelicStatus.Active : RelicStatus.Normal);
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x00218C48 File Offset: 0x00216E48
		public void NotifyAttackPlayed()
		{
			int attacksPlayed = this.AttacksPlayed;
			this.AttacksPlayed = attacksPlayed + 1;
			if (this.AttacksPlayed == 0)
			{
				TaskHelper.RunSafely(this.DoActivateVisuals());
			}
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x00218C7C File Offset: 0x00216E7C
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			if (cardSource == null)
			{
				return 1m;
			}
			if (dealer != base.Owner.Creature && dealer != base.Owner.Osty)
			{
				return 1m;
			}
			if (this.AttackToDouble == null)
			{
				CardPile pile = cardSource.Pile;
				if ((pile == null || pile.Type != PileType.Play) && this.AttacksPlayed == 9)
				{
					return 2m;
				}
				return 1m;
			}
			else
			{
				if (cardSource == this.AttackToDouble)
				{
					return 2m;
				}
				return 1m;
			}
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x00218D18 File Offset: 0x00216F18
		[NullableContext(1)]
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Type != CardType.Attack)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			this.NotifyAttackPlayed();
			if (this.AttacksPlayed == 0)
			{
				this.AttackToDouble = cardPlay.Card;
			}
			return Task.CompletedTask;
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x00218D71 File Offset: 0x00216F71
		[NullableContext(1)]
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (this.AttackToDouble == null)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card != this.AttackToDouble)
			{
				return Task.CompletedTask;
			}
			this.AttackToDouble = null;
			return Task.CompletedTask;
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x00218DA4 File Offset: 0x00216FA4
		[NullableContext(1)]
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x040021FA RID: 8698
		private const int _attacksThreshold = 10;

		// Token: 0x040021FB RID: 8699
		private bool _isActivating;

		// Token: 0x040021FC RID: 8700
		private int _attacksPlayed;

		// Token: 0x040021FD RID: 8701
		private CardModel _attackToDouble;
	}
}
