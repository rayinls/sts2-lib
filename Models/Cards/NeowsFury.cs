using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009DB RID: 2523
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NeowsFury : CardModel
	{
		// Token: 0x06006DC2 RID: 28098 RVA: 0x00261D80 File Offset: 0x0025FF80
		public NeowsFury()
			: base(1, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D8F RID: 7567
		// (get) Token: 0x06006DC3 RID: 28099 RVA: 0x00261D8D File Offset: 0x0025FF8D
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D90 RID: 7568
		// (get) Token: 0x06006DC4 RID: 28100 RVA: 0x00261D90 File Offset: 0x0025FF90
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001D91 RID: 7569
		// (get) Token: 0x06006DC5 RID: 28101 RVA: 0x00261D98 File Offset: 0x0025FF98
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(10m, ValueProp.Move),
					new CardsVar(2)
				});
			}
		}

		// Token: 0x06006DC6 RID: 28102 RVA: 0x00261DC0 File Offset: 0x0025FFC0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			IEnumerable<CardModel> enumerable = PileType.Discard.GetPile(base.Owner).Cards.TakeRandom(base.DynamicVars.Cards.IntValue, base.Owner.RunState.Rng.CombatCardSelection);
			foreach (CardModel cardModel in enumerable)
			{
				await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x06006DC7 RID: 28103 RVA: 0x00261E13 File Offset: 0x00260013
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}
