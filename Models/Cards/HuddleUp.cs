using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000994 RID: 2452
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HuddleUp : CardModel
	{
		// Token: 0x06006C39 RID: 27705 RVA: 0x0025EB93 File Offset: 0x0025CD93
		public HuddleUp()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllAllies, true)
		{
		}

		// Token: 0x17001CEC RID: 7404
		// (get) Token: 0x06006C3A RID: 27706 RVA: 0x0025EBA0 File Offset: 0x0025CDA0
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001CED RID: 7405
		// (get) Token: 0x06006C3B RID: 27707 RVA: 0x0025EBA3 File Offset: 0x0025CDA3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x06006C3C RID: 27708 RVA: 0x0025EBB0 File Offset: 0x0025CDB0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<Creature> enumerable = from c in base.CombatState.GetTeammatesOf(base.Owner.Creature)
				where c != null && c.IsAlive && c.IsPlayer
				select c;
			foreach (Creature creature in enumerable)
			{
				await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, creature.Player, false);
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06006C3D RID: 27709 RVA: 0x0025EBFB File Offset: 0x0025CDFB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
