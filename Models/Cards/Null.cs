using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009E4 RID: 2532
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Null : CardModel
	{
		// Token: 0x06006DF3 RID: 28147 RVA: 0x002623AF File Offset: 0x002605AF
		public Null()
			: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DA5 RID: 7589
		// (get) Token: 0x06006DF4 RID: 28148 RVA: 0x002623BC File Offset: 0x002605BC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<WeakPower>(),
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<DarkOrb>()
				});
			}
		}

		// Token: 0x17001DA6 RID: 7590
		// (get) Token: 0x06006DF5 RID: 28149 RVA: 0x002623E7 File Offset: 0x002605E7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(10m, ValueProp.Move),
					new PowerVar<WeakPower>(2m)
				});
			}
		}

		// Token: 0x06006DF6 RID: 28150 RVA: 0x00262414 File Offset: 0x00260614
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<WeakPower>(cardPlay.Target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
			await OrbCmd.Channel<DarkOrb>(choiceContext, base.Owner);
		}

		// Token: 0x06006DF7 RID: 28151 RVA: 0x00262467 File Offset: 0x00260667
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
			base.DynamicVars.Weak.UpgradeValueBy(1m);
		}
	}
}
