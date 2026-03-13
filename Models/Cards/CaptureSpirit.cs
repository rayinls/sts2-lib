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
	// Token: 0x020008D6 RID: 2262
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CaptureSpirit : CardModel
	{
		// Token: 0x06006843 RID: 26691 RVA: 0x00257026 File Offset: 0x00255226
		public CaptureSpirit()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B34 RID: 6964
		// (get) Token: 0x06006844 RID: 26692 RVA: 0x00257033 File Offset: 0x00255233
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(3m, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move),
					new CardsVar(3)
				});
			}
		}

		// Token: 0x17001B35 RID: 6965
		// (get) Token: 0x06006845 RID: 26693 RVA: 0x00257059 File Offset: 0x00255259
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Soul>(false));
			}
		}

		// Token: 0x06006846 RID: 26694 RVA: 0x00257068 File Offset: 0x00255268
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.Damage(choiceContext, cardPlay.Target, base.DynamicVars.Damage, this);
			List<Soul> list = Soul.Create(base.Owner, base.DynamicVars.Cards.IntValue, base.CombatState).ToList<Soul>();
			IReadOnlyList<CardPileAddResult> readOnlyList = await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Draw, true, CardPilePosition.Random);
			CardCmd.PreviewCardPileAdd(readOnlyList, 1.2f, CardPreviewStyle.HorizontalLayout);
		}

		// Token: 0x06006847 RID: 26695 RVA: 0x002570BB File Offset: 0x002552BB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
