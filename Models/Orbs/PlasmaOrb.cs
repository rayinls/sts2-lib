using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Orbs
{
	// Token: 0x0200072C RID: 1836
	[NullableContext(1)]
	[Nullable(0)]
	public class PlasmaOrb : OrbModel
	{
		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x060058F7 RID: 22775 RVA: 0x0022BFAF File Offset: 0x0022A1AF
		protected override string ChannelSfx
		{
			get
			{
				return "event:/sfx/characters/defect/defect_plasma_channel";
			}
		}

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x060058F8 RID: 22776 RVA: 0x0022BFB6 File Offset: 0x0022A1B6
		public override Color DarkenedColor
		{
			get
			{
				return new Color("008585");
			}
		}

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x060058F9 RID: 22777 RVA: 0x0022BFC2 File Offset: 0x0022A1C2
		public override decimal PassiveVal
		{
			get
			{
				return 1m;
			}
		}

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x060058FA RID: 22778 RVA: 0x0022BFC9 File Offset: 0x0022A1C9
		public override decimal EvokeVal
		{
			get
			{
				return 2m;
			}
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x0022BFD4 File Offset: 0x0022A1D4
		public override async Task AfterTurnStartOrbTrigger(PlayerChoiceContext choiceContext)
		{
			await this.Passive(choiceContext, null);
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x0022C020 File Offset: 0x0022A220
		public override async Task Passive(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			if (target != null)
			{
				throw new InvalidOperationException("Plasma orbs cannot target creatures.");
			}
			base.Trigger();
			await PlayerCmd.GainEnergy(this.PassiveVal, base.Owner);
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x0022C06C File Offset: 0x0022A26C
		public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext playerChoiceContext)
		{
			base.PlayEvokeSfx();
			await PlayerCmd.GainEnergy(this.EvokeVal, base.Owner);
			return new <>z__ReadOnlySingleElementList<Creature>(base.Owner.Creature);
		}
	}
}
