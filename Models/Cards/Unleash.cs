using System;
using System.Collections.Generic;
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
	// Token: 0x02000AAB RID: 2731
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Unleash : CardModel
	{
		// Token: 0x06007218 RID: 29208 RVA: 0x0026A6BF File Offset: 0x002688BF
		public Unleash()
			: base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F52 RID: 8018
		// (get) Token: 0x06007219 RID: 29209 RVA: 0x0026A6CC File Offset: 0x002688CC
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001F53 RID: 8019
		// (get) Token: 0x0600721A RID: 29210 RVA: 0x0026A6D9 File Offset: 0x002688D9
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001F54 RID: 8020
		// (get) Token: 0x0600721B RID: 29211 RVA: 0x0026A6E8 File Offset: 0x002688E8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(6m);
				array[1] = new ExtraDamageVar(1m).FromOsty();
				array[2] = new CalculatedDamageVar(ValueProp.Move).FromOsty().WithMultiplier(delegate(CardModel card, [Nullable(2)] Creature _)
				{
					Creature osty = card.Owner.Osty;
					return (osty != null && osty.IsAlive) ? osty.CurrentHp : 0;
				});
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x0600721C RID: 29212 RVA: 0x0026A754 File Offset: 0x00268954
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

		// Token: 0x0600721D RID: 29213 RVA: 0x0026A7A7 File Offset: 0x002689A7
		protected override void OnUpgrade()
		{
			base.DynamicVars.CalculationBase.UpgradeValueBy(3m);
		}
	}
}
