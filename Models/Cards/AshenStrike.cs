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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000898 RID: 2200
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AshenStrike : CardModel
	{
		// Token: 0x06006706 RID: 26374 RVA: 0x00254803 File Offset: 0x00252A03
		public AshenStrike()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001AB3 RID: 6835
		// (get) Token: 0x06006707 RID: 26375 RVA: 0x00254810 File Offset: 0x00252A10
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001AB4 RID: 6836
		// (get) Token: 0x06006708 RID: 26376 RVA: 0x00254820 File Offset: 0x00252A20
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(6m);
				array[1] = new ExtraDamageVar(3m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature _) => PileType.Exhaust.GetPile(card.Owner).Cards.Count);
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x17001AB5 RID: 6837
		// (get) Token: 0x06006709 RID: 26377 RVA: 0x00254881 File Offset: 0x00252A81
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x0600670A RID: 26378 RVA: 0x00254890 File Offset: 0x00252A90
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x0600670B RID: 26379 RVA: 0x002548E3 File Offset: 0x00252AE3
		protected override void OnUpgrade()
		{
			base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
		}
	}
}
