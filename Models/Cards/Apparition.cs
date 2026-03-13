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
	// Token: 0x02000894 RID: 2196
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Apparition : CardModel
	{
		// Token: 0x060066F3 RID: 26355 RVA: 0x00254646 File Offset: 0x00252846
		public Apparition()
			: base(1, CardType.Skill, CardRarity.Ancient, TargetType.Self, true)
		{
		}

		// Token: 0x17001AA9 RID: 6825
		// (get) Token: 0x060066F4 RID: 26356 RVA: 0x00254653 File Offset: 0x00252853
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Ethereal,
					CardKeyword.Exhaust
				});
			}
		}

		// Token: 0x17001AAA RID: 6826
		// (get) Token: 0x060066F5 RID: 26357 RVA: 0x00254668 File Offset: 0x00252868
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<IntangiblePower>());
			}
		}

		// Token: 0x17001AAB RID: 6827
		// (get) Token: 0x060066F6 RID: 26358 RVA: 0x00254674 File Offset: 0x00252874
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<IntangiblePower>(1m));
			}
		}

		// Token: 0x060066F7 RID: 26359 RVA: 0x00254688 File Offset: 0x00252888
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<IntangiblePower>(base.Owner.Creature, base.DynamicVars["IntangiblePower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060066F8 RID: 26360 RVA: 0x002546CB File Offset: 0x002528CB
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Ethereal);
		}
	}
}
