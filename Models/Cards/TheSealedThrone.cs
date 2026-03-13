using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A95 RID: 2709
	public sealed class TheSealedThrone : CardModel
	{
		// Token: 0x060071A5 RID: 29093 RVA: 0x002699E2 File Offset: 0x00267BE2
		public TheSealedThrone()
			: base(1, CardType.Power, CardRarity.Ancient, TargetType.Self, true)
		{
		}

		// Token: 0x17001F24 RID: 7972
		// (get) Token: 0x060071A6 RID: 29094 RVA: 0x002699EF File Offset: 0x00267BEF
		public override int CanonicalStarCost
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060071A7 RID: 29095 RVA: 0x002699F4 File Offset: 0x00267BF4
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<TheSealedThronePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x060071A8 RID: 29096 RVA: 0x00269A37 File Offset: 0x00267C37
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Innate);
		}
	}
}
