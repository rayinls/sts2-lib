using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000990 RID: 2448
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HighFive : CardModel
	{
		// Token: 0x06006C20 RID: 27680 RVA: 0x0025E897 File Offset: 0x0025CA97
		public HighFive()
			: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001CE0 RID: 7392
		// (get) Token: 0x06006C21 RID: 27681 RVA: 0x0025E8A4 File Offset: 0x0025CAA4
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001CE1 RID: 7393
		// (get) Token: 0x06006C22 RID: 27682 RVA: 0x0025E8B1 File Offset: 0x0025CAB1
		protected override bool IsPlayable
		{
			get
			{
				return !base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001CE2 RID: 7394
		// (get) Token: 0x06006C23 RID: 27683 RVA: 0x0025E8C1 File Offset: 0x0025CAC1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new OstyDamageVar(11m, ValueProp.Move),
					new PowerVar<VulnerablePower>(2m)
				});
			}
		}

		// Token: 0x17001CE3 RID: 7395
		// (get) Token: 0x06006C24 RID: 27684 RVA: 0x0025E8EC File Offset: 0x0025CAEC
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001CE4 RID: 7396
		// (get) Token: 0x06006C25 RID: 27685 RVA: 0x0025E8FB File Offset: 0x0025CAFB
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x06006C26 RID: 27686 RVA: 0x0025E908 File Offset: 0x0025CB08
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (!Osty.CheckMissingWithAnim(base.Owner))
			{
				await DamageCmd.Attack(base.DynamicVars.OstyDamage.BaseValue).FromOsty(base.Owner.Osty, this).TargetingAllOpponents(base.CombatState)
					.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
					.Execute(choiceContext);
				await PowerCmd.Apply<VulnerablePower>(base.CombatState.HittableEnemies, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
			}
		}

		// Token: 0x06006C27 RID: 27687 RVA: 0x0025E953 File Offset: 0x0025CB53
		protected override void OnUpgrade()
		{
			base.DynamicVars.OstyDamage.UpgradeValueBy(2m);
			base.DynamicVars.Vulnerable.UpgradeValueBy(1m);
		}
	}
}
