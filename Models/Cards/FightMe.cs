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
	// Token: 0x0200094D RID: 2381
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FightMe : CardModel
	{
		// Token: 0x06006ABC RID: 27324 RVA: 0x0025BA57 File Offset: 0x00259C57
		public FightMe()
			: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C4F RID: 7247
		// (get) Token: 0x06006ABD RID: 27325 RVA: 0x0025BA64 File Offset: 0x00259C64
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17001C50 RID: 7248
		// (get) Token: 0x06006ABE RID: 27326 RVA: 0x0025BA70 File Offset: 0x00259C70
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(5m, ValueProp.Move),
					new RepeatVar(2),
					new PowerVar<StrengthPower>(2m),
					new DynamicVar("EnemyStrength", 1m)
				});
			}
		}

		// Token: 0x06006ABF RID: 27327 RVA: 0x0025BAC0 File Offset: 0x00259CC0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.DynamicVars.Repeat.IntValue).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "heavy_attack.mp3")
				.Execute(choiceContext);
			await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars["StrengthPower"].BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<StrengthPower>(cardPlay.Target, base.DynamicVars["EnemyStrength"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006AC0 RID: 27328 RVA: 0x0025BB13 File Offset: 0x00259D13
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars.Strength.UpgradeValueBy(1m);
		}

		// Token: 0x0400257C RID: 9596
		private const string _enemyStrengthKey = "EnemyStrength";
	}
}
