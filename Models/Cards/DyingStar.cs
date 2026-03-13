using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200092E RID: 2350
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DyingStar : CardModel
	{
		// Token: 0x06006A0F RID: 27151 RVA: 0x0025A5EA File Offset: 0x002587EA
		public DyingStar()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001C00 RID: 7168
		// (get) Token: 0x06006A10 RID: 27152 RVA: 0x0025A5F7 File Offset: 0x002587F7
		public override int CanonicalStarCost
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001C01 RID: 7169
		// (get) Token: 0x06006A11 RID: 27153 RVA: 0x0025A5FA File Offset: 0x002587FA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(9m, ValueProp.Move),
					new DynamicVar("StrengthLoss", 9m)
				});
			}
		}

		// Token: 0x17001C02 RID: 7170
		// (get) Token: 0x06006A12 RID: 27154 RVA: 0x0025A62B File Offset: 0x0025882B
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Ethereal);
			}
		}

		// Token: 0x17001C03 RID: 7171
		// (get) Token: 0x06006A13 RID: 27155 RVA: 0x0025A633 File Offset: 0x00258833
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06006A14 RID: 27156 RVA: 0x0025A640 File Offset: 0x00258840
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Attack", base.Owner.Character.AttackAnimDelay);
			IReadOnlyList<Creature> enemies = base.CombatState.HittableEnemies;
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_starry_impact", null, null)
				.SpawningHitVfxOnEachCreature()
				.Execute(choiceContext);
			foreach (Creature enemy in enemies)
			{
				await PowerCmd.Apply<DyingStarPower>(enemy, base.DynamicVars["StrengthLoss"].BaseValue, base.Owner.Creature, this, false);
				VfxCmd.PlayOnCreature(enemy, "vfx/vfx_attack_slash");
				enemy = null;
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x0025A68B File Offset: 0x0025888B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars["StrengthLoss"].UpgradeValueBy(2m);
		}

		// Token: 0x04002571 RID: 9585
		private const string _strengthLossKey = "StrengthLoss";
	}
}
