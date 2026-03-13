using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Orbs
{
	// Token: 0x02000729 RID: 1833
	[NullableContext(1)]
	[Nullable(0)]
	public class FrostOrb : OrbModel
	{
		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x060058DC RID: 22748 RVA: 0x0022BBE3 File Offset: 0x00229DE3
		protected override string ChannelSfx
		{
			get
			{
				return "event:/sfx/characters/defect/defect_frost_channel";
			}
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x060058DD RID: 22749 RVA: 0x0022BBEA File Offset: 0x00229DEA
		public override Color DarkenedColor
		{
			get
			{
				return new Color("7860a7");
			}
		}

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x060058DE RID: 22750 RVA: 0x0022BBF6 File Offset: 0x00229DF6
		public override decimal PassiveVal
		{
			get
			{
				return base.ModifyOrbValue(2m);
			}
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x060058DF RID: 22751 RVA: 0x0022BC04 File Offset: 0x00229E04
		public override decimal EvokeVal
		{
			get
			{
				return base.ModifyOrbValue(5m);
			}
		}

		// Token: 0x060058E0 RID: 22752 RVA: 0x0022BC14 File Offset: 0x00229E14
		public override async Task BeforeTurnEndOrbTrigger(PlayerChoiceContext choiceContext)
		{
			await this.Passive(choiceContext, null);
		}

		// Token: 0x060058E1 RID: 22753 RVA: 0x0022BC60 File Offset: 0x00229E60
		public override async Task Passive(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			if (target != null)
			{
				throw new InvalidOperationException("Frost orbs cannot target creatures.");
			}
			base.Trigger();
			base.PlayPassiveSfx();
			await CreatureCmd.GainBlock(base.Owner.Creature, this.PassiveVal, ValueProp.Unpowered, null, false);
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x0022BCAC File Offset: 0x00229EAC
		public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext playerChoiceContext)
		{
			base.PlayEvokeSfx();
			await CreatureCmd.GainBlock(base.Owner.Creature, this.EvokeVal, ValueProp.Unpowered, null, false);
			return new <>z__ReadOnlySingleElementList<Creature>(base.Owner.Creature);
		}
	}
}
