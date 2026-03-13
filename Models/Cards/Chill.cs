using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008DE RID: 2270
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Chill : CardModel
	{
		// Token: 0x06006867 RID: 26727 RVA: 0x002574A7 File Offset: 0x002556A7
		public Chill()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B42 RID: 6978
		// (get) Token: 0x06006868 RID: 26728 RVA: 0x002574B4 File Offset: 0x002556B4
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001B43 RID: 6979
		// (get) Token: 0x06006869 RID: 26729 RVA: 0x002574BC File Offset: 0x002556BC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<FrostOrb>()
				});
			}
		}

		// Token: 0x0600686A RID: 26730 RVA: 0x002574E0 File Offset: 0x002556E0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			IReadOnlyList<Creature> hittableEnemies = base.CombatState.HittableEnemies;
			foreach (Creature creature in hittableEnemies)
			{
				await OrbCmd.Channel<FrostOrb>(choiceContext, base.Owner);
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x0600686B RID: 26731 RVA: 0x0025752B File Offset: 0x0025572B
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}
