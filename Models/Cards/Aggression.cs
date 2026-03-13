using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200088C RID: 2188
	public sealed class Aggression : CardModel
	{
		// Token: 0x060066CF RID: 26319 RVA: 0x0025421B File Offset: 0x0025241B
		public Aggression()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x060066D0 RID: 26320 RVA: 0x00254228 File Offset: 0x00252428
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<AggressionPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x060066D1 RID: 26321 RVA: 0x0025426B File Offset: 0x0025246B
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Innate);
		}
	}
}
