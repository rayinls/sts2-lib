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
	// Token: 0x02000942 RID: 2370
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Exterminate : CardModel
	{
		// Token: 0x06006A7C RID: 27260 RVA: 0x0025B20F File Offset: 0x0025940F
		public Exterminate()
			: base(1, CardType.Attack, CardRarity.Event, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001C32 RID: 7218
		// (get) Token: 0x06006A7D RID: 27261 RVA: 0x0025B21C File Offset: 0x0025941C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(3m, ValueProp.Move),
					new RepeatVar(4)
				});
			}
		}

		// Token: 0x06006A7E RID: 27262 RVA: 0x0025B244 File Offset: 0x00259444
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.DynamicVars.Repeat.IntValue).FromCard(this)
				.TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006A7F RID: 27263 RVA: 0x0025B28F File Offset: 0x0025948F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
		}
	}
}
