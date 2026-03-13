using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000996 RID: 2454
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class IAmInvincible : CardModel
	{
		// Token: 0x06006C44 RID: 27716 RVA: 0x0025ECFF File Offset: 0x0025CEFF
		public IAmInvincible()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001CF0 RID: 7408
		// (get) Token: 0x06006C45 RID: 27717 RVA: 0x0025ED0C File Offset: 0x0025CF0C
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001CF1 RID: 7409
		// (get) Token: 0x06006C46 RID: 27718 RVA: 0x0025ED0F File Offset: 0x0025CF0F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(9m, ValueProp.Move));
			}
		}

		// Token: 0x06006C47 RID: 27719 RVA: 0x0025ED24 File Offset: 0x0025CF24
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006C48 RID: 27720 RVA: 0x0025ED70 File Offset: 0x0025CF70
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Creature.Side)
			{
				CardPile pile = PileType.Draw.GetPile(base.Owner);
				if (pile.Cards.FirstOrDefault<CardModel>() == this)
				{
					await CardPileCmd.AutoPlayFromDrawPile(choiceContext, base.Owner, 1, CardPilePosition.Top, false);
				}
			}
		}

		// Token: 0x06006C49 RID: 27721 RVA: 0x0025EDC3 File Offset: 0x0025CFC3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
