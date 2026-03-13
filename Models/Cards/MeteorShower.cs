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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009C8 RID: 2504
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MeteorShower : CardModel
	{
		// Token: 0x06006D59 RID: 27993 RVA: 0x002610E3 File Offset: 0x0025F2E3
		public MeteorShower()
			: base(0, CardType.Attack, CardRarity.Ancient, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001D5E RID: 7518
		// (get) Token: 0x06006D5A RID: 27994 RVA: 0x002610F0 File Offset: 0x0025F2F0
		public override int CanonicalStarCost
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001D5F RID: 7519
		// (get) Token: 0x06006D5B RID: 27995 RVA: 0x002610F3 File Offset: 0x0025F2F3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(14m, ValueProp.Move),
					new PowerVar<VulnerablePower>(2m),
					new PowerVar<WeakPower>(2m)
				});
			}
		}

		// Token: 0x17001D60 RID: 7520
		// (get) Token: 0x06006D5C RID: 27996 RVA: 0x0026112C File Offset: 0x0025F32C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<WeakPower>(),
					HoverTipFactory.FromPower<VulnerablePower>()
				});
			}
		}

		// Token: 0x06006D5D RID: 27997 RVA: 0x0026114C File Offset: 0x0025F34C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			await PowerCmd.Apply<WeakPower>(base.CombatState.HittableEnemies, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<VulnerablePower>(base.CombatState.HittableEnemies, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006D5E RID: 27998 RVA: 0x00261197 File Offset: 0x0025F397
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(7m);
		}
	}
}
