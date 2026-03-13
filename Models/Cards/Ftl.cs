using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000962 RID: 2402
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Ftl : CardModel
	{
		// Token: 0x06006B31 RID: 27441 RVA: 0x0025C9CF File Offset: 0x0025ABCF
		public Ftl()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x06006B32 RID: 27442 RVA: 0x0025C9DC File Offset: 0x0025ABDC
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return this.CanDrawCard;
			}
		}

		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x06006B33 RID: 27443 RVA: 0x0025C9E4 File Offset: 0x0025ABE4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(5m, ValueProp.Move),
					new IntVar("PlayMax", 3m),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x06006B34 RID: 27444 RVA: 0x0025CA1C File Offset: 0x0025AC1C
		private bool CanDrawCard
		{
			get
			{
				int num = CombatManager.Instance.History.CardPlaysFinished.Count((CardPlayFinishedEntry e) => e.HappenedThisTurn(base.CombatState) && e.CardPlay.Card.Owner == base.Owner);
				return num < base.DynamicVars["PlayMax"].IntValue;
			}
		}

		// Token: 0x06006B35 RID: 27445 RVA: 0x0025CA64 File Offset: 0x0025AC64
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			if (this.CanDrawCard)
			{
				await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
			}
		}

		// Token: 0x06006B36 RID: 27446 RVA: 0x0025CAB7 File Offset: 0x0025ACB7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars["PlayMax"].UpgradeValueBy(1m);
		}

		// Token: 0x04002581 RID: 9601
		private const string _playMaxKey = "PlayMax";
	}
}
