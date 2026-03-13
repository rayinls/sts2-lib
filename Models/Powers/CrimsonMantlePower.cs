using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005FC RID: 1532
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CrimsonMantlePower : PowerModel
	{
		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x060051D5 RID: 20949 RVA: 0x002200AF File Offset: 0x0021E2AF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x060051D6 RID: 20950 RVA: 0x002200B2 File Offset: 0x0021E2B2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x060051D7 RID: 20951 RVA: 0x002200B5 File Offset: 0x0021E2B5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x060051D8 RID: 20952 RVA: 0x002200C7 File Offset: 0x0021E2C7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar("SelfDamage", 0m, ValueProp.Unblockable | ValueProp.Unpowered));
			}
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x002200E0 File Offset: 0x0021E2E0
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner.Player)
			{
				base.Flash();
				DamageVar damageVar = (DamageVar)base.DynamicVars["SelfDamage"];
				await CreatureCmd.Damage(choiceContext, base.Owner, damageVar.BaseValue, damageVar.Props, base.Owner, null);
				await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
			}
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x00220134 File Offset: 0x0021E334
		public void IncrementSelfDamage()
		{
			base.AssertMutable();
			DynamicVar dynamicVar = base.DynamicVars["SelfDamage"];
			decimal baseValue = dynamicVar.BaseValue;
			dynamicVar.BaseValue = baseValue + 1m;
		}

		// Token: 0x0400224F RID: 8783
		private const string _selfDamageKey = "SelfDamage";
	}
}
