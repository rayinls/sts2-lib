using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedThunderclap : CardModel
	{
		public RedThunderclap()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies, true)
		{
		}

		// (get) Token: 0x060071C8 RID: 29128 RVA: 0x00269DC5 File Offset: 0x00267FC5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[]
				{
					new DamageVar(4m, ValueProp.Move),
					new PowerVar<VulnerablePower>(1m)
				};
			}
		}

		// (get) Token: 0x060071C9 RID: 29129 RVA: 0x00269DEE File Offset: 0x00267FEE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.FromPower<VulnerablePower>() };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<VulnerablePower>(base.CombatState.HittableEnemies, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
