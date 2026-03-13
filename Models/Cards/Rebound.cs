using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A1F RID: 2591
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Rebound : CardModel
	{
		// Token: 0x06006F1D RID: 28445 RVA: 0x00264A03 File Offset: 0x00262C03
		public Rebound()
			: base(1, CardType.Attack, CardRarity.Event, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E1A RID: 7706
		// (get) Token: 0x06006F1E RID: 28446 RVA: 0x00264A10 File Offset: 0x00262C10
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(9m, ValueProp.Move));
			}
		}

		// Token: 0x17001E1B RID: 7707
		// (get) Token: 0x06006F1F RID: 28447 RVA: 0x00264A24 File Offset: 0x00262C24
		public override CardPoolModel VisualCardPool
		{
			get
			{
				return ModelDb.CardPool<DefectCardPool>();
			}
		}

		// Token: 0x06006F20 RID: 28448 RVA: 0x00264A2C File Offset: 0x00262C2C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<ReboundPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006F21 RID: 28449 RVA: 0x00264A7F File Offset: 0x00262C7F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
