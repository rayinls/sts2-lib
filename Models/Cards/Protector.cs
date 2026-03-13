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
	// Token: 0x02000A0B RID: 2571
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Protector : CardModel
	{
		// Token: 0x06006EB4 RID: 28340 RVA: 0x00263BF3 File Offset: 0x00261DF3
		public Protector()
			: base(1, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DEF RID: 7663
		// (get) Token: 0x06006EB5 RID: 28341 RVA: 0x00263C00 File Offset: 0x00261E00
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001DF0 RID: 7664
		// (get) Token: 0x06006EB6 RID: 28342 RVA: 0x00263C0D File Offset: 0x00261E0D
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001DF1 RID: 7665
		// (get) Token: 0x06006EB7 RID: 28343 RVA: 0x00263C1C File Offset: 0x00261E1C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(10m);
				array[1] = new ExtraDamageVar(1m).FromOsty();
				array[2] = new CalculatedDamageVar(ValueProp.Move).FromOsty().WithMultiplier(delegate(CardModel card, [Nullable(2)] Creature _)
				{
					Creature osty = card.Owner.Osty;
					return (osty != null && osty.IsAlive) ? osty.MaxHp : 0;
				});
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006EB8 RID: 28344 RVA: 0x00263C88 File Offset: 0x00261E88
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

		// Token: 0x06006EB9 RID: 28345 RVA: 0x00263CDB File Offset: 0x00261EDB
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
			base.DynamicVars.CalculationBase.UpgradeValueBy(5m);
		}
	}
}
