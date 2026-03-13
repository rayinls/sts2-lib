using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000951 RID: 2385
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Fisticuffs : CardModel
	{
		// Token: 0x06006AD0 RID: 27344 RVA: 0x0025BD5F File Offset: 0x00259F5F
		public Fisticuffs()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C57 RID: 7255
		// (get) Token: 0x06006AD1 RID: 27345 RVA: 0x0025BD6C File Offset: 0x00259F6C
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C58 RID: 7256
		// (get) Token: 0x06006AD2 RID: 27346 RVA: 0x0025BD6F File Offset: 0x00259F6F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x06006AD3 RID: 27347 RVA: 0x0025BD84 File Offset: 0x00259F84
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			AttackCommand attackCommand = await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			AttackCommand attackCommand2 = attackCommand;
			await CreatureCmd.GainBlock(base.Owner.Creature, attackCommand2.Results.Sum((DamageResult r) => r.TotalDamage + r.OverkillDamage), ValueProp.Move, cardPlay, false);
		}

		// Token: 0x06006AD4 RID: 27348 RVA: 0x0025BDD7 File Offset: 0x00259FD7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}
