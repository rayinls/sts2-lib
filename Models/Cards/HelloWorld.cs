using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200098A RID: 2442
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HelloWorld : CardModel
	{
		// Token: 0x06006C06 RID: 27654 RVA: 0x0025E52B File Offset: 0x0025C72B
		public HelloWorld()
			: base(1, CardType.Power, CardRarity.Event, TargetType.Self, true)
		{
		}

		// Token: 0x17001CD8 RID: 7384
		// (get) Token: 0x06006C07 RID: 27655 RVA: 0x0025E538 File Offset: 0x0025C738
		public override CardPoolModel VisualCardPool
		{
			get
			{
				return ModelDb.CardPool<DefectCardPool>();
			}
		}

		// Token: 0x06006C08 RID: 27656 RVA: 0x0025E540 File Offset: 0x0025C740
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<HelloWorldPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006C09 RID: 27657 RVA: 0x0025E583 File Offset: 0x0025C783
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Innate);
		}
	}
}
