using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006F4 RID: 1780
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ExplosiveAmpoule : PotionModel
	{
		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06005792 RID: 22418 RVA: 0x0022A0F3 File Offset: 0x002282F3
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06005793 RID: 22419 RVA: 0x0022A0F6 File Offset: 0x002282F6
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06005794 RID: 22420 RVA: 0x0022A0F9 File Offset: 0x002282F9
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AllEnemies;
			}
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06005795 RID: 22421 RVA: 0x0022A0FC File Offset: 0x002282FC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(10m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06005796 RID: 22422 RVA: 0x0022A110 File Offset: 0x00228310
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			Creature player = base.Owner.Creature;
			DamageVar damage = base.DynamicVars.Damage;
			IReadOnlyList<Creature> targets = player.CombatState.HittableEnemies;
			foreach (Creature creature in targets)
			{
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance != null)
				{
					instance.CombatVfxContainer.AddChildSafely(NFireSmokePuffVfx.Create(creature));
				}
			}
			await Cmd.CustomScaledWait(0.2f, 0.3f, false, default(CancellationToken));
			await CreatureCmd.Damage(choiceContext, targets, damage.BaseValue, damage.Props, player, null);
		}
	}
}
