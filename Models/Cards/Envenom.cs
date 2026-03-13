using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000939 RID: 2361
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Envenom : CardModel
	{
		// Token: 0x06006A49 RID: 27209 RVA: 0x0025AC00 File Offset: 0x00258E00
		public Envenom()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001C1B RID: 7195
		// (get) Token: 0x06006A4A RID: 27210 RVA: 0x0025AC0D File Offset: 0x00258E0D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<EnvenomPower>(1m));
			}
		}

		// Token: 0x17001C1C RID: 7196
		// (get) Token: 0x06006A4B RID: 27211 RVA: 0x0025AC1E File Offset: 0x00258E1E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x06006A4C RID: 27212 RVA: 0x0025AC2C File Offset: 0x00258E2C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<EnvenomPower>(base.Owner.Creature, base.DynamicVars["EnvenomPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006A4D RID: 27213 RVA: 0x0025AC6F File Offset: 0x00258E6F
		protected override void OnUpgrade()
		{
			base.DynamicVars["EnvenomPower"].UpgradeValueBy(1m);
		}
	}
}
