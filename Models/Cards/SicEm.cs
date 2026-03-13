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
	// Token: 0x02000A4E RID: 2638
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SicEm : CardModel
	{
		// Token: 0x06007020 RID: 28704 RVA: 0x002668FF File Offset: 0x00264AFF
		public SicEm()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E8A RID: 7818
		// (get) Token: 0x06007021 RID: 28705 RVA: 0x0026690C File Offset: 0x00264B0C
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001E8B RID: 7819
		// (get) Token: 0x06007022 RID: 28706 RVA: 0x00266919 File Offset: 0x00264B19
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001E8C RID: 7820
		// (get) Token: 0x06007023 RID: 28707 RVA: 0x00266928 File Offset: 0x00264B28
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonStatic, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001E8D RID: 7821
		// (get) Token: 0x06007024 RID: 28708 RVA: 0x0026693B File Offset: 0x00264B3B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new OstyDamageVar(5m, ValueProp.Move),
					new PowerVar<SicEmPower>(2m)
				});
			}
		}

		// Token: 0x06007025 RID: 28709 RVA: 0x00266968 File Offset: 0x00264B68
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			if (!Osty.CheckMissingWithAnim(base.Owner))
			{
				await DamageCmd.Attack(base.DynamicVars.OstyDamage.BaseValue).FromOsty(base.Owner.Osty, this).Targeting(cardPlay.Target)
					.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
					.Execute(choiceContext);
			}
			await PowerCmd.Apply<SicEmPower>(cardPlay.Target, base.DynamicVars["SicEmPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06007026 RID: 28710 RVA: 0x002669BB File Offset: 0x00264BBB
		protected override void OnUpgrade()
		{
			base.DynamicVars.OstyDamage.UpgradeValueBy(1m);
			base.DynamicVars["SicEmPower"].UpgradeValueBy(1m);
		}
	}
}
