using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006B7 RID: 1719
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TagTeamPower : PowerModel
	{
		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06005612 RID: 22034 RVA: 0x00227AAF File Offset: 0x00225CAF
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06005613 RID: 22035 RVA: 0x00227AB2 File Offset: 0x00225CB2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06005614 RID: 22036 RVA: 0x00227AB5 File Offset: 0x00225CB5
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06005615 RID: 22037 RVA: 0x00227AB8 File Offset: 0x00225CB8
		public override int DisplayAmount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06005616 RID: 22038 RVA: 0x00227ABB File Offset: 0x00225CBB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StringVar("Applier", ""));
			}
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x00227AD4 File Offset: 0x00225CD4
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			((StringVar)base.DynamicVars["Applier"]).StringValue = PlatformUtil.GetPlayerName(RunManager.Instance.NetService.Platform, base.Applier.Player.NetId);
			return Task.CompletedTask;
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x00227B24 File Offset: 0x00225D24
		public override int ModifyCardPlayCount(CardModel card, [Nullable(2)] Creature target, int playCount)
		{
			if (card.Type != CardType.Attack)
			{
				return playCount;
			}
			if (card.Owner.Creature == base.Applier)
			{
				return playCount;
			}
			if (target != base.Owner)
			{
				return playCount;
			}
			return playCount + base.Amount;
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x00227B5C File Offset: 0x00225D5C
		public override async Task AfterModifyingCardPlayCount(CardModel card)
		{
			await PowerCmd.Remove(this);
		}

		// Token: 0x04002276 RID: 8822
		private const string _applierTag = "Applier";
	}
}
