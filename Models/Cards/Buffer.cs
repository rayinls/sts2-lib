using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008C4 RID: 2244
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Buffer : CardModel
	{
		// Token: 0x060067F1 RID: 26609 RVA: 0x002566B3 File Offset: 0x002548B3
		public Buffer()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B16 RID: 6934
		// (get) Token: 0x060067F2 RID: 26610 RVA: 0x002566C0 File Offset: 0x002548C0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<BufferPower>(1m));
			}
		}

		// Token: 0x060067F3 RID: 26611 RVA: 0x002566D4 File Offset: 0x002548D4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<BufferPower>(base.Owner.Creature, base.DynamicVars["BufferPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060067F4 RID: 26612 RVA: 0x00256717 File Offset: 0x00254917
		protected override void OnUpgrade()
		{
			base.DynamicVars["BufferPower"].UpgradeValueBy(1m);
		}
	}
}
