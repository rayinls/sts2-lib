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
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006E5 RID: 1765
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BoneBrew : PotionModel
	{
		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x06005739 RID: 22329 RVA: 0x00229AAF File Offset: 0x00227CAF
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x0600573A RID: 22330 RVA: 0x00229AB2 File Offset: 0x00227CB2
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x0600573B RID: 22331 RVA: 0x00229AB5 File Offset: 0x00227CB5
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x0600573C RID: 22332 RVA: 0x00229AB8 File Offset: 0x00227CB8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new SummonVar(15m));
			}
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x0600573D RID: 22333 RVA: 0x00229ACB File Offset: 0x00227CCB
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new DynamicVar[] { base.DynamicVars.Summon }));
			}
		}

		// Token: 0x0600573E RID: 22334 RVA: 0x00229AF0 File Offset: 0x00227CF0
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(base.Owner.Creature, new Color("6876bd"));
			}
			await OstyCmd.Summon(choiceContext, base.Owner, base.DynamicVars.Summon.BaseValue, this);
		}
	}
}
