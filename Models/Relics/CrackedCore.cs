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
	// Token: 0x020004DA RID: 1242
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CrackedCore : RelicModel
	{
		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06004AC6 RID: 19142 RVA: 0x0021296B File Offset: 0x00210B6B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Starter;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06004AC7 RID: 19143 RVA: 0x0021296E File Offset: 0x00210B6E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Lightning", 1m));
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06004AC8 RID: 19144 RVA: 0x00212984 File Offset: 0x00210B84
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<LightningOrb>()
				});
			}
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x002129A8 File Offset: 0x00210BA8
		public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					int i = 0;
					while (i < base.DynamicVars["Lightning"].BaseValue)
					{
						await OrbCmd.Channel<LightningOrb>(new BlockingPlayerChoiceContext(), base.Owner);
						i++;
					}
				}
			}
		}

		// Token: 0x040021A1 RID: 8609
		private const string _lightningKey = "Lightning";
	}
}
