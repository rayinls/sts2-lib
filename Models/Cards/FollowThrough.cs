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
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200095A RID: 2394
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FollowThrough : CardModel
	{
		// Token: 0x06006B03 RID: 27395 RVA: 0x0025C495 File Offset: 0x0025A695
		public FollowThrough()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001C6A RID: 7274
		// (get) Token: 0x06006B04 RID: 27396 RVA: 0x0025C4A2 File Offset: 0x0025A6A2
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return this.WasLastCardPlayedSkill;
			}
		}

		// Token: 0x17001C6B RID: 7275
		// (get) Token: 0x06006B05 RID: 27397 RVA: 0x0025C4AA File Offset: 0x0025A6AA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(6m, ValueProp.Move),
					new PowerVar<WeakPower>(1m)
				});
			}
		}

		// Token: 0x17001C6C RID: 7276
		// (get) Token: 0x06006B06 RID: 27398 RVA: 0x0025C4D3 File Offset: 0x0025A6D3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x06006B07 RID: 27399 RVA: 0x0025C4E0 File Offset: 0x0025A6E0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			if (this.WasLastCardPlayedSkill)
			{
				await PowerCmd.Apply<WeakPower>(base.CombatState.HittableEnemies, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
			}
		}

		// Token: 0x06006B08 RID: 27400 RVA: 0x0025C52B File Offset: 0x0025A72B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars.Weak.UpgradeValueBy(1m);
		}

		// Token: 0x17001C6D RID: 7277
		// (get) Token: 0x06006B09 RID: 27401 RVA: 0x0025C558 File Offset: 0x0025A758
		private bool WasLastCardPlayedSkill
		{
			get
			{
				CardPlayStartedEntry cardPlayStartedEntry = CombatManager.Instance.History.CardPlaysStarted.LastOrDefault((CardPlayStartedEntry e) => e.CardPlay.Card.Owner == base.Owner && e.HappenedThisTurn(base.CombatState) && e.CardPlay.Card != this);
				return cardPlayStartedEntry != null && cardPlayStartedEntry.CardPlay.Card.Type == CardType.Skill;
			}
		}
	}
}
