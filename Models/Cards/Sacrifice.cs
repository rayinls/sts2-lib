using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A31 RID: 2609
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Sacrifice : CardModel
	{
		// Token: 0x06006F84 RID: 28548 RVA: 0x00265662 File Offset: 0x00263862
		public Sacrifice()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E47 RID: 7751
		// (get) Token: 0x06006F85 RID: 28549 RVA: 0x0026566F File Offset: 0x0026386F
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001E48 RID: 7752
		// (get) Token: 0x06006F86 RID: 28550 RVA: 0x0026567C File Offset: 0x0026387C
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Retain);
			}
		}

		// Token: 0x17001E49 RID: 7753
		// (get) Token: 0x06006F87 RID: 28551 RVA: 0x00265684 File Offset: 0x00263884
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006F88 RID: 28552 RVA: 0x00265688 File Offset: 0x00263888
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (!Osty.CheckMissingWithAnim(base.Owner))
			{
				int blockGain = base.Owner.Osty.MaxHp * 2;
				await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
				await CreatureCmd.Kill(base.Owner.Osty, false);
				await CreatureCmd.GainBlock(base.Owner.Creature, blockGain, ValueProp.Move, cardPlay, false);
			}
		}

		// Token: 0x06006F89 RID: 28553 RVA: 0x002656D3 File Offset: 0x002638D3
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
