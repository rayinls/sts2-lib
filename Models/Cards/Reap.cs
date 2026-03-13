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
	// Token: 0x02000A1B RID: 2587
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Reap : CardModel
	{
		// Token: 0x06006F0A RID: 28426 RVA: 0x002647CF File Offset: 0x002629CF
		public Reap()
			: base(3, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E13 RID: 7699
		// (get) Token: 0x06006F0B RID: 28427 RVA: 0x002647DC File Offset: 0x002629DC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(27m, ValueProp.Move));
			}
		}

		// Token: 0x17001E14 RID: 7700
		// (get) Token: 0x06006F0C RID: 28428 RVA: 0x002647F0 File Offset: 0x002629F0
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Retain);
			}
		}

		// Token: 0x06006F0D RID: 28429 RVA: 0x002647F8 File Offset: 0x002629F8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006F0E RID: 28430 RVA: 0x0026484B File Offset: 0x00262A4B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(6m);
		}
	}
}
