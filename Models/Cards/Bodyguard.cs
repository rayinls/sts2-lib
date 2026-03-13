using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008B6 RID: 2230
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Bodyguard : CardModel
	{
		// Token: 0x060067A3 RID: 26531 RVA: 0x00255C3B File Offset: 0x00253E3B
		public Bodyguard()
			: base(1, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
		{
		}

		// Token: 0x17001AF5 RID: 6901
		// (get) Token: 0x060067A4 RID: 26532 RVA: 0x00255C48 File Offset: 0x00253E48
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new SummonVar(5m));
			}
		}

		// Token: 0x17001AF6 RID: 6902
		// (get) Token: 0x060067A5 RID: 26533 RVA: 0x00255C5A File Offset: 0x00253E5A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new DynamicVar[] { base.DynamicVars.Summon }));
			}
		}

		// Token: 0x060067A6 RID: 26534 RVA: 0x00255C7C File Offset: 0x00253E7C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OstyCmd.Summon(choiceContext, base.Owner, base.DynamicVars.Summon.BaseValue, this);
		}

		// Token: 0x060067A7 RID: 26535 RVA: 0x00255CC7 File Offset: 0x00253EC7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Summon.UpgradeValueBy(2m);
		}
	}
}
