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
	// Token: 0x02000A18 RID: 2584
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Rampage : CardModel
	{
		// Token: 0x06006EF7 RID: 28407 RVA: 0x00264527 File Offset: 0x00262727
		public Rampage()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E0B RID: 7691
		// (get) Token: 0x06006EF8 RID: 28408 RVA: 0x00264534 File Offset: 0x00262734
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(9m, ValueProp.Move),
					new DynamicVar("Increase", 5m)
				});
			}
		}

		// Token: 0x17001E0C RID: 7692
		// (get) Token: 0x06006EF9 RID: 28409 RVA: 0x00264564 File Offset: 0x00262764
		// (set) Token: 0x06006EFA RID: 28410 RVA: 0x0026456C File Offset: 0x0026276C
		private decimal ExtraDamageFromPlays
		{
			get
			{
				return this._extraDamageFromPlays;
			}
			set
			{
				base.AssertMutable();
				this._extraDamageFromPlays = value;
			}
		}

		// Token: 0x06006EFB RID: 28411 RVA: 0x0026457C File Offset: 0x0026277C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			base.DynamicVars.Damage.BaseValue += base.DynamicVars["Increase"].BaseValue;
			this.ExtraDamageFromPlays += base.DynamicVars["Increase"].BaseValue;
		}

		// Token: 0x06006EFC RID: 28412 RVA: 0x002645CF File Offset: 0x002627CF
		protected override void AfterDowngraded()
		{
			base.AfterDowngraded();
			base.DynamicVars.Damage.BaseValue += this.ExtraDamageFromPlays;
		}

		// Token: 0x06006EFD RID: 28413 RVA: 0x002645F8 File Offset: 0x002627F8
		protected override void OnUpgrade()
		{
			base.DynamicVars["Increase"].UpgradeValueBy(4m);
		}

		// Token: 0x040025C3 RID: 9667
		private const string _increaseKey = "Increase";

		// Token: 0x040025C4 RID: 9668
		private decimal _extraDamageFromPlays;
	}
}
