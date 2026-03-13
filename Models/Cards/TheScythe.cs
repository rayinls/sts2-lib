using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A94 RID: 2708
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheScythe : CardModel
	{
		// Token: 0x06007199 RID: 29081 RVA: 0x002698A7 File Offset: 0x00267AA7
		public TheScythe()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F20 RID: 7968
		// (get) Token: 0x0600719A RID: 29082 RVA: 0x002698BC File Offset: 0x00267ABC
		// (set) Token: 0x0600719B RID: 29083 RVA: 0x002698C4 File Offset: 0x00267AC4
		[SavedProperty]
		public int CurrentDamage
		{
			get
			{
				return this._currentDamage;
			}
			set
			{
				base.AssertMutable();
				this._currentDamage = value;
				base.DynamicVars.Damage.BaseValue = this._currentDamage;
			}
		}

		// Token: 0x17001F21 RID: 7969
		// (get) Token: 0x0600719C RID: 29084 RVA: 0x002698EE File Offset: 0x00267AEE
		// (set) Token: 0x0600719D RID: 29085 RVA: 0x002698F6 File Offset: 0x00267AF6
		[SavedProperty]
		public int IncreasedDamage
		{
			get
			{
				return this._increasedDamage;
			}
			set
			{
				base.AssertMutable();
				this._increasedDamage = value;
			}
		}

		// Token: 0x17001F22 RID: 7970
		// (get) Token: 0x0600719E RID: 29086 RVA: 0x00269905 File Offset: 0x00267B05
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001F23 RID: 7971
		// (get) Token: 0x0600719F RID: 29087 RVA: 0x0026990D File Offset: 0x00267B0D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(this.CurrentDamage, ValueProp.Move),
					new IntVar("Increase", 3m)
				});
			}
		}

		// Token: 0x060071A0 RID: 29088 RVA: 0x00269944 File Offset: 0x00267B44
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			int intValue = base.DynamicVars["Increase"].IntValue;
			this.BuffFromPlay(intValue);
			TheScythe theScythe = base.DeckVersion as TheScythe;
			if (theScythe != null)
			{
				theScythe.BuffFromPlay(intValue);
			}
		}

		// Token: 0x060071A1 RID: 29089 RVA: 0x00269997 File Offset: 0x00267B97
		protected override void OnUpgrade()
		{
			base.DynamicVars["Increase"].UpgradeValueBy(1m);
		}

		// Token: 0x060071A2 RID: 29090 RVA: 0x002699B3 File Offset: 0x00267BB3
		protected override void AfterDowngraded()
		{
			this.UpdateDamage();
		}

		// Token: 0x060071A3 RID: 29091 RVA: 0x002699BB File Offset: 0x00267BBB
		private void BuffFromPlay(int extraDamage)
		{
			this.IncreasedDamage += extraDamage;
			this.UpdateDamage();
		}

		// Token: 0x060071A4 RID: 29092 RVA: 0x002699D1 File Offset: 0x00267BD1
		private void UpdateDamage()
		{
			this.CurrentDamage = 13 + this.IncreasedDamage;
		}

		// Token: 0x040025DC RID: 9692
		private const string _increaseKey = "Increase";

		// Token: 0x040025DD RID: 9693
		private const int _baseDamage = 13;

		// Token: 0x040025DE RID: 9694
		private int _currentDamage = 13;

		// Token: 0x040025DF RID: 9695
		private int _increasedDamage;
	}
}
