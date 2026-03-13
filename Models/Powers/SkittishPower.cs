using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000698 RID: 1688
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SkittishPower : PowerModel
	{
		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x0600555D RID: 21853 RVA: 0x002265CB File Offset: 0x002247CB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x0600555E RID: 21854 RVA: 0x002265CE File Offset: 0x002247CE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x0600555F RID: 21855 RVA: 0x002265D1 File Offset: 0x002247D1
		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06005560 RID: 21856 RVA: 0x002265D4 File Offset: 0x002247D4
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06005561 RID: 21857 RVA: 0x002265E6 File Offset: 0x002247E6
		// (set) Token: 0x06005562 RID: 21858 RVA: 0x002265F3 File Offset: 0x002247F3
		public bool HasGainedBlockThisTurn
		{
			get
			{
				return base.GetInternalData<SkittishPower.Data>().hasGainedBlockThisTurn;
			}
			private set
			{
				base.AssertMutable();
				base.GetInternalData<SkittishPower.Data>().hasGainedBlockThisTurn = value;
			}
		}

		// Token: 0x06005563 RID: 21859 RVA: 0x00226607 File Offset: 0x00224807
		protected override object InitInternalData()
		{
			return new SkittishPower.Data();
		}

		// Token: 0x06005564 RID: 21860 RVA: 0x00226610 File Offset: 0x00224810
		public override async Task AfterAttack(AttackCommand command)
		{
			if (!this.HasGainedBlockThisTurn)
			{
				if (command.DamageProps.HasFlag(ValueProp.Move))
				{
					if (command.ModelSource is CardModel)
					{
						DamageResult damageResult = command.Results.FirstOrDefault((DamageResult r) => r.Receiver == base.Owner);
						if (damageResult != null)
						{
							if (damageResult.UnblockedDamage != 0)
							{
								this.HasGainedBlockThisTurn = true;
								SfxCmd.Play("event:/sfx/enemy/enemy_attacks/phantasmal_gardeners/phantasmal_gardeners_retract", 1f);
								await CreatureCmd.TriggerAnim(base.Owner, "BlockStart", 0.3f);
								await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x06005565 RID: 21861 RVA: 0x0022665C File Offset: 0x0022485C
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side != base.Owner.Side)
			{
				if (this.HasGainedBlockThisTurn)
				{
					SfxCmd.Play("event:/sfx/enemy/enemy_attacks/phantasmal_gardeners/phantasmal_gardeners_extend", 1f);
					await CreatureCmd.TriggerAnim(base.Owner, "BlockEnd", 0.15f);
				}
				this.HasGainedBlockThisTurn = false;
			}
		}

		// Token: 0x0400226F RID: 8815
		private const string _extendSfx = "event:/sfx/enemy/enemy_attacks/phantasmal_gardeners/phantasmal_gardeners_extend";

		// Token: 0x04002270 RID: 8816
		private const string _retractSfx = "event:/sfx/enemy/enemy_attacks/phantasmal_gardeners/phantasmal_gardeners_retract";

		// Token: 0x02001A8C RID: 6796
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006885 RID: 26757
			public bool hasGainedBlockThisTurn;
		}
	}
}
