using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A41 RID: 2625
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SevenStars : CardModel
	{
		// Token: 0x06006FD4 RID: 28628 RVA: 0x00265FD0 File Offset: 0x002641D0
		public SevenStars()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001E67 RID: 7783
		// (get) Token: 0x06006FD5 RID: 28629 RVA: 0x00265FDD File Offset: 0x002641DD
		public override int CanonicalStarCost
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x17001E68 RID: 7784
		// (get) Token: 0x06006FD6 RID: 28630 RVA: 0x00265FE0 File Offset: 0x002641E0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(7m, ValueProp.Move),
					new RepeatVar(7)
				});
			}
		}

		// Token: 0x06006FD7 RID: 28631 RVA: 0x00266008 File Offset: 0x00264208
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.DynamicVars.Repeat.IntValue).FromCard(this)
				.TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_starry_impact", null, "slash_attack.mp3")
				.SpawningHitVfxOnEachCreature()
				.Execute(choiceContext);
		}

		// Token: 0x06006FD8 RID: 28632 RVA: 0x00266053 File Offset: 0x00264253
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
