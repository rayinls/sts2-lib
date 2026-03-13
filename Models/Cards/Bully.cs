using System;
using System.Collections.Generic;
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
	// Token: 0x020008C7 RID: 2247
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Bully : CardModel
	{
		// Token: 0x060067FD RID: 26621 RVA: 0x00256865 File Offset: 0x00254A65
		public Bully()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B19 RID: 6937
		// (get) Token: 0x060067FE RID: 26622 RVA: 0x00256874 File Offset: 0x00254A74
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(4m);
				array[1] = new ExtraDamageVar(2m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel _, [Nullable(2)] Creature target) => (target != null) ? target.GetPowerAmount<VulnerablePower>() : 0);
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x17001B1A RID: 6938
		// (get) Token: 0x060067FF RID: 26623 RVA: 0x002568D5 File Offset: 0x00254AD5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x06006800 RID: 26624 RVA: 0x002568E4 File Offset: 0x00254AE4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006801 RID: 26625 RVA: 0x00256937 File Offset: 0x00254B37
		protected override void OnUpgrade()
		{
			base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
		}
	}
}
