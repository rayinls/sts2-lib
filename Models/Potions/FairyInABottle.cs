using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006F5 RID: 1781
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FairyInABottle : PotionModel
	{
		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06005798 RID: 22424 RVA: 0x0022A163 File Offset: 0x00228363
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06005799 RID: 22425 RVA: 0x0022A166 File Offset: 0x00228366
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.Automatic;
			}
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x0600579A RID: 22426 RVA: 0x0022A169 File Offset: 0x00228369
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x0600579B RID: 22427 RVA: 0x0022A16C File Offset: 0x0022836C
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600579C RID: 22428 RVA: 0x0022A170 File Offset: 0x00228370
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await CreatureCmd.Heal(target, target.MaxHp * 0.3m, true);
		}

		// Token: 0x0600579D RID: 22429 RVA: 0x0022A1B3 File Offset: 0x002283B3
		public override bool ShouldDie(Creature creature)
		{
			return creature != base.Owner.Creature;
		}

		// Token: 0x0600579E RID: 22430 RVA: 0x0022A1C8 File Offset: 0x002283C8
		public override async Task AfterPreventingDeath(Creature creature)
		{
			await base.OnUseWrapper(new ThrowingPlayerChoiceContext(), creature);
		}
	}
}
