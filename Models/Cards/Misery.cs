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
	// Token: 0x020009D1 RID: 2513
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Misery : CardModel
	{
		// Token: 0x06006D90 RID: 28048 RVA: 0x00261761 File Offset: 0x0025F961
		public Misery()
			: base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D7B RID: 7547
		// (get) Token: 0x06006D91 RID: 28049 RVA: 0x0026176E File Offset: 0x0025F96E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x06006D92 RID: 28050 RVA: 0x00261784 File Offset: 0x0025F984
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			List<PowerModel> originalDebuffs = (from p in cardPlay.Target.Powers
				where p.TypeForCurrentAmount == PowerType.Debuff
				select (PowerModel)p.ClonePreservingMutability()).ToList<PowerModel>();
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			foreach (Creature enemy in base.CombatState.HittableEnemies)
			{
				if (enemy != cardPlay.Target)
				{
					foreach (PowerModel powerModel in originalDebuffs)
					{
						PowerModel powerById = enemy.GetPowerById(powerModel.Id);
						if (powerById != null && !powerById.IsInstanced)
						{
							Misery.DoHackyThingsForSpecificPowers(powerById);
							await PowerCmd.ModifyAmount(powerById, powerModel.Amount, base.Owner.Creature, this, false);
						}
						else
						{
							PowerModel powerModel2 = (PowerModel)powerModel.ClonePreservingMutability();
							Misery.DoHackyThingsForSpecificPowers(powerModel2);
							await PowerCmd.Apply(powerModel2, enemy, powerModel.Amount, base.Owner.Creature, this, false);
						}
					}
					List<PowerModel>.Enumerator enumerator2 = default(List<PowerModel>.Enumerator);
					enemy = null;
				}
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06006D93 RID: 28051 RVA: 0x002617D8 File Offset: 0x0025F9D8
		private static void DoHackyThingsForSpecificPowers(PowerModel power)
		{
			ITemporaryPower temporaryPower = power as ITemporaryPower;
			if (temporaryPower != null)
			{
				temporaryPower.IgnoreNextInstance();
			}
		}

		// Token: 0x06006D94 RID: 28052 RVA: 0x002617F5 File Offset: 0x0025F9F5
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.AddKeyword(CardKeyword.Retain);
		}
	}
}
