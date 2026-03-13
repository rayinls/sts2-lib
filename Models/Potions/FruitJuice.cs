using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006FB RID: 1787
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FruitJuice : PotionModel
	{
		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x060057C2 RID: 22466 RVA: 0x0022A52F File Offset: 0x0022872F
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x060057C3 RID: 22467 RVA: 0x0022A532 File Offset: 0x00228732
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.AnyTime;
			}
		}

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x060057C4 RID: 22468 RVA: 0x0022A535 File Offset: 0x00228735
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x060057C5 RID: 22469 RVA: 0x0022A538 File Offset: 0x00228738
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x060057C6 RID: 22470 RVA: 0x0022A53B File Offset: 0x0022873B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(5m));
			}
		}

		// Token: 0x060057C7 RID: 22471 RVA: 0x0022A550 File Offset: 0x00228750
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await CreatureCmd.GainMaxHp(target, base.DynamicVars.MaxHp.BaseValue);
		}
	}
}
