using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedRampage : CardModel
	{
		public RedRampage()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// (get) Token: 0x06006EF8 RID: 28408 RVA: 0x00264534 File Offset: 0x00262734
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[]
				{
					new DamageVar(9m, ValueProp.Move),
					new DynamicVar("Increase", 5m)
				};
			}
		}

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

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			base.DynamicVars.Damage.BaseValue += base.DynamicVars["Increase"].BaseValue;
			this.ExtraDamageFromPlays += base.DynamicVars["Increase"].BaseValue;
		}

		protected override void AfterDowngraded()
		{
			base.AfterDowngraded();
			base.DynamicVars.Damage.BaseValue += this.ExtraDamageFromPlays;
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars["Increase"].UpgradeValueBy(4m);
		}

		private const string _increaseKey = "Increase";

		private decimal _extraDamageFromPlays;
	}
}
