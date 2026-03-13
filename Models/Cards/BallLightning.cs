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
	// Token: 0x0200089F RID: 2207
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BallLightning : CardModel
	{
		// Token: 0x0600672D RID: 26413 RVA: 0x00254CA7 File Offset: 0x00252EA7
		public BallLightning()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001AC6 RID: 6854
		// (get) Token: 0x0600672E RID: 26414 RVA: 0x00254CB4 File Offset: 0x00252EB4
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<LightningOrb>()
				});
			}
		}

		// Token: 0x17001AC7 RID: 6855
		// (get) Token: 0x0600672F RID: 26415 RVA: 0x00254CD7 File Offset: 0x00252ED7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x06006730 RID: 26416 RVA: 0x00254CEC File Offset: 0x00252EEC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			await OrbCmd.Channel<LightningOrb>(choiceContext, base.Owner);
		}

		// Token: 0x06006731 RID: 26417 RVA: 0x00254D3F File Offset: 0x00252F3F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
