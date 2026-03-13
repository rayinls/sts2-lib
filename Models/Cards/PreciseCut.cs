using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A03 RID: 2563
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PreciseCut : CardModel
	{
		// Token: 0x06006E91 RID: 28305 RVA: 0x002637AB File Offset: 0x002619AB
		public PreciseCut()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DE3 RID: 7651
		// (get) Token: 0x06006E92 RID: 28306 RVA: 0x002637B8 File Offset: 0x002619B8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(13m);
				array[1] = new ExtraDamageVar(2m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier(delegate(CardModel card, [Nullable(2)] Creature _)
				{
					int num = PileType.Hand.GetPile(card.Owner).Cards.Count;
					CardPile pile = card.Pile;
					if (pile != null && pile.Type == PileType.Hand)
					{
						num--;
					}
					return -num;
				});
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006E93 RID: 28307 RVA: 0x0026381C File Offset: 0x00261A1C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, "dagger_throw.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006E94 RID: 28308 RVA: 0x0026386F File Offset: 0x00261A6F
		protected override void OnUpgrade()
		{
			base.DynamicVars.CalculationBase.UpgradeValueBy(3m);
		}
	}
}
