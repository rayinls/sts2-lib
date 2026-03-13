using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005B9 RID: 1465
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class Vambrace : RelicModel
	{
		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06005060 RID: 20576 RVA: 0x0021CDE9 File Offset: 0x0021AFE9
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06005061 RID: 20577 RVA: 0x0021CDEC File Offset: 0x0021AFEC
		// (set) Token: 0x06005062 RID: 20578 RVA: 0x0021CDF4 File Offset: 0x0021AFF4
		private CardModel TriggeringCard
		{
			get
			{
				return this._triggeringCard;
			}
			set
			{
				base.AssertMutable();
				this._triggeringCard = value;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x06005063 RID: 20579 RVA: 0x0021CE03 File Offset: 0x0021B003
		// (set) Token: 0x06005064 RID: 20580 RVA: 0x0021CE0B File Offset: 0x0021B00B
		private bool BlockGainedThisCombat
		{
			get
			{
				return this._blockGainedThisCombat;
			}
			set
			{
				base.AssertMutable();
				this._blockGainedThisCombat = value;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06005065 RID: 20581 RVA: 0x0021CE1A File Offset: 0x0021B01A
		[Nullable(1)]
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			[NullableContext(1)]
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x0021CE2C File Offset: 0x0021B02C
		[NullableContext(1)]
		public override Task BeforeCombatStart()
		{
			this.TriggeringCard = null;
			this.BlockGainedThisCombat = false;
			base.Status = RelicStatus.Active;
			return Task.CompletedTask;
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x0021CE48 File Offset: 0x0021B048
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
			if (this.TriggeringCard != null && this.TriggeringCard != cardSource)
			{
				return 1m;
			}
			if (target != base.Owner.Creature)
			{
				return 1m;
			}
			if (this.BlockGainedThisCombat)
			{
				return 1m;
			}
			return 2m;
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x0021CEAD File Offset: 0x0021B0AD
		[return: Nullable(1)]
		public override Task AfterModifyingBlockAmount(decimal modifiedAmount, CardModel cardSource, CardPlay cardPlay)
		{
			if (modifiedAmount <= 0m)
			{
				return Task.CompletedTask;
			}
			if (cardSource == null)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			base.Status = RelicStatus.Normal;
			this.TriggeringCard = cardSource;
			return Task.CompletedTask;
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x0021CEE4 File Offset: 0x0021B0E4
		[NullableContext(1)]
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card != this.TriggeringCard)
			{
				return Task.CompletedTask;
			}
			if (this.BlockGainedThisCombat)
			{
				return Task.CompletedTask;
			}
			this.BlockGainedThisCombat = true;
			return Task.CompletedTask;
		}

		// Token: 0x0600506A RID: 20586 RVA: 0x0021CF38 File Offset: 0x0021B138
		[NullableContext(1)]
		public override Task AfterCombatEnd(CombatRoom room)
		{
			this.TriggeringCard = null;
			this.BlockGainedThisCombat = false;
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x0400223C RID: 8764
		private CardModel _triggeringCard;

		// Token: 0x0400223D RID: 8765
		private bool _blockGainedThisCombat;
	}
}
