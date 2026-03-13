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
	// Token: 0x02000966 RID: 2406
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GammaBlast : CardModel
	{
		// Token: 0x06006B47 RID: 27463 RVA: 0x0025CCBD File Offset: 0x0025AEBD
		public GammaBlast()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x06006B48 RID: 27464 RVA: 0x0025CCCA File Offset: 0x0025AECA
		public override int CanonicalStarCost
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x06006B49 RID: 27465 RVA: 0x0025CCCD File Offset: 0x0025AECD
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

		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x06006B4A RID: 27466 RVA: 0x0025CCEA File Offset: 0x0025AEEA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(13m, ValueProp.Move),
					new PowerVar<VulnerablePower>(2m),
					new PowerVar<WeakPower>(2m)
				});
			}
		}

		// Token: 0x06006B4B RID: 27467 RVA: 0x0025CD24 File Offset: 0x0025AF24
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_giant_horizontal_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<WeakPower>(cardPlay.Target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006B4C RID: 27468 RVA: 0x0025CD77 File Offset: 0x0025AF77
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}
	}
}
