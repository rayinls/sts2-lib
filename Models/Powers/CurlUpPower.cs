using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000600 RID: 1536
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CurlUpPower : PowerModel
	{
		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x060051E8 RID: 20968 RVA: 0x00220254 File Offset: 0x0021E454
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x060051E9 RID: 20969 RVA: 0x00220257 File Offset: 0x0021E457
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x060051EA RID: 20970 RVA: 0x0022025A File Offset: 0x0021E45A
		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x060051EB RID: 20971 RVA: 0x0022025D File Offset: 0x0021E45D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x0022026F File Offset: 0x0021E46F
		protected override object InitInternalData()
		{
			return new CurlUpPower.Data();
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x00220278 File Offset: 0x0021E478
		public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult _, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (!props.IsPoweredAttack())
			{
				return Task.CompletedTask;
			}
			if (cardSource == null)
			{
				return Task.CompletedTask;
			}
			if (base.GetInternalData<CurlUpPower.Data>().playedCard != null && cardSource != base.GetInternalData<CurlUpPower.Data>().playedCard)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<CurlUpPower.Data>().playedCard = cardSource;
			return Task.CompletedTask;
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x002202E4 File Offset: 0x0021E4E4
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card == base.GetInternalData<CurlUpPower.Data>().playedCard)
			{
				base.GetInternalData<CurlUpPower.Data>().playedCard = null;
				SfxCmd.Play("event:/sfx/enemy/enemy_attacks/giant_louse/giant_louse_curl", 1f);
				await CreatureCmd.TriggerAnim(base.Owner, "Curl", 0.25f);
				await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
				LouseProgenitor louseProgenitor = base.Owner.Monster as LouseProgenitor;
				if (louseProgenitor != null)
				{
					louseProgenitor.Curled = true;
				}
				await PowerCmd.Remove(this);
			}
		}

		// Token: 0x020019D1 RID: 6609
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x040064DF RID: 25823
			[Nullable(2)]
			public CardModel playedCard;
		}
	}
}
