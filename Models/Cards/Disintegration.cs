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
	// Token: 0x02000921 RID: 2337
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Disintegration : CardModel, KnowledgeDemon.IChoosable
	{
		// Token: 0x060069C9 RID: 27081 RVA: 0x00259E7B File Offset: 0x0025807B
		public Disintegration()
			: base(-1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001BDF RID: 7135
		// (get) Token: 0x060069CA RID: 27082 RVA: 0x00259E88 File Offset: 0x00258088
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001BE0 RID: 7136
		// (get) Token: 0x060069CB RID: 27083 RVA: 0x00259E8B File Offset: 0x0025808B
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BE1 RID: 7137
		// (get) Token: 0x060069CC RID: 27084 RVA: 0x00259E8E File Offset: 0x0025808E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<DisintegrationPower>(6m));
			}
		}

		// Token: 0x060069CD RID: 27085 RVA: 0x00259EA0 File Offset: 0x002580A0
		public async Task OnChosen()
		{
			await PowerCmd.Apply<DisintegrationPower>(base.Owner.Creature, base.DynamicVars["DisintegrationPower"].BaseValue, base.Owner.Creature, this, false);
		}
	}
}
