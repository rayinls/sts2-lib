using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200089D RID: 2205
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Backstab : CardModel
	{
		// Token: 0x06006721 RID: 26401 RVA: 0x00254B77 File Offset: 0x00252D77
		public Backstab()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001ABF RID: 6847
		// (get) Token: 0x06006722 RID: 26402 RVA: 0x00254B84 File Offset: 0x00252D84
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Exhaust,
					CardKeyword.Innate
				});
			}
		}

		// Token: 0x17001AC0 RID: 6848
		// (get) Token: 0x06006723 RID: 26403 RVA: 0x00254B99 File Offset: 0x00252D99
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(11m, ValueProp.Move));
			}
		}

		// Token: 0x06006724 RID: 26404 RVA: 0x00254BB0 File Offset: 0x00252DB0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_dramatic_stab", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006725 RID: 26405 RVA: 0x00254C03 File Offset: 0x00252E03
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}
