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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000977 RID: 2423
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GraveWarden : CardModel
	{
		// Token: 0x06006BA5 RID: 27557 RVA: 0x0025D8F2 File Offset: 0x0025BAF2
		public GraveWarden()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x06006BA6 RID: 27558 RVA: 0x0025D8FF File Offset: 0x0025BAFF
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001CAF RID: 7343
		// (get) Token: 0x06006BA7 RID: 27559 RVA: 0x0025D902 File Offset: 0x0025BB02
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(8m, ValueProp.Move),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x17001CB0 RID: 7344
		// (get) Token: 0x06006BA8 RID: 27560 RVA: 0x0025D927 File Offset: 0x0025BB27
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Soul>(base.IsUpgraded));
			}
		}

		// Token: 0x06006BA9 RID: 27561 RVA: 0x0025D93C File Offset: 0x0025BB3C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			List<Soul> list = Soul.Create(base.Owner, base.DynamicVars.Cards.IntValue, base.CombatState).ToList<Soul>();
			if (base.IsUpgraded)
			{
				foreach (Soul soul in list)
				{
					CardCmd.Upgrade(soul, CardPreviewStyle.HorizontalLayout);
				}
			}
			IReadOnlyList<CardPileAddResult> readOnlyList = await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Draw, true, CardPilePosition.Random);
			CardCmd.PreviewCardPileAdd(readOnlyList, 1.2f, CardPreviewStyle.HorizontalLayout);
		}

		// Token: 0x06006BAA RID: 27562 RVA: 0x0025D987 File Offset: 0x0025BB87
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
		}
	}
}
