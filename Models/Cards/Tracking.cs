using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AA0 RID: 2720
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Tracking : CardModel
	{
		// Token: 0x060071E1 RID: 29153 RVA: 0x0026A0CF File Offset: 0x002682CF
		public Tracking()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001F3C RID: 7996
		// (get) Token: 0x060071E2 RID: 29154 RVA: 0x0026A0DC File Offset: 0x002682DC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x060071E3 RID: 29155 RVA: 0x0026A0E8 File Offset: 0x002682E8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			if (base.Owner.Creature.HasPower<TrackingPower>())
			{
				await PowerCmd.Apply<TrackingPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
			}
			else
			{
				await PowerCmd.Apply<TrackingPower>(base.Owner.Creature, 2m, base.Owner.Creature, this, false);
			}
		}

		// Token: 0x060071E4 RID: 29156 RVA: 0x0026A12B File Offset: 0x0026832B
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
