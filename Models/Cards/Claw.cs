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
	// Token: 0x020008E1 RID: 2273
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Claw : CardModel
	{
		// Token: 0x06006878 RID: 26744 RVA: 0x002576C3 File Offset: 0x002558C3
		public Claw()
			: base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B4A RID: 6986
		// (get) Token: 0x06006879 RID: 26745 RVA: 0x002576D0 File Offset: 0x002558D0
		// (set) Token: 0x0600687A RID: 26746 RVA: 0x002576D8 File Offset: 0x002558D8
		private decimal ExtraDamageFromClawPlays
		{
			get
			{
				return this._extraDamageFromClawPlays;
			}
			set
			{
				base.AssertMutable();
				this._extraDamageFromClawPlays = value;
			}
		}

		// Token: 0x17001B4B RID: 6987
		// (get) Token: 0x0600687B RID: 26747 RVA: 0x002576E7 File Offset: 0x002558E7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(3m, ValueProp.Move),
					new DynamicVar("Increase", 2m)
				});
			}
		}

		// Token: 0x0600687C RID: 26748 RVA: 0x00257718 File Offset: 0x00255918
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitVfxNode((Creature t) => NScratchVfx.Create(t, true))
				.Execute(choiceContext);
			IEnumerable<Claw> enumerable = base.Owner.PlayerCombatState.AllCards.OfType<Claw>();
			decimal baseValue = base.DynamicVars["Increase"].BaseValue;
			foreach (Claw claw in enumerable)
			{
				claw.BuffFromClawPlay(baseValue);
			}
		}

		// Token: 0x0600687D RID: 26749 RVA: 0x0025776B File Offset: 0x0025596B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars["Increase"].UpgradeValueBy(1m);
		}

		// Token: 0x0600687E RID: 26750 RVA: 0x0025779C File Offset: 0x0025599C
		protected override void AfterDowngraded()
		{
			base.AfterDowngraded();
			base.DynamicVars.Damage.BaseValue += this.ExtraDamageFromClawPlays;
		}

		// Token: 0x0600687F RID: 26751 RVA: 0x002577C5 File Offset: 0x002559C5
		private void BuffFromClawPlay(decimal extraDamage)
		{
			base.DynamicVars.Damage.BaseValue += extraDamage;
			this.ExtraDamageFromClawPlays += extraDamage;
		}

		// Token: 0x04002564 RID: 9572
		private const string _increaseKey = "Increase";

		// Token: 0x04002565 RID: 9573
		private decimal _extraDamageFromClawPlays;
	}
}
