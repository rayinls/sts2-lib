using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A8B RID: 2699
	public sealed class Tank : CardModel
	{
		// Token: 0x0600716D RID: 29037 RVA: 0x00269317 File Offset: 0x00267517
		public Tank()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001F0E RID: 7950
		// (get) Token: 0x0600716E RID: 29038 RVA: 0x00269324 File Offset: 0x00267524
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x0600716F RID: 29039 RVA: 0x00269328 File Offset: 0x00267528
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<TankPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06007170 RID: 29040 RVA: 0x0026936B File Offset: 0x0026756B
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
