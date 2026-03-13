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
	// Token: 0x020009D6 RID: 2518
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Monologue : CardModel
	{
		// Token: 0x06006DAA RID: 28074 RVA: 0x00261A71 File Offset: 0x0025FC71
		public Monologue()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001D85 RID: 7557
		// (get) Token: 0x06006DAB RID: 28075 RVA: 0x00261A7E File Offset: 0x0025FC7E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Power", 1m));
			}
		}

		// Token: 0x17001D86 RID: 7558
		// (get) Token: 0x06006DAC RID: 28076 RVA: 0x00261A94 File Offset: 0x0025FC94
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06006DAD RID: 28077 RVA: 0x00261AA0 File Offset: 0x0025FCA0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			MonologuePower monologuePower = await PowerCmd.Apply<MonologuePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
			if (monologuePower != null)
			{
				monologuePower.DynamicVars.Strength.BaseValue = base.DynamicVars["Power"].BaseValue;
			}
		}

		// Token: 0x06006DAE RID: 28078 RVA: 0x00261AE3 File Offset: 0x0025FCE3
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Retain);
		}

		// Token: 0x040025B4 RID: 9652
		private const string _powerKey = "Power";
	}
}
