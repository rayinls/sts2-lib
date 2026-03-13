using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000566 RID: 1382
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PhylacteryUnbound : RelicModel
	{
		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06004E51 RID: 20049 RVA: 0x00218FF7 File Offset: 0x002171F7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Starter;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06004E52 RID: 20050 RVA: 0x00218FFA File Offset: 0x002171FA
		public override bool SpawnsPets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06004E53 RID: 20051 RVA: 0x00218FFD File Offset: 0x002171FD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new SummonVar("StartOfCombat", 5m),
					new SummonVar("StartOfTurn", 2m)
				});
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06004E54 RID: 20052 RVA: 0x00219030 File Offset: 0x00217230
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonStatic, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x00219044 File Offset: 0x00217244
		public override async Task BeforeCombatStart()
		{
			await OstyCmd.Summon(new ThrowingPlayerChoiceContext(), base.Owner, base.DynamicVars["StartOfCombat"].BaseValue, this);
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x00219088 File Offset: 0x00217288
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == CombatSide.Player)
			{
				await OstyCmd.Summon(new ThrowingPlayerChoiceContext(), base.Owner, base.DynamicVars["StartOfTurn"].BaseValue, this);
			}
		}

		// Token: 0x040021FF RID: 8703
		private const string _startOfCombatKey = "StartOfCombat";

		// Token: 0x04002200 RID: 8704
		private const string _startOfTurnKey = "StartOfTurn";
	}
}
