using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000960 RID: 2400
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FranticEscape : CardModel
	{
		// Token: 0x06006B25 RID: 27429 RVA: 0x0025C856 File Offset: 0x0025AA56
		public FranticEscape()
			: base(1, CardType.Status, CardRarity.Status, TargetType.Self, true)
		{
		}

		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x06006B26 RID: 27430 RVA: 0x0025C864 File Offset: 0x0025AA64
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				Creature sandpitEnemy = this.GetSandpitEnemy();
				SandpitPower sandpitPower = ((sandpitEnemy != null) ? sandpitEnemy.GetPower<SandpitPower>() : null);
				if (sandpitPower == null)
				{
					return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<SandpitPower>());
				}
				return sandpitPower.HoverTips;
			}
		}

		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x06006B27 RID: 27431 RVA: 0x0025C898 File Offset: 0x0025AA98
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x06006B28 RID: 27432 RVA: 0x0025C89B File Offset: 0x0025AA9B
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006B29 RID: 27433 RVA: 0x0025C8A0 File Offset: 0x0025AAA0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			Creature sandpitEnemy = this.GetSandpitEnemy();
			if (sandpitEnemy != null)
			{
				SandpitPower sandpitPower = sandpitEnemy.Powers.OfType<SandpitPower>().FirstOrDefault((SandpitPower s) => s.Target == base.Owner.Creature);
				if (sandpitPower != null)
				{
					await PowerCmd.ModifyAmount(sandpitPower, 1m, sandpitEnemy, this, false);
				}
			}
			base.EnergyCost.AddThisCombat(1, false);
		}

		// Token: 0x06006B2A RID: 27434 RVA: 0x0025C8E3 File Offset: 0x0025AAE3
		[NullableContext(2)]
		private Creature GetSandpitEnemy()
		{
			CombatState combatState = base.CombatState;
			if (combatState == null)
			{
				return null;
			}
			return combatState.Enemies.FirstOrDefault((Creature c) => c.HasPower<SandpitPower>());
		}
	}
}
