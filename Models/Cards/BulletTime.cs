using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008C6 RID: 2246
	public sealed class BulletTime : CardModel
	{
		// Token: 0x060067FA RID: 26618 RVA: 0x00256807 File Offset: 0x00254A07
		public BulletTime()
			: base(3, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x060067FB RID: 26619 RVA: 0x00256814 File Offset: 0x00254A14
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			foreach (CardModel cardModel in PileType.Hand.GetPile(base.Owner).Cards)
			{
				if (!cardModel.EnergyCost.CostsX)
				{
					cardModel.SetToFreeThisTurn();
				}
			}
			await PowerCmd.Apply<NoDrawPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x060067FC RID: 26620 RVA: 0x00256857 File Offset: 0x00254A57
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
