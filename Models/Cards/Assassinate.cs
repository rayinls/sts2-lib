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
	// Token: 0x02000899 RID: 2201
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Assassinate : CardModel
	{
		// Token: 0x0600670C RID: 26380 RVA: 0x002548FA File Offset: 0x00252AFA
		public Assassinate()
			: base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001AB6 RID: 6838
		// (get) Token: 0x0600670D RID: 26381 RVA: 0x00254907 File Offset: 0x00252B07
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(10m, ValueProp.Move),
					new PowerVar<VulnerablePower>(1m)
				});
			}
		}

		// Token: 0x17001AB7 RID: 6839
		// (get) Token: 0x0600670E RID: 26382 RVA: 0x00254931 File Offset: 0x00252B31
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x17001AB8 RID: 6840
		// (get) Token: 0x0600670F RID: 26383 RVA: 0x0025493D File Offset: 0x00252B3D
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Innate,
					CardKeyword.Exhaust
				});
			}
		}

		// Token: 0x06006710 RID: 26384 RVA: 0x00254954 File Offset: 0x00252B54
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_dramatic_stab", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006711 RID: 26385 RVA: 0x002549A7 File Offset: 0x00252BA7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
			base.DynamicVars.Vulnerable.UpgradeValueBy(1m);
		}
	}
}
