using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A2B RID: 2603
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RipAndTear : CardModel
	{
		// Token: 0x06006F66 RID: 28518 RVA: 0x002652D7 File Offset: 0x002634D7
		public RipAndTear()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.RandomEnemy, true)
		{
		}

		// Token: 0x17001E3C RID: 7740
		// (get) Token: 0x06006F67 RID: 28519 RVA: 0x002652E4 File Offset: 0x002634E4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x17001E3D RID: 7741
		// (get) Token: 0x06006F68 RID: 28520 RVA: 0x002652F7 File Offset: 0x002634F7
		public override CardPoolModel VisualCardPool
		{
			get
			{
				return ModelDb.CardPool<DefectCardPool>();
			}
		}

		// Token: 0x06006F69 RID: 28521 RVA: 0x00265300 File Offset: 0x00263500
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(2).FromCard(this)
				.TargetingRandomOpponents(base.CombatState, true)
				.WithHitVfxNode((Creature t) => NScratchVfx.Create(t, true))
				.Execute(choiceContext);
		}

		// Token: 0x06006F6A RID: 28522 RVA: 0x0026534B File Offset: 0x0026354B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}
