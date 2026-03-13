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
	// Token: 0x02000997 RID: 2455
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class IceLance : CardModel
	{
		// Token: 0x06006C4A RID: 27722 RVA: 0x0025EDDB File Offset: 0x0025CFDB
		public IceLance()
			: base(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CF2 RID: 7410
		// (get) Token: 0x06006C4B RID: 27723 RVA: 0x0025EDE8 File Offset: 0x0025CFE8
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

		// Token: 0x17001CF3 RID: 7411
		// (get) Token: 0x06006C4C RID: 27724 RVA: 0x0025EE0B File Offset: 0x0025D00B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(19m, ValueProp.Move),
					new RepeatVar(3)
				});
			}
		}

		// Token: 0x06006C4D RID: 27725 RVA: 0x0025EE34 File Offset: 0x0025D034
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			for (int i = 0; i < base.DynamicVars.Repeat.IntValue; i++)
			{
				await OrbCmd.Channel<FrostOrb>(choiceContext, base.Owner);
			}
		}

		// Token: 0x06006C4E RID: 27726 RVA: 0x0025EE87 File Offset: 0x0025D087
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}
	}
}
