using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005A0 RID: 1440
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SwordOfStone : RelicModel
	{
		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06004FB9 RID: 20409 RVA: 0x0021BA5B File Offset: 0x00219C5B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06004FBA RID: 20410 RVA: 0x0021BA5E File Offset: 0x00219C5E
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06004FBB RID: 20411 RVA: 0x0021BA61 File Offset: 0x00219C61
		public override int DisplayAmount
		{
			get
			{
				return this.ElitesDefeated;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06004FBC RID: 20412 RVA: 0x0021BA69 File Offset: 0x00219C69
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Elites", 5m));
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06004FBD RID: 20413 RVA: 0x0021BA80 File Offset: 0x00219C80
		// (set) Token: 0x06004FBE RID: 20414 RVA: 0x0021BA88 File Offset: 0x00219C88
		[SavedProperty]
		public int ElitesDefeated
		{
			get
			{
				return this._elitesDefeated;
			}
			set
			{
				base.AssertMutable();
				this._elitesDefeated = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x0021BAA0 File Offset: 0x00219CA0
		public override async Task AfterCombatVictory(CombatRoom room)
		{
			if (room.RoomType == RoomType.Elite)
			{
				int elitesDefeated = this.ElitesDefeated;
				this.ElitesDefeated = elitesDefeated + 1;
				base.Flash();
				if (this.ElitesDefeated >= base.DynamicVars["Elites"].BaseValue)
				{
					await RelicCmd.Replace(this, ModelDb.Relic<SwordOfJade>().ToMutable());
				}
			}
		}

		// Token: 0x04002221 RID: 8737
		private const string _elitesKey = "Elites";

		// Token: 0x04002222 RID: 8738
		private int _elitesDefeated;
	}
}
