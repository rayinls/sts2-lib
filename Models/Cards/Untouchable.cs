using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AAE RID: 2734
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Untouchable : CardModel
	{
		// Token: 0x06007226 RID: 29222 RVA: 0x0026A8BF File Offset: 0x00268ABF
		public Untouchable()
			: base(2, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001F57 RID: 8023
		// (get) Token: 0x06007227 RID: 29223 RVA: 0x0026A8CC File Offset: 0x00268ACC
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F58 RID: 8024
		// (get) Token: 0x06007228 RID: 29224 RVA: 0x0026A8CF File Offset: 0x00268ACF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(9m, ValueProp.Move));
			}
		}

		// Token: 0x17001F59 RID: 8025
		// (get) Token: 0x06007229 RID: 29225 RVA: 0x0026A8E3 File Offset: 0x00268AE3
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Sly);
			}
		}

		// Token: 0x0600722A RID: 29226 RVA: 0x0026A8EC File Offset: 0x00268AEC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x0600722B RID: 29227 RVA: 0x0026A937 File Offset: 0x00268B37
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
