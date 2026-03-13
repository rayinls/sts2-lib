using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A6B RID: 2667
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Squeeze : CardModel
	{
		// Token: 0x060070CD RID: 28877 RVA: 0x00267E34 File Offset: 0x00266034
		public Squeeze()
			: base(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001ED2 RID: 7890
		// (get) Token: 0x060070CE RID: 28878 RVA: 0x00267E41 File Offset: 0x00266041
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001ED3 RID: 7891
		// (get) Token: 0x060070CF RID: 28879 RVA: 0x00267E4E File Offset: 0x0026604E
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001ED4 RID: 7892
		// (get) Token: 0x060070D0 RID: 28880 RVA: 0x00267E60 File Offset: 0x00266060
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(25m);
				array[1] = new ExtraDamageVar(5m).FromOsty();
				array[2] = new CalculatedDamageVar(ValueProp.Move).FromOsty().WithMultiplier((CardModel card, [Nullable(2)] Creature _) => card.Owner.PlayerCombatState.AllCards.Count((CardModel c) => c.Tags.Contains(CardTag.OstyAttack) && c != card));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x060070D1 RID: 28881 RVA: 0x00267ECC File Offset: 0x002660CC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			if (!Osty.CheckMissingWithAnim(base.Owner))
			{
				await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromOsty(base.Owner.Osty, this).Targeting(cardPlay.Target)
					.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
					.Execute(choiceContext);
			}
		}

		// Token: 0x060070D2 RID: 28882 RVA: 0x00267F1F File Offset: 0x0026611F
		protected override void OnUpgrade()
		{
			base.DynamicVars.CalculationBase.UpgradeValueBy(5m);
			base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
		}
	}
}
