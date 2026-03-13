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
	// Token: 0x02000A5F RID: 2655
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Sow : CardModel
	{
		// Token: 0x0600708A RID: 28810 RVA: 0x0026756E File Offset: 0x0026576E
		public Sow()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001EB8 RID: 7864
		// (get) Token: 0x0600708B RID: 28811 RVA: 0x0026757B File Offset: 0x0026577B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(8m, ValueProp.Move));
			}
		}

		// Token: 0x17001EB9 RID: 7865
		// (get) Token: 0x0600708C RID: 28812 RVA: 0x0026758E File Offset: 0x0026578E
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Retain);
			}
		}

		// Token: 0x0600708D RID: 28813 RVA: 0x00267598 File Offset: 0x00265798
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_blunt", null, "heavy_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x0600708E RID: 28814 RVA: 0x002675E3 File Offset: 0x002657E3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
