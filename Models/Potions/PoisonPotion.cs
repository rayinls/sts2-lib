using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x02000708 RID: 1800
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PoisonPotion : PotionModel
	{
		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06005816 RID: 22550 RVA: 0x0022AB3F File Offset: 0x00228D3F
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06005817 RID: 22551 RVA: 0x0022AB42 File Offset: 0x00228D42
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06005818 RID: 22552 RVA: 0x0022AB45 File Offset: 0x00228D45
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyEnemy;
			}
		}

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06005819 RID: 22553 RVA: 0x0022AB48 File Offset: 0x00228D48
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PoisonPower>(6m));
			}
		}

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x0600581A RID: 22554 RVA: 0x0022AB5A File Offset: 0x00228D5A
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x0022AB68 File Offset: 0x00228D68
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(target) : null);
			if (ncreature != null)
			{
				NGaseousImpactVfx ngaseousImpactVfx = NGaseousImpactVfx.Create(ncreature.VfxSpawnPosition, new Color("83eb85"));
				NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(ngaseousImpactVfx);
			}
			await PowerCmd.Apply<PoisonPower>(target, base.DynamicVars.Poison.BaseValue, base.Owner.Creature, null, false);
		}
	}
}
