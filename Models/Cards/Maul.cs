using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009C3 RID: 2499
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Maul : CardModel
	{
		// Token: 0x06006D3E RID: 27966 RVA: 0x00260CE9 File Offset: 0x0025EEE9
		public Maul()
			: base(1, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D56 RID: 7510
		// (get) Token: 0x06006D3F RID: 27967 RVA: 0x00260CF6 File Offset: 0x0025EEF6
		// (set) Token: 0x06006D40 RID: 27968 RVA: 0x00260CFE File Offset: 0x0025EEFE
		private decimal ExtraDamageFromMaulPlays
		{
			get
			{
				return this._extraDamageFromMaulPlays;
			}
			set
			{
				base.AssertMutable();
				this._extraDamageFromMaulPlays = value;
			}
		}

		// Token: 0x17001D57 RID: 7511
		// (get) Token: 0x06006D41 RID: 27969 RVA: 0x00260D0D File Offset: 0x0025EF0D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(5m, ValueProp.Move),
					new DynamicVar("Increase", 1m)
				});
			}
		}

		// Token: 0x06006D42 RID: 27970 RVA: 0x00260D3C File Offset: 0x0025EF3C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(2).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitVfxNode((Creature t) => NScratchVfx.Create(t, true))
				.Execute(choiceContext);
			IEnumerable<Maul> enumerable = base.Owner.PlayerCombatState.AllCards.OfType<Maul>();
			decimal baseValue = base.DynamicVars["Increase"].BaseValue;
			foreach (Maul maul in enumerable)
			{
				maul.BuffFromMaulPlay(baseValue);
			}
		}

		// Token: 0x06006D43 RID: 27971 RVA: 0x00260D8F File Offset: 0x0025EF8F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars["Increase"].UpgradeValueBy(1m);
		}

		// Token: 0x06006D44 RID: 27972 RVA: 0x00260DC0 File Offset: 0x0025EFC0
		protected override void AfterDowngraded()
		{
			base.AfterDowngraded();
			base.DynamicVars.Damage.BaseValue += this.ExtraDamageFromMaulPlays;
		}

		// Token: 0x06006D45 RID: 27973 RVA: 0x00260DE9 File Offset: 0x0025EFE9
		private void BuffFromMaulPlay(decimal extraDamage)
		{
			base.DynamicVars.Damage.BaseValue += extraDamage;
			this.ExtraDamageFromMaulPlays += extraDamage;
		}

		// Token: 0x040025B0 RID: 9648
		private const string _increaseKey = "Increase";

		// Token: 0x040025B1 RID: 9649
		private decimal _extraDamageFromMaulPlays;
	}
}
