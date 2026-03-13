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
using MegaCrit.Sts2.Core.Models.Orbs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005A1 RID: 1441
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SymbioticVirus : RelicModel
	{
		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06004FC1 RID: 20417 RVA: 0x0021BAF3 File Offset: 0x00219CF3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06004FC2 RID: 20418 RVA: 0x0021BAF6 File Offset: 0x00219CF6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Dark", 1m));
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06004FC3 RID: 20419 RVA: 0x0021BB0C File Offset: 0x00219D0C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<DarkOrb>()
				});
			}
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x0021BB30 File Offset: 0x00219D30
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					int i = 0;
					while (i < base.DynamicVars["Dark"].BaseValue)
					{
						await OrbCmd.Channel<DarkOrb>(new BlockingPlayerChoiceContext(), base.Owner);
						i++;
					}
				}
			}
		}

		// Token: 0x04002223 RID: 8739
		private const string _darknessKey = "Dark";
	}
}
