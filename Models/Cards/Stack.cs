using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A6C RID: 2668
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Stack : CardModel
	{
		// Token: 0x060070D3 RID: 28883 RVA: 0x00267F4C File Offset: 0x0026614C
		public Stack()
			: base(1, CardType.Skill, CardRarity.Event, TargetType.Self, true)
		{
		}

		// Token: 0x17001ED5 RID: 7893
		// (get) Token: 0x060070D4 RID: 28884 RVA: 0x00267F59 File Offset: 0x00266159
		public override CardPoolModel VisualCardPool
		{
			get
			{
				return ModelDb.CardPool<DefectCardPool>();
			}
		}

		// Token: 0x17001ED6 RID: 7894
		// (get) Token: 0x060070D5 RID: 28885 RVA: 0x00267F60 File Offset: 0x00266160
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001ED7 RID: 7895
		// (get) Token: 0x060070D6 RID: 28886 RVA: 0x00267F64 File Offset: 0x00266164
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(0m);
				array[1] = new CalculationExtraVar(1m);
				array[2] = new CalculatedBlockVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature _) => PileType.Discard.GetPile(card.Owner).Cards.Count<CardModel>());
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x060070D7 RID: 28887 RVA: 0x00267FC4 File Offset: 0x002661C4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.CalculatedBlock.Calculate(cardPlay.Target), base.DynamicVars.CalculatedBlock.Props, cardPlay, false);
		}

		// Token: 0x060070D8 RID: 28888 RVA: 0x0026800F File Offset: 0x0026620F
		protected override void OnUpgrade()
		{
			base.DynamicVars.CalculationBase.UpgradeValueBy(3m);
		}
	}
}
