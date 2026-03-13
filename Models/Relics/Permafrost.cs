using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000563 RID: 1379
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Permafrost : RelicModel
	{
		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06004E3F RID: 20031 RVA: 0x00218DEF File Offset: 0x00216FEF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06004E40 RID: 20032 RVA: 0x00218DF2 File Offset: 0x00216FF2
		// (set) Token: 0x06004E41 RID: 20033 RVA: 0x00218DFA File Offset: 0x00216FFA
		private bool ActivatedThisCombat
		{
			get
			{
				return this._activatedThisCombat;
			}
			set
			{
				base.AssertMutable();
				this._activatedThisCombat = value;
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06004E42 RID: 20034 RVA: 0x00218E09 File Offset: 0x00217009
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(6m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x00218E1C File Offset: 0x0021701C
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			if (!(room is CombatRoom))
			{
				return Task.CompletedTask;
			}
			this.ActivatedThisCombat = false;
			return Task.CompletedTask;
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x00218E38 File Offset: 0x00217038
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (CombatManager.Instance.IsInProgress)
			{
				if (cardPlay.Card.Owner == base.Owner)
				{
					if (cardPlay.Card.Type == CardType.Power)
					{
						if (!this.ActivatedThisCombat)
						{
							base.Flash();
							await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
							this.ActivatedThisCombat = true;
						}
					}
				}
			}
		}

		// Token: 0x040021FE RID: 8702
		private bool _activatedThisCombat;
	}
}
