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
	// Token: 0x02000AB7 RID: 2743
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Volley : CardModel
	{
		// Token: 0x06007253 RID: 29267 RVA: 0x0026ADCF File Offset: 0x00268FCF
		public Volley()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.RandomEnemy, true)
		{
		}

		// Token: 0x17001F69 RID: 8041
		// (get) Token: 0x06007254 RID: 29268 RVA: 0x0026ADDC File Offset: 0x00268FDC
		protected override bool HasEnergyCostX
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F6A RID: 8042
		// (get) Token: 0x06007255 RID: 29269 RVA: 0x0026ADDF File Offset: 0x00268FDF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(10m, ValueProp.Move));
			}
		}

		// Token: 0x06007256 RID: 29270 RVA: 0x0026ADF4 File Offset: 0x00268FF4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.ResolveEnergyXValue()).FromCard(this)
				.TargetingRandomOpponents(base.CombatState, true)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06007257 RID: 29271 RVA: 0x0026AE3F File Offset: 0x0026903F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}
