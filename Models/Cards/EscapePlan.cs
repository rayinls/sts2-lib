using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200093C RID: 2364
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EscapePlan : CardModel
	{
		// Token: 0x06006A5A RID: 27226 RVA: 0x0025ADD3 File Offset: 0x00258FD3
		public EscapePlan()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C23 RID: 7203
		// (get) Token: 0x06006A5B RID: 27227 RVA: 0x0025ADE0 File Offset: 0x00258FE0
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C24 RID: 7204
		// (get) Token: 0x06006A5C RID: 27228 RVA: 0x0025ADE3 File Offset: 0x00258FE3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(3m, ValueProp.Move));
			}
		}

		// Token: 0x06006A5D RID: 27229 RVA: 0x0025ADF8 File Offset: 0x00258FF8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<CardModel> enumerable = await CardPileCmd.Draw(choiceContext, 1m, base.Owner, false);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null && cardModel.Type == CardType.Skill)
			{
				await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			}
		}

		// Token: 0x06006A5E RID: 27230 RVA: 0x0025AE4B File Offset: 0x0025904B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
		}
	}
}
