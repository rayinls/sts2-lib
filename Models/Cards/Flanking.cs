using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000954 RID: 2388
	public sealed class Flanking : CardModel
	{
		// Token: 0x06006AE0 RID: 27360 RVA: 0x0025BFD6 File Offset: 0x0025A1D6
		public Flanking()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C5D RID: 7261
		// (get) Token: 0x06006AE1 RID: 27361 RVA: 0x0025BFE3 File Offset: 0x0025A1E3
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x06006AE2 RID: 27362 RVA: 0x0025BFE8 File Offset: 0x0025A1E8
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<FlankingPower>(cardPlay.Target, 2m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006AE3 RID: 27363 RVA: 0x0025C033 File Offset: 0x0025A233
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
