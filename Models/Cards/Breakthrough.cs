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
	// Token: 0x020008C1 RID: 2241
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Breakthrough : CardModel
	{
		// Token: 0x060067E1 RID: 26593 RVA: 0x00256489 File Offset: 0x00254689
		public Breakthrough()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001B0F RID: 6927
		// (get) Token: 0x060067E2 RID: 26594 RVA: 0x00256496 File Offset: 0x00254696
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(9m, ValueProp.Move),
					new HpLossVar(1m)
				});
			}
		}

		// Token: 0x060067E3 RID: 26595 RVA: 0x002564C0 File Offset: 0x002546C0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			VfxCmd.PlayOnCreatureCenter(base.Owner.Creature, "vfx/vfx_bloody_impact");
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_blunt", null, "heavy_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x060067E4 RID: 26596 RVA: 0x0025650B File Offset: 0x0025470B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}
