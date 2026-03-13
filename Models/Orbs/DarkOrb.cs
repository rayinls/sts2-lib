using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Orbs;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Orbs
{
	// Token: 0x02000728 RID: 1832
	[NullableContext(1)]
	[Nullable(0)]
	public class DarkOrb : OrbModel
	{
		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x060058D4 RID: 22740 RVA: 0x0022BA9E File Offset: 0x00229C9E
		protected override string ChannelSfx
		{
			get
			{
				return "event:/sfx/characters/defect/defect_dark_channel";
			}
		}

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x060058D5 RID: 22741 RVA: 0x0022BAA5 File Offset: 0x00229CA5
		public override Color DarkenedColor
		{
			get
			{
				return new Color("9001d3");
			}
		}

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x060058D6 RID: 22742 RVA: 0x0022BAB1 File Offset: 0x00229CB1
		public override decimal PassiveVal
		{
			get
			{
				return base.ModifyOrbValue(6m);
			}
		}

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x060058D7 RID: 22743 RVA: 0x0022BABF File Offset: 0x00229CBF
		public override decimal EvokeVal
		{
			get
			{
				return this._evokeVal;
			}
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x0022BAC8 File Offset: 0x00229CC8
		public override async Task BeforeTurnEndOrbTrigger(PlayerChoiceContext choiceContext)
		{
			await this.Passive(choiceContext, null);
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x0022BB14 File Offset: 0x00229D14
		public override Task Passive(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			if (target != null)
			{
				throw new InvalidOperationException("Dark orbs cannot target creatures.");
			}
			base.Trigger();
			this._evokeVal += this.PassiveVal;
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				NCreature creatureNode = instance.GetCreatureNode(base.Owner.Creature);
				if (creatureNode != null)
				{
					NOrbManager orbManager = creatureNode.OrbManager;
					if (orbManager != null)
					{
						orbManager.UpdateVisuals(OrbEvokeType.None);
					}
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x0022BB84 File Offset: 0x00229D84
		public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext playerChoiceContext)
		{
			IReadOnlyList<Creature> hittableEnemies = base.CombatState.HittableEnemies;
			IEnumerable<Creature> enumerable;
			if (hittableEnemies.Count == 0)
			{
				enumerable = Array.Empty<Creature>();
			}
			else
			{
				base.PlayEvokeSfx();
				Creature weakestEnemy = hittableEnemies.MinBy((Creature c) => c.CurrentHp);
				await CreatureCmd.Damage(playerChoiceContext, weakestEnemy, this.EvokeVal, ValueProp.Unpowered, base.Owner.Creature);
				enumerable = new <>z__ReadOnlySingleElementList<Creature>(weakestEnemy);
			}
			return enumerable;
		}

		// Token: 0x04002282 RID: 8834
		private decimal _evokeVal = 6m;
	}
}
