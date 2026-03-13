using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
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
	// Token: 0x02000922 RID: 2338
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Dismantle : CardModel
	{
		// Token: 0x060069CE RID: 27086 RVA: 0x00259EE3 File Offset: 0x002580E3
		public Dismantle()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001BE2 RID: 7138
		// (get) Token: 0x060069CF RID: 27087 RVA: 0x00259EF0 File Offset: 0x002580F0
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				CombatState combatState = base.CombatState;
				if (combatState == null)
				{
					return false;
				}
				return combatState.HittableEnemies.Any((Creature e) => e.HasPower<VulnerablePower>());
			}
		}

		// Token: 0x17001BE3 RID: 7139
		// (get) Token: 0x060069D0 RID: 27088 RVA: 0x00259F27 File Offset: 0x00258127
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(8m, ValueProp.Move));
			}
		}

		// Token: 0x17001BE4 RID: 7140
		// (get) Token: 0x060069D1 RID: 27089 RVA: 0x00259F3A File Offset: 0x0025813A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x060069D2 RID: 27090 RVA: 0x00259F48 File Offset: 0x00258148
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			int num = (cardPlay.Target.HasPower<VulnerablePower>() ? 2 : 1);
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(num).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "heavy_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x060069D3 RID: 27091 RVA: 0x00259F9B File Offset: 0x0025819B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}
