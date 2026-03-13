using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005EF RID: 1519
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ColossusPower : PowerModel
	{
		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x0600518D RID: 20877 RVA: 0x0021F907 File Offset: 0x0021DB07
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x0600518E RID: 20878 RVA: 0x0021F90A File Offset: 0x0021DB0A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x0600518F RID: 20879 RVA: 0x0021F90D File Offset: 0x0021DB0D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("DamageDecrease", 0.5m));
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06005190 RID: 20880 RVA: 0x0021F928 File Offset: 0x0021DB28
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x0021F934 File Offset: 0x0021DB34
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return 1m;
			}
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			if (dealer == null)
			{
				return 1m;
			}
			if (!dealer.HasPower<VulnerablePower>())
			{
				return 1m;
			}
			return base.DynamicVars["DamageDecrease"].BaseValue;
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x0021F98C File Offset: 0x0021DB8C
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Enemy)
			{
				await PowerCmd.TickDownDuration(this);
			}
		}

		// Token: 0x0400224C RID: 8780
		private const string _damageDecreaseKey = "DamageDecrease";
	}
}
