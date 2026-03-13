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
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200094B RID: 2379
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Fetch : CardModel
	{
		// Token: 0x06006AAD RID: 27309 RVA: 0x0025B8B5 File Offset: 0x00259AB5
		public Fetch()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C47 RID: 7239
		// (get) Token: 0x06006AAE RID: 27310 RVA: 0x0025B8C2 File Offset: 0x00259AC2
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x06006AAF RID: 27311 RVA: 0x0025B8D1 File Offset: 0x00259AD1
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x06006AB0 RID: 27312 RVA: 0x0025B8DE File Offset: 0x00259ADE
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return !this.HasBeenPlayedThisTurn;
			}
		}

		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x06006AB1 RID: 27313 RVA: 0x0025B8E9 File Offset: 0x00259AE9
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new OstyDamageVar(3m, ValueProp.Move),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x06006AB2 RID: 27314 RVA: 0x0025B910 File Offset: 0x00259B10
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			if (!Osty.CheckMissingWithAnim(base.Owner))
			{
				await DamageCmd.Attack(base.DynamicVars.OstyDamage.BaseValue).FromOsty(base.Owner.Osty, this).Targeting(cardPlay.Target)
					.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
					.Execute(choiceContext);
				if (!this.HasBeenPlayedThisTurn)
				{
					await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
				}
			}
		}

		// Token: 0x06006AB3 RID: 27315 RVA: 0x0025B963 File Offset: 0x00259B63
		protected override void OnUpgrade()
		{
			base.DynamicVars.OstyDamage.UpgradeValueBy(3m);
		}

		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x06006AB4 RID: 27316 RVA: 0x0025B97B File Offset: 0x00259B7B
		private bool HasBeenPlayedThisTurn
		{
			get
			{
				return CombatManager.Instance.History.CardPlaysFinished.Any((CardPlayFinishedEntry e) => e.CardPlay.Card == this && e.HappenedThisTurn(base.CombatState));
			}
		}
	}
}
