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
	// Token: 0x02000986 RID: 2438
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HeavenlyDrill : CardModel
	{
		// Token: 0x06006BF2 RID: 27634 RVA: 0x0025E20B File Offset: 0x0025C40B
		public HeavenlyDrill()
			: base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x06006BF3 RID: 27635 RVA: 0x0025E218 File Offset: 0x0025C418
		protected override bool HasEnergyCostX
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x06006BF4 RID: 27636 RVA: 0x0025E21B File Offset: 0x0025C41B
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return base.Owner.PlayerCombatState.Energy >= base.DynamicVars.Energy.IntValue;
			}
		}

		// Token: 0x17001CD2 RID: 7378
		// (get) Token: 0x06006BF5 RID: 27637 RVA: 0x0025E242 File Offset: 0x0025C442
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(8m, ValueProp.Move),
					new EnergyVar(4)
				});
			}
		}

		// Token: 0x06006BF6 RID: 27638 RVA: 0x0025E268 File Offset: 0x0025C468
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			int num = base.ResolveEnergyXValue();
			if (num >= base.DynamicVars.Energy.IntValue)
			{
				num *= 2;
			}
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(num).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_giant_horizontal_slash", null, "slash_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006BF7 RID: 27639 RVA: 0x0025E2BB File Offset: 0x0025C4BB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}
