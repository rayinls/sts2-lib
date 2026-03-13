using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A40 RID: 2624
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SetupStrike : CardModel
	{
		// Token: 0x06006FCE RID: 28622 RVA: 0x00265EFB File Offset: 0x002640FB
		public SetupStrike()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E64 RID: 7780
		// (get) Token: 0x06006FCF RID: 28623 RVA: 0x00265F08 File Offset: 0x00264108
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17001E65 RID: 7781
		// (get) Token: 0x06006FD0 RID: 28624 RVA: 0x00265F14 File Offset: 0x00264114
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001E66 RID: 7782
		// (get) Token: 0x06006FD1 RID: 28625 RVA: 0x00265F23 File Offset: 0x00264123
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(7m, ValueProp.Move),
					new PowerVar<StrengthPower>(2m)
				});
			}
		}

		// Token: 0x06006FD2 RID: 28626 RVA: 0x00265F50 File Offset: 0x00264150
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<SetupStrikePower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006FD3 RID: 28627 RVA: 0x00265FA3 File Offset: 0x002641A3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars.Strength.UpgradeValueBy(1m);
		}
	}
}
