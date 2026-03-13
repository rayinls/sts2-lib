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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000943 RID: 2371
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FallingStar : CardModel
	{
		// Token: 0x06006A80 RID: 27264 RVA: 0x0025B2A6 File Offset: 0x002594A6
		public FallingStar()
			: base(0, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C33 RID: 7219
		// (get) Token: 0x06006A81 RID: 27265 RVA: 0x0025B2B3 File Offset: 0x002594B3
		public override int CanonicalStarCost
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001C34 RID: 7220
		// (get) Token: 0x06006A82 RID: 27266 RVA: 0x0025B2B6 File Offset: 0x002594B6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(7m, ValueProp.Move),
					new PowerVar<VulnerablePower>(1m),
					new PowerVar<WeakPower>(1m)
				});
			}
		}

		// Token: 0x17001C35 RID: 7221
		// (get) Token: 0x06006A83 RID: 27267 RVA: 0x0025B2EC File Offset: 0x002594EC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<WeakPower>(),
					HoverTipFactory.FromPower<VulnerablePower>()
				});
			}
		}

		// Token: 0x06006A84 RID: 27268 RVA: 0x0025B30C File Offset: 0x0025950C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_starry_impact", "blunt_attack.mp3", null)
				.Execute(choiceContext);
			await PowerCmd.Apply<WeakPower>(cardPlay.Target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006A85 RID: 27269 RVA: 0x0025B35F File Offset: 0x0025955F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}
