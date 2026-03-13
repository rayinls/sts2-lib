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
	// Token: 0x02000A55 RID: 2645
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Sloth : CardModel, KnowledgeDemon.IChoosable
	{
		// Token: 0x06007042 RID: 28738 RVA: 0x00266CEF File Offset: 0x00264EEF
		public Sloth()
			: base(-1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001E98 RID: 7832
		// (get) Token: 0x06007043 RID: 28739 RVA: 0x00266CFC File Offset: 0x00264EFC
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001E99 RID: 7833
		// (get) Token: 0x06007044 RID: 28740 RVA: 0x00266CFF File Offset: 0x00264EFF
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001E9A RID: 7834
		// (get) Token: 0x06007045 RID: 28741 RVA: 0x00266D02 File Offset: 0x00264F02
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<SlothPower>(3m));
			}
		}

		// Token: 0x06007046 RID: 28742 RVA: 0x00266D14 File Offset: 0x00264F14
		public async Task OnChosen()
		{
			await PowerCmd.Apply<SlothPower>(base.Owner.Creature, base.DynamicVars["SlothPower"].IntValue, base.Owner.Creature, this, false);
		}
	}
}
