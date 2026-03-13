using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Orbs
{
	// Token: 0x0200072B RID: 1835
	[NullableContext(1)]
	[Nullable(0)]
	public class LightningOrb : OrbModel
	{
		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x060058EC RID: 22764 RVA: 0x0022BE23 File Offset: 0x0022A023
		protected override string PassiveSfx
		{
			get
			{
				return "event:/sfx/characters/defect/defect_lightning_passive";
			}
		}

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x060058ED RID: 22765 RVA: 0x0022BE2A File Offset: 0x0022A02A
		protected override string EvokeSfx
		{
			get
			{
				return "event:/sfx/characters/defect/defect_lightning_evoke";
			}
		}

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x060058EE RID: 22766 RVA: 0x0022BE31 File Offset: 0x0022A031
		protected override string ChannelSfx
		{
			get
			{
				return "event:/sfx/characters/defect/defect_lightning_channel";
			}
		}

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x060058EF RID: 22767 RVA: 0x0022BE38 File Offset: 0x0022A038
		public override Color DarkenedColor
		{
			get
			{
				return new Color("796606");
			}
		}

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x060058F0 RID: 22768 RVA: 0x0022BE44 File Offset: 0x0022A044
		public override decimal PassiveVal
		{
			get
			{
				return base.ModifyOrbValue(3m);
			}
		}

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x060058F1 RID: 22769 RVA: 0x0022BE52 File Offset: 0x0022A052
		public override decimal EvokeVal
		{
			get
			{
				return base.ModifyOrbValue(8m);
			}
		}

		// Token: 0x060058F2 RID: 22770 RVA: 0x0022BE60 File Offset: 0x0022A060
		public override async Task BeforeTurnEndOrbTrigger(PlayerChoiceContext choiceContext)
		{
			await this.Passive(choiceContext, null);
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x0022BEAC File Offset: 0x0022A0AC
		public override async Task Passive(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			base.Trigger();
			await this.ApplyLightningDamage(this.PassiveVal, target, choiceContext);
		}

		// Token: 0x060058F4 RID: 22772 RVA: 0x0022BF00 File Offset: 0x0022A100
		public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext playerChoiceContext)
		{
			return await this.ApplyLightningDamage(this.EvokeVal, null, playerChoiceContext);
		}

		// Token: 0x060058F5 RID: 22773 RVA: 0x0022BF4C File Offset: 0x0022A14C
		private async Task<IEnumerable<Creature>> ApplyLightningDamage(decimal value, [Nullable(2)] Creature target, PlayerChoiceContext choiceContext)
		{
			List<Creature> list = (from e in base.CombatState.GetOpponentsOf(base.Owner.Creature)
				where e.IsHittable
				select e).ToList<Creature>();
			IEnumerable<Creature> enumerable;
			if (list.Count == 0)
			{
				enumerable = Array.Empty<Creature>();
			}
			else
			{
				IReadOnlyList<Creature> targets;
				if (target != null)
				{
					targets = new <>z__ReadOnlySingleElementList<Creature>(target);
				}
				else
				{
					targets = new <>z__ReadOnlySingleElementList<Creature>(base.Owner.RunState.Rng.CombatTargets.NextItem<Creature>(list));
				}
				foreach (Creature creature in targets)
				{
					VfxCmd.PlayOnCreature(creature, "vfx/vfx_attack_lightning");
				}
				base.PlayEvokeSfx();
				await CreatureCmd.Damage(choiceContext, targets, value, ValueProp.Unpowered, base.Owner.Creature);
				enumerable = targets;
			}
			return enumerable;
		}
	}
}
