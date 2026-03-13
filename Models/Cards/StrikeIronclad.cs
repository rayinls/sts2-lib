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
	// Token: 0x02000A77 RID: 2679
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StrikeIronclad : CardModel
	{
		// Token: 0x06007108 RID: 28936 RVA: 0x0026865F File Offset: 0x0026685F
		public StrikeIronclad()
			: base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EE6 RID: 7910
		// (get) Token: 0x06007109 RID: 28937 RVA: 0x0026866C File Offset: 0x0026686C
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001EE7 RID: 7911
		// (get) Token: 0x0600710A RID: 28938 RVA: 0x0026867B File Offset: 0x0026687B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(6m, ValueProp.Move));
			}
		}

		// Token: 0x0600710B RID: 28939 RVA: 0x00268690 File Offset: 0x00266890
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x0600710C RID: 28940 RVA: 0x002686E3 File Offset: 0x002668E3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
