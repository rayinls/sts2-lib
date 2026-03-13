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
	// Token: 0x0200090A RID: 2314
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Debilitate : CardModel
	{
		// Token: 0x0600694A RID: 26954 RVA: 0x0025914F File Offset: 0x0025734F
		public Debilitate()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001BA1 RID: 7073
		// (get) Token: 0x0600694B RID: 26955 RVA: 0x0025915C File Offset: 0x0025735C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<VulnerablePower>(),
					HoverTipFactory.FromPower<WeakPower>()
				});
			}
		}

		// Token: 0x17001BA2 RID: 7074
		// (get) Token: 0x0600694C RID: 26956 RVA: 0x00259179 File Offset: 0x00257379
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(7m, ValueProp.Move),
					new PowerVar<DebilitatePower>(3m)
				});
			}
		}

		// Token: 0x0600694D RID: 26957 RVA: 0x002591A4 File Offset: 0x002573A4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<DebilitatePower>(cardPlay.Target, base.DynamicVars["DebilitatePower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600694E RID: 26958 RVA: 0x002591F7 File Offset: 0x002573F7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars["DebilitatePower"].UpgradeValueBy(1m);
		}
	}
}
