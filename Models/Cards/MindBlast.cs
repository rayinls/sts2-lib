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
	// Token: 0x020009CB RID: 2507
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MindBlast : CardModel
	{
		// Token: 0x06006D6C RID: 28012 RVA: 0x00261340 File Offset: 0x0025F540
		public MindBlast()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D68 RID: 7528
		// (get) Token: 0x06006D6D RID: 28013 RVA: 0x00261350 File Offset: 0x0025F550
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(0m);
				array[1] = new ExtraDamageVar(1m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature _) => PileType.Draw.GetPile(card.Owner).Cards.Count);
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x17001D69 RID: 7529
		// (get) Token: 0x06006D6E RID: 28014 RVA: 0x002613AF File Offset: 0x0025F5AF
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Innate);
			}
		}

		// Token: 0x06006D6F RID: 28015 RVA: 0x002613B8 File Offset: 0x0025F5B8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006D70 RID: 28016 RVA: 0x0026140B File Offset: 0x0025F60B
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
