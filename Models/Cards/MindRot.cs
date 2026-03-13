using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009CC RID: 2508
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MindRot : CardModel, KnowledgeDemon.IChoosable
	{
		// Token: 0x06006D71 RID: 28017 RVA: 0x00261419 File Offset: 0x0025F619
		public MindRot()
			: base(-1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001D6A RID: 7530
		// (get) Token: 0x06006D72 RID: 28018 RVA: 0x00261426 File Offset: 0x0025F626
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D6B RID: 7531
		// (get) Token: 0x06006D73 RID: 28019 RVA: 0x00261429 File Offset: 0x0025F629
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001D6C RID: 7532
		// (get) Token: 0x06006D74 RID: 28020 RVA: 0x0026142C File Offset: 0x0025F62C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<MindRotPower>(1m));
			}
		}

		// Token: 0x06006D75 RID: 28021 RVA: 0x00261440 File Offset: 0x0025F640
		public async Task OnChosen()
		{
			await PowerCmd.Apply<MindRotPower>(base.Owner.Creature, base.DynamicVars["MindRotPower"].IntValue, base.Owner.Creature, this, false);
		}
	}
}
