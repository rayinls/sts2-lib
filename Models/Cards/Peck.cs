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
	// Token: 0x020009F6 RID: 2550
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Peck : CardModel
	{
		// Token: 0x06006E4F RID: 28239 RVA: 0x00262EB5 File Offset: 0x002610B5
		public Peck()
			: base(1, CardType.Attack, CardRarity.Event, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DCA RID: 7626
		// (get) Token: 0x06006E50 RID: 28240 RVA: 0x00262EC2 File Offset: 0x002610C2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(2m, ValueProp.Move),
					new RepeatVar(3)
				});
			}
		}

		// Token: 0x06006E51 RID: 28241 RVA: 0x00262EE8 File Offset: 0x002610E8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.DynamicVars.Repeat.IntValue).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006E52 RID: 28242 RVA: 0x00262F3B File Offset: 0x0026113B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Repeat.UpgradeValueBy(1m);
		}
	}
}
