using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006E2 RID: 1762
	public sealed class BlessingOfTheForge : PotionModel
	{
		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06005727 RID: 22311 RVA: 0x0022991B File Offset: 0x00227B1B
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06005728 RID: 22312 RVA: 0x0022991E File Offset: 0x00227B1E
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06005729 RID: 22313 RVA: 0x00229921 File Offset: 0x00227B21
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x0600572A RID: 22314 RVA: 0x00229924 File Offset: 0x00227B24
		[NullableContext(1)]
		protected override Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(base.Owner.Creature, new Color("e06e58"));
			}
			foreach (CardModel cardModel in PileType.Hand.GetPile(base.Owner).Cards)
			{
				if (cardModel.IsUpgradable)
				{
					CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
				}
			}
			return Task.CompletedTask;
		}
	}
}
