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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008B0 RID: 2224
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BladeOfInk : CardModel
	{
		// Token: 0x06006784 RID: 26500 RVA: 0x0025587E File Offset: 0x00253A7E
		public BladeOfInk()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001AE8 RID: 6888
		// (get) Token: 0x06006785 RID: 26501 RVA: 0x0025588B File Offset: 0x00253A8B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17001AE9 RID: 6889
		// (get) Token: 0x06006786 RID: 26502 RVA: 0x00255897 File Offset: 0x00253A97
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(2m));
			}
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x002558AC File Offset: 0x00253AAC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<BladeOfInkPower>(base.Owner.Creature, base.DynamicVars.Strength.IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x002558EF File Offset: 0x00253AEF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Strength.UpgradeValueBy(1m);
		}
	}
}
