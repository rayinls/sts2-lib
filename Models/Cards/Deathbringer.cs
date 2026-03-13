using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000907 RID: 2311
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Deathbringer : CardModel
	{
		// Token: 0x06006938 RID: 26936 RVA: 0x00258EAB File Offset: 0x002570AB
		public Deathbringer()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001B99 RID: 7065
		// (get) Token: 0x06006939 RID: 26937 RVA: 0x00258EB8 File Offset: 0x002570B8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<DoomPower>(21m),
					new PowerVar<WeakPower>(1m)
				});
			}
		}

		// Token: 0x17001B9A RID: 7066
		// (get) Token: 0x0600693A RID: 26938 RVA: 0x00258EE1 File Offset: 0x002570E1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<DoomPower>(),
					HoverTipFactory.FromPower<WeakPower>()
				});
			}
		}

		// Token: 0x0600693B RID: 26939 RVA: 0x00258F00 File Offset: 0x00257100
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);
			await PowerCmd.Apply<DoomPower>(base.CombatState.HittableEnemies, base.DynamicVars.Doom.BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<WeakPower>(base.CombatState.HittableEnemies, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600693C RID: 26940 RVA: 0x00258F43 File Offset: 0x00257143
		protected override void OnUpgrade()
		{
			base.DynamicVars.Doom.UpgradeValueBy(5m);
		}
	}
}
