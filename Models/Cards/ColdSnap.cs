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
	// Token: 0x020008E6 RID: 2278
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ColdSnap : CardModel
	{
		// Token: 0x06006894 RID: 26772 RVA: 0x00257A01 File Offset: 0x00255C01
		public ColdSnap()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B56 RID: 6998
		// (get) Token: 0x06006895 RID: 26773 RVA: 0x00257A0E File Offset: 0x00255C0E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(6m, ValueProp.Move));
			}
		}

		// Token: 0x17001B57 RID: 6999
		// (get) Token: 0x06006896 RID: 26774 RVA: 0x00257A21 File Offset: 0x00255C21
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<FrostOrb>()
				});
			}
		}

		// Token: 0x06006897 RID: 26775 RVA: 0x00257A44 File Offset: 0x00255C44
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await OrbCmd.Channel<FrostOrb>(choiceContext, base.Owner);
		}

		// Token: 0x06006898 RID: 26776 RVA: 0x00257A97 File Offset: 0x00255C97
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
