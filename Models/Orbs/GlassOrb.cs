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
	// Token: 0x0200072A RID: 1834
	[NullableContext(1)]
	[Nullable(0)]
	public class GlassOrb : OrbModel
	{
		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x060058E4 RID: 22756 RVA: 0x0022BCF7 File Offset: 0x00229EF7
		protected override string ChannelSfx
		{
			get
			{
				return "event:/sfx/characters/defect/defect_glass_channel";
			}
		}

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x060058E5 RID: 22757 RVA: 0x0022BCFE File Offset: 0x00229EFE
		public override Color DarkenedColor
		{
			get
			{
				return new Color("008585");
			}
		}

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x060058E6 RID: 22758 RVA: 0x0022BD0A File Offset: 0x00229F0A
		public override decimal PassiveVal
		{
			get
			{
				return base.ModifyOrbValue(this._passiveVal);
			}
		}

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x060058E7 RID: 22759 RVA: 0x0022BD18 File Offset: 0x00229F18
		public override decimal EvokeVal
		{
			get
			{
				return this.PassiveVal * 2m;
			}
		}

		// Token: 0x060058E8 RID: 22760 RVA: 0x0022BD2C File Offset: 0x00229F2C
		public override async Task BeforeTurnEndOrbTrigger(PlayerChoiceContext choiceContext)
		{
			await this.Passive(choiceContext, null);
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x0022BD78 File Offset: 0x00229F78
		public override async Task Passive(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			List<Creature> list = base.CombatState.HittableEnemies.Where((Creature e) => e.IsHittable).ToList<Creature>();
			decimal passiveVal = this.PassiveVal;
			if (!(passiveVal <= 0m))
			{
				base.Trigger();
				base.PlayPassiveSfx();
				this._passiveVal = Math.Max(0m, this._passiveVal - 1m);
				await CreatureCmd.Damage(choiceContext, list, passiveVal, ValueProp.Unpowered, base.Owner.Creature);
			}
		}

		// Token: 0x060058EA RID: 22762 RVA: 0x0022BDC4 File Offset: 0x00229FC4
		public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext playerChoiceContext)
		{
			List<Creature> enemies = base.CombatState.HittableEnemies.Where((Creature e) => e.IsHittable).ToList<Creature>();
			IEnumerable<Creature> enumerable;
			if (this.EvokeVal <= 0m)
			{
				enumerable = Array.Empty<Creature>();
			}
			else
			{
				await CreatureCmd.Damage(playerChoiceContext, enemies, this.EvokeVal, ValueProp.Unpowered, base.Owner.Creature);
				enumerable = enemies;
			}
			return enumerable;
		}

		// Token: 0x04002283 RID: 8835
		private decimal _passiveVal = 4m;
	}
}
