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
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008EE RID: 2286
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ConsumingShadow : CardModel
	{
		// Token: 0x060068BE RID: 26814 RVA: 0x00257FEB File Offset: 0x002561EB
		public ConsumingShadow()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B68 RID: 7016
		// (get) Token: 0x060068BF RID: 26815 RVA: 0x00257FF8 File Offset: 0x002561F8
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<DarkOrb>(),
					HoverTipFactory.Static(StaticHoverTip.Evoke, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x17001B69 RID: 7017
		// (get) Token: 0x060068C0 RID: 26816 RVA: 0x00258029 File Offset: 0x00256229
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new RepeatVar(2),
					new PowerVar<ConsumingShadowPower>(1m)
				});
			}
		}

		// Token: 0x060068C1 RID: 26817 RVA: 0x0025804C File Offset: 0x0025624C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			for (int i = 0; i < base.DynamicVars.Repeat.IntValue; i++)
			{
				await OrbCmd.Channel<DarkOrb>(choiceContext, base.Owner);
			}
			await PowerCmd.Apply<ConsumingShadowPower>(base.Owner.Creature, base.DynamicVars["ConsumingShadowPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x00258097 File Offset: 0x00256297
		protected override void OnUpgrade()
		{
			base.DynamicVars.Repeat.UpgradeValueBy(1m);
		}
	}
}
