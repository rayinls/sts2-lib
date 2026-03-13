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
	// Token: 0x02000A29 RID: 2601
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Ricochet : CardModel
	{
		// Token: 0x06006F59 RID: 28505 RVA: 0x00265125 File Offset: 0x00263325
		public Ricochet()
			: base(2, CardType.Attack, CardRarity.Common, TargetType.RandomEnemy, true)
		{
		}

		// Token: 0x17001E36 RID: 7734
		// (get) Token: 0x06006F5A RID: 28506 RVA: 0x00265132 File Offset: 0x00263332
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

		// Token: 0x17001E37 RID: 7735
		// (get) Token: 0x06006F5B RID: 28507 RVA: 0x00265157 File Offset: 0x00263357
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Sly);
			}
		}

		// Token: 0x06006F5C RID: 28508 RVA: 0x00265160 File Offset: 0x00263360
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.DynamicVars.Repeat.IntValue).FromCard(this)
				.TargetingRandomOpponents(base.CombatState, true)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006F5D RID: 28509 RVA: 0x002651AB File Offset: 0x002633AB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Repeat.UpgradeValueBy(1m);
		}
	}
}
