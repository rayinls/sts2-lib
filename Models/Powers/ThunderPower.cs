using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006C5 RID: 1733
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ThunderPower : PowerModel
	{
		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x0600567F RID: 22143 RVA: 0x002288BA File Offset: 0x00226ABA
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06005680 RID: 22144 RVA: 0x002288BD File Offset: 0x00226ABD
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06005681 RID: 22145 RVA: 0x002288C0 File Offset: 0x00226AC0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Evoke, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<LightningOrb>()
				});
			}
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x002288E4 File Offset: 0x00226AE4
		public override async Task AfterOrbEvoked(PlayerChoiceContext choiceContext, OrbModel orb, IEnumerable<Creature> targets)
		{
			if (orb.Owner == base.Owner.Player)
			{
				if (orb is LightningOrb)
				{
					List<Creature> livingTargets = targets.Where((Creature c) => c.IsAlive).ToList<Creature>();
					base.Flash();
					SfxCmd.Play("slash_attack.mp3", 1f);
					VfxCmd.PlayOnCreatureCenters(livingTargets, "vfx/vfx_attack_slash");
					await CreatureCmd.TriggerAnim(orb.Owner.Creature, "Attack", base.Owner.Player.Character.AttackAnimDelay);
					await CreatureCmd.Damage(choiceContext, livingTargets, base.Amount, ValueProp.Unpowered, base.Owner, null);
				}
			}
		}
	}
}
