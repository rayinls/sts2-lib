using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A84 RID: 2692
	[NullableContext(1)]
	[Nullable(0)]
	public class SweepingGaze : CardModel
	{
		// Token: 0x06007148 RID: 29000 RVA: 0x00268ECB File Offset: 0x002670CB
		public SweepingGaze()
			: base(0, CardType.Attack, CardRarity.Token, TargetType.RandomEnemy, true)
		{
		}

		// Token: 0x17001EFE RID: 7934
		// (get) Token: 0x06007149 RID: 29001 RVA: 0x00268ED8 File Offset: 0x002670D8
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001EFF RID: 7935
		// (get) Token: 0x0600714A RID: 29002 RVA: 0x00268EE5 File Offset: 0x002670E5
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001F00 RID: 7936
		// (get) Token: 0x0600714B RID: 29003 RVA: 0x00268EF4 File Offset: 0x002670F4
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Ethereal,
					CardKeyword.Exhaust
				});
			}
		}

		// Token: 0x17001F01 RID: 7937
		// (get) Token: 0x0600714C RID: 29004 RVA: 0x00268F09 File Offset: 0x00267109
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new OstyDamageVar(10m, ValueProp.Move));
			}
		}

		// Token: 0x0600714D RID: 29005 RVA: 0x00268F20 File Offset: 0x00267120
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (!Osty.CheckMissingWithAnim(base.Owner))
			{
				await DamageCmd.Attack(base.DynamicVars.OstyDamage.BaseValue).FromOsty(base.Owner.Osty, this).TargetingRandomOpponents(base.CombatState, true)
					.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
					.Execute(choiceContext);
			}
		}

		// Token: 0x0600714E RID: 29006 RVA: 0x00268F6B File Offset: 0x0026716B
		protected override void OnUpgrade()
		{
			base.DynamicVars.OstyDamage.UpgradeValueBy(5m);
		}
	}
}
