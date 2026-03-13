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
	// Token: 0x020009AA RID: 2474
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KinglyPunch : CardModel
	{
		// Token: 0x06006CA7 RID: 27815 RVA: 0x0025F889 File Offset: 0x0025DA89
		public KinglyPunch()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D17 RID: 7447
		// (get) Token: 0x06006CA8 RID: 27816 RVA: 0x0025F896 File Offset: 0x0025DA96
		// (set) Token: 0x06006CA9 RID: 27817 RVA: 0x0025F89E File Offset: 0x0025DA9E
		private decimal ExtraDamage
		{
			get
			{
				return this._extraDamage;
			}
			set
			{
				base.AssertMutable();
				this._extraDamage = value;
			}
		}

		// Token: 0x17001D18 RID: 7448
		// (get) Token: 0x06006CAA RID: 27818 RVA: 0x0025F8AD File Offset: 0x0025DAAD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(8m, ValueProp.Move),
					new DynamicVar("Increase", 3m)
				});
			}
		}

		// Token: 0x06006CAB RID: 27819 RVA: 0x0025F8DC File Offset: 0x0025DADC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006CAC RID: 27820 RVA: 0x0025F930 File Offset: 0x0025DB30
		public override Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (card != this)
			{
				return Task.CompletedTask;
			}
			decimal baseValue = base.DynamicVars["Increase"].BaseValue;
			base.DynamicVars.Damage.BaseValue += baseValue;
			this.ExtraDamage += baseValue;
			return Task.CompletedTask;
		}

		// Token: 0x06006CAD RID: 27821 RVA: 0x0025F990 File Offset: 0x0025DB90
		protected override void OnUpgrade()
		{
			base.DynamicVars["Increase"].UpgradeValueBy(2m);
		}

		// Token: 0x06006CAE RID: 27822 RVA: 0x0025F9AD File Offset: 0x0025DBAD
		protected override void AfterDowngraded()
		{
			base.AfterDowngraded();
			base.DynamicVars.Damage.BaseValue += this.ExtraDamage;
		}

		// Token: 0x04002591 RID: 9617
		private const string _increaseKey = "Increase";

		// Token: 0x04002592 RID: 9618
		private decimal _extraDamage;
	}
}
