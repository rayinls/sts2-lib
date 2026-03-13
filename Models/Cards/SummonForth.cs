using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A7D RID: 2685
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SummonForth : CardModel
	{
		// Token: 0x06007124 RID: 28964 RVA: 0x002689F0 File Offset: 0x00266BF0
		public SummonForth()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001EF0 RID: 7920
		// (get) Token: 0x06007125 RID: 28965 RVA: 0x002689FD File Offset: 0x00266BFD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new ForgeVar(8));
			}
		}

		// Token: 0x17001EF1 RID: 7921
		// (get) Token: 0x06007126 RID: 28966 RVA: 0x00268A0A File Offset: 0x00266C0A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge().Concat(new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Retain)));
			}
		}

		// Token: 0x06007127 RID: 28967 RVA: 0x00268A24 File Offset: 0x00266C24
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await ForgeCmd.Forge(base.DynamicVars.Forge.IntValue, base.Owner, this);
			IEnumerable<SovereignBlade> enumerable = (from c in base.Owner.PlayerCombatState.AllCards.OfType<SovereignBlade>()
				where c.Pile.Type != PileType.Hand
				select c).ToList<SovereignBlade>();
			foreach (SovereignBlade sovereignBlade in enumerable)
			{
				await CardPileCmd.Add(sovereignBlade, PileType.Hand, CardPilePosition.Bottom, null, false);
			}
			IEnumerator<SovereignBlade> enumerator = null;
		}

		// Token: 0x06007128 RID: 28968 RVA: 0x00268A67 File Offset: 0x00266C67
		protected override void OnUpgrade()
		{
			base.DynamicVars.Forge.UpgradeValueBy(3m);
		}
	}
}
