using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200088D RID: 2189
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Alchemize : CardModel
	{
		// Token: 0x060066D2 RID: 26322 RVA: 0x00254274 File Offset: 0x00252474
		public Alchemize()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001A9E RID: 6814
		// (get) Token: 0x060066D3 RID: 26323 RVA: 0x00254281 File Offset: 0x00252481
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A9F RID: 6815
		// (get) Token: 0x060066D4 RID: 26324 RVA: 0x00254284 File Offset: 0x00252484
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x060066D5 RID: 26325 RVA: 0x0025428C File Offset: 0x0025248C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PotionCmd.TryToProcure(PotionFactory.CreateRandomPotionInCombat(base.Owner, base.Owner.RunState.Rng.CombatPotionGeneration, null).ToMutable(), base.Owner, -1);
		}

		// Token: 0x060066D6 RID: 26326 RVA: 0x002542CF File Offset: 0x002524CF
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
