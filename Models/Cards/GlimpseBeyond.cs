using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200096F RID: 2415
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GlimpseBeyond : CardModel
	{
		// Token: 0x06006B7C RID: 27516 RVA: 0x0025D34A File Offset: 0x0025B54A
		public GlimpseBeyond()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.AllAllies, true)
		{
		}

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x06006B7D RID: 27517 RVA: 0x0025D357 File Offset: 0x0025B557
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x06006B7E RID: 27518 RVA: 0x0025D35A File Offset: 0x0025B55A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x06006B7F RID: 27519 RVA: 0x0025D367 File Offset: 0x0025B567
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Soul>(false));
			}
		}

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x06006B80 RID: 27520 RVA: 0x0025D374 File Offset: 0x0025B574
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006B81 RID: 27521 RVA: 0x0025D37C File Offset: 0x0025B57C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<Creature> enumerable = from c in base.CombatState.GetTeammatesOf(base.Owner.Creature)
				where c != null && c.IsAlive && c.IsPlayer
				select c;
			foreach (Creature creature in enumerable)
			{
				List<Soul> list = Soul.Create(creature.Player, base.DynamicVars.Cards.IntValue, base.CombatState).ToList<Soul>();
				IReadOnlyList<CardPileAddResult> readOnlyList = await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Draw, true, CardPilePosition.Random);
				IReadOnlyList<CardPileAddResult> readOnlyList2 = readOnlyList;
				if (LocalContext.IsMe(creature))
				{
					CardCmd.PreviewCardPileAdd(readOnlyList2, 1.2f, CardPreviewStyle.HorizontalLayout);
				}
				creature = null;
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06006B82 RID: 27522 RVA: 0x0025D3BF File Offset: 0x0025B5BF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
