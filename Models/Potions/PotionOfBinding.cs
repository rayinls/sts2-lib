using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x02000709 RID: 1801
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PotionOfBinding : PotionModel
	{
		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x0600581D RID: 22557 RVA: 0x0022ABBB File Offset: 0x00228DBB
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x0600581E RID: 22558 RVA: 0x0022ABBE File Offset: 0x00228DBE
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x0600581F RID: 22559 RVA: 0x0022ABC1 File Offset: 0x00228DC1
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AllEnemies;
			}
		}

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x06005820 RID: 22560 RVA: 0x0022ABC4 File Offset: 0x00228DC4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<VulnerablePower>(1m),
					new PowerVar<WeakPower>(1m)
				});
			}
		}

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x06005821 RID: 22561 RVA: 0x0022ABEB File Offset: 0x00228DEB
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<WeakPower>(),
					HoverTipFactory.FromPower<VulnerablePower>()
				});
			}
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x0022AC08 File Offset: 0x00228E08
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			IReadOnlyList<Creature> targets = base.Owner.Creature.CombatState.HittableEnemies;
			foreach (Creature creature in targets)
			{
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance != null)
				{
					instance.CombatVfxContainer.AddChildSafely(NSmokePuffVfx.Create(creature, NSmokePuffVfx.SmokePuffColor.Green));
				}
			}
			await PowerCmd.Apply<WeakPower>(targets, base.DynamicVars["VulnerablePower"].IntValue, base.Owner.Creature, null, false);
			await PowerCmd.Apply<VulnerablePower>(targets, base.DynamicVars["WeakPower"].IntValue, base.Owner.Creature, null, false);
		}
	}
}
