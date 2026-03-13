using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000958 RID: 2392
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FlickFlack : CardModel
	{
		// Token: 0x06006AF8 RID: 27384 RVA: 0x0025C32F File Offset: 0x0025A52F
		public FlickFlack()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001C65 RID: 7269
		// (get) Token: 0x06006AF9 RID: 27385 RVA: 0x0025C33C File Offset: 0x0025A53C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x17001C66 RID: 7270
		// (get) Token: 0x06006AFA RID: 27386 RVA: 0x0025C34F File Offset: 0x0025A54F
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Sly);
			}
		}

		// Token: 0x06006AFB RID: 27387 RVA: 0x0025C358 File Offset: 0x0025A558
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Attack", base.Owner.Character.AttackAnimDelay);
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006AFC RID: 27388 RVA: 0x0025C3A3 File Offset: 0x0025A5A3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}
