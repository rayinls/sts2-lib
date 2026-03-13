using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200098F RID: 2447
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HiddenGem : CardModel
	{
		// Token: 0x06006C1B RID: 27675 RVA: 0x0025E7FF File Offset: 0x0025C9FF
		public HiddenGem()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001CDE RID: 7390
		// (get) Token: 0x06006C1C RID: 27676 RVA: 0x0025E80C File Offset: 0x0025CA0C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new IntVar("Replay", 2m));
			}
		}

		// Token: 0x17001CDF RID: 7391
		// (get) Token: 0x06006C1D RID: 27677 RVA: 0x0025E823 File Offset: 0x0025CA23
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.ReplayStatic, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06006C1E RID: 27678 RVA: 0x0025E838 File Offset: 0x0025CA38
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			List<CardModel> list = PileType.Draw.GetPile(base.Owner).Cards.ToList<CardModel>();
			if (list.Count != 0)
			{
				List<CardModel> list2 = list.Where(delegate(CardModel c)
				{
					bool flag = !c.Keywords.Contains(CardKeyword.Unplayable);
					bool flag2 = flag;
					if (flag2)
					{
						CardType type = c.Type;
						bool flag3 = type - CardType.Curse <= 1;
						flag2 = !flag3;
					}
					return flag2;
				}).ToList<CardModel>();
				List<CardModel> list3 = list2.Where(delegate(CardModel c)
				{
					CardType type2 = c.Type;
					return type2 - CardType.Attack <= 2;
				}).ToList<CardModel>();
				IEnumerable<CardModel> enumerable;
				if (list3.Count != 0)
				{
					enumerable = list3;
				}
				else
				{
					enumerable = list2;
				}
				CardModel cardModel = base.Owner.RunState.Rng.CombatCardSelection.NextItem<CardModel>(enumerable);
				if (cardModel != null)
				{
					cardModel.BaseReplayCount += base.DynamicVars["Replay"].IntValue;
					CardCmd.Preview(cardModel, 1.2f, CardPreviewStyle.HorizontalLayout);
				}
			}
		}

		// Token: 0x06006C1F RID: 27679 RVA: 0x0025E87B File Offset: 0x0025CA7B
		protected override void OnUpgrade()
		{
			base.DynamicVars["Replay"].UpgradeValueBy(1m);
		}

		// Token: 0x04002590 RID: 9616
		private const string _replayKey = "Replay";
	}
}
