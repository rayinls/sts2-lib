using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000972 RID: 2418
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GoForTheEyes : CardModel
	{
		// Token: 0x06006B8C RID: 27532 RVA: 0x0025D526 File Offset: 0x0025B726
		public GoForTheEyes()
			: base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x06006B8D RID: 27533 RVA: 0x0025D533 File Offset: 0x0025B733
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				if (base.CombatState == null)
				{
					return false;
				}
				return base.CombatState.HittableEnemies.Any(delegate(Creature e)
				{
					MonsterModel monster = e.Monster;
					return monster != null && monster.IntendsToAttack;
				});
			}
		}

		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x06006B8E RID: 27534 RVA: 0x0025D56E File Offset: 0x0025B76E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x06006B8F RID: 27535 RVA: 0x0025D57A File Offset: 0x0025B77A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(3m, ValueProp.Move),
					new PowerVar<WeakPower>(1m)
				});
			}
		}

		// Token: 0x06006B90 RID: 27536 RVA: 0x0025D5A4 File Offset: 0x0025B7A4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			if (cardPlay.Target.Monster.IntendsToAttack)
			{
				await PowerCmd.Apply<WeakPower>(cardPlay.Target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
			}
		}

		// Token: 0x06006B91 RID: 27537 RVA: 0x0025D5F7 File Offset: 0x0025B7F7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars.Weak.UpgradeValueBy(1m);
		}
	}
}
