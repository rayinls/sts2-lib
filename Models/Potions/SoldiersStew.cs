using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x02000716 RID: 1814
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SoldiersStew : PotionModel
	{
		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x06005874 RID: 22644 RVA: 0x0022B213 File Offset: 0x00229413
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x06005875 RID: 22645 RVA: 0x0022B216 File Offset: 0x00229416
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06005876 RID: 22646 RVA: 0x0022B219 File Offset: 0x00229419
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06005877 RID: 22647 RVA: 0x0022B21C File Offset: 0x0022941C
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.ReplayStatic, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x0022B230 File Offset: 0x00229430
		protected override Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, new Color("e6a045"));
			}
			List<CardModel> list = target.Player.PlayerCombatState.AllCards.Where((CardModel c) => c.Tags.Contains(CardTag.Strike)).ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				CardModel cardModel2 = cardModel;
				int baseReplayCount = cardModel2.BaseReplayCount;
				cardModel2.BaseReplayCount = baseReplayCount + 1;
			}
			return Task.CompletedTask;
		}
	}
}
