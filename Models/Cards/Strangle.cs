using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A74 RID: 2676
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Strangle : CardModel
	{
		// Token: 0x060070FB RID: 28923 RVA: 0x002684A3 File Offset: 0x002666A3
		public Strangle()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EE2 RID: 7906
		// (get) Token: 0x060070FC RID: 28924 RVA: 0x002684B0 File Offset: 0x002666B0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(8m, ValueProp.Move),
					new PowerVar<StranglePower>(2m)
				});
			}
		}

		// Token: 0x060070FD RID: 28925 RVA: 0x002684DC File Offset: 0x002666DC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<StranglePower>(cardPlay.Target, base.DynamicVars["StranglePower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060070FE RID: 28926 RVA: 0x0026852F File Offset: 0x0026672F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars["StranglePower"].UpgradeValueBy(1m);
		}
	}
}
