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
	// Token: 0x020009FF RID: 2559
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Poke : CardModel
	{
		// Token: 0x06006E7E RID: 28286 RVA: 0x00263588 File Offset: 0x00261788
		public Poke()
			: base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DDA RID: 7642
		// (get) Token: 0x06006E7F RID: 28287 RVA: 0x00263595 File Offset: 0x00261795
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001DDB RID: 7643
		// (get) Token: 0x06006E80 RID: 28288 RVA: 0x002635A2 File Offset: 0x002617A2
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001DDC RID: 7644
		// (get) Token: 0x06006E81 RID: 28289 RVA: 0x002635B1 File Offset: 0x002617B1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new OstyDamageVar(6m, ValueProp.Move));
			}
		}

		// Token: 0x06006E82 RID: 28290 RVA: 0x002635C4 File Offset: 0x002617C4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			if (!Osty.CheckMissingWithAnim(base.Owner))
			{
				await DamageCmd.Attack(base.DynamicVars.OstyDamage.BaseValue).FromOsty(base.Owner.Osty, this).Targeting(cardPlay.Target)
					.WithAttackerAnim("attack_poke", 0.3f, null)
					.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
					.Execute(choiceContext);
			}
		}

		// Token: 0x06006E83 RID: 28291 RVA: 0x00263617 File Offset: 0x00261817
		protected override void OnUpgrade()
		{
			base.DynamicVars.OstyDamage.UpgradeValueBy(3m);
		}
	}
}
