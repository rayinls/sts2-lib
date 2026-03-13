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
	// Token: 0x02000A69 RID: 2665
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Spur : CardModel
	{
		// Token: 0x060070C2 RID: 28866 RVA: 0x00267C93 File Offset: 0x00265E93
		public Spur()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001ECD RID: 7885
		// (get) Token: 0x060070C3 RID: 28867 RVA: 0x00267CA0 File Offset: 0x00265EA0
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Retain);
			}
		}

		// Token: 0x17001ECE RID: 7886
		// (get) Token: 0x060070C4 RID: 28868 RVA: 0x00267CA8 File Offset: 0x00265EA8
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new DynamicVar[] { base.DynamicVars.Summon }));
			}
		}

		// Token: 0x17001ECF RID: 7887
		// (get) Token: 0x060070C5 RID: 28869 RVA: 0x00267CCA File Offset: 0x00265ECA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new SummonVar(3m),
					new HealVar(5m)
				});
			}
		}

		// Token: 0x060070C6 RID: 28870 RVA: 0x00267CF4 File Offset: 0x00265EF4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OstyCmd.Summon(choiceContext, base.Owner, base.DynamicVars.Summon.BaseValue, this);
			await CreatureCmd.Heal(base.Owner.Osty, base.DynamicVars.Heal.BaseValue, true);
		}

		// Token: 0x060070C7 RID: 28871 RVA: 0x00267D3F File Offset: 0x00265F3F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Summon.UpgradeValueBy(2m);
			base.DynamicVars.Heal.UpgradeValueBy(2m);
		}
	}
}
