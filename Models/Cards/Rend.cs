using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A26 RID: 2598
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Rend : CardModel
	{
		// Token: 0x06006F46 RID: 28486 RVA: 0x00264EA5 File Offset: 0x002630A5
		public Rend()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E2D RID: 7725
		// (get) Token: 0x06006F47 RID: 28487 RVA: 0x00264EB4 File Offset: 0x002630B4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(15m);
				array[1] = new ExtraDamageVar(5m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier(delegate(CardModel card, [Nullable(2)] Creature target)
				{
					int num;
					if (target == null)
					{
						num = 0;
					}
					else
					{
						IEnumerable<PowerModel> powers = target.Powers;
						Func<PowerModel, bool> func;
						if ((func = Rend.<>O.<0>__ShouldCountPower) == null)
						{
							func = (Rend.<>O.<0>__ShouldCountPower = new Func<PowerModel, bool>(Rend.ShouldCountPower));
						}
						num = powers.Count(func);
					}
					return num;
				});
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006F48 RID: 28488 RVA: 0x00264F18 File Offset: 0x00263118
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006F49 RID: 28489 RVA: 0x00264F6B File Offset: 0x0026316B
		protected override void OnUpgrade()
		{
			base.DynamicVars.ExtraDamage.UpgradeValueBy(3m);
			base.DynamicVars.CalculationBase.UpgradeValueBy(3m);
		}

		// Token: 0x06006F4A RID: 28490 RVA: 0x00264F99 File Offset: 0x00263199
		private static bool ShouldCountPower(PowerModel power)
		{
			return power.TypeForCurrentAmount == PowerType.Debuff && !(power is ITemporaryPower);
		}

		// Token: 0x02001FD0 RID: 8144
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04008289 RID: 33417
			[Nullable(0)]
			public static Func<PowerModel, bool> <0>__ShouldCountPower;
		}
	}
}
