using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A23 RID: 2595
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Refract : CardModel
	{
		// Token: 0x06006F32 RID: 28466 RVA: 0x00264C3A File Offset: 0x00262E3A
		public Refract()
			: base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E23 RID: 7715
		// (get) Token: 0x06006F33 RID: 28467 RVA: 0x00264C47 File Offset: 0x00262E47
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<GlassOrb>()
				});
			}
		}

		// Token: 0x17001E24 RID: 7716
		// (get) Token: 0x06006F34 RID: 28468 RVA: 0x00264C6A File Offset: 0x00262E6A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new RepeatVar(2),
					new DamageVar(9m, ValueProp.Move)
				});
			}
		}

		// Token: 0x06006F35 RID: 28469 RVA: 0x00264C90 File Offset: 0x00262E90
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(2).FromCard(this)
				.OnlyPlayAnimOnce()
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			for (int i = 0; i < base.DynamicVars.Repeat.IntValue; i++)
			{
				await OrbCmd.Channel<GlassOrb>(choiceContext, base.Owner);
			}
		}

		// Token: 0x06006F36 RID: 28470 RVA: 0x00264CE3 File Offset: 0x00262EE3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
