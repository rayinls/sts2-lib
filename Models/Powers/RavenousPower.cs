using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200067D RID: 1661
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RavenousPower : PowerModel
	{
		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x060054B4 RID: 21684 RVA: 0x00225093 File Offset: 0x00223293
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x060054B5 RID: 21685 RVA: 0x00225096 File Offset: 0x00223296
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x060054B6 RID: 21686 RVA: 0x00225099 File Offset: 0x00223299
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrengthPower>(),
					HoverTipFactory.Static(StaticHoverTip.Stun, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x002250BC File Offset: 0x002232BC
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature target, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (target != base.Owner)
				{
					if (target.Side == base.Owner.Side)
					{
						if (!base.Owner.IsDead)
						{
							base.Flash();
							SfxCmd.Play("event:/sfx/enemy/enemy_attacks/corpse_slugs/corpse_slugs_ravenous", 1f);
							await CreatureCmd.TriggerAnim(base.Owner, "DevourStartTrigger", 0.5f);
							((CorpseSlug)base.Owner.Monster).IsRavenous = true;
							await CreatureCmd.Stun(base.Owner, new Func<IReadOnlyList<Creature>, Task>(this.StunnedMove), null);
							await PowerCmd.Apply<StrengthPower>(base.Owner, base.Amount, base.Owner, null, false);
						}
					}
				}
			}
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x00225110 File Offset: 0x00223310
		private async Task StunnedMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/corpse_slugs/corpse_slugs_ravenous_up_double", 1f);
			await CreatureCmd.TriggerAnim(base.Owner, "DevourEndkTrigger", 0.5f);
			((CorpseSlug)base.Owner.Monster).IsRavenous = false;
		}
	}
}
