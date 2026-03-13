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
	// Token: 0x02000A79 RID: 2681
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StrikeRegent : CardModel
	{
		// Token: 0x06007112 RID: 28946 RVA: 0x00268797 File Offset: 0x00266997
		public StrikeRegent()
			: base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EEA RID: 7914
		// (get) Token: 0x06007113 RID: 28947 RVA: 0x002687A4 File Offset: 0x002669A4
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001EEB RID: 7915
		// (get) Token: 0x06007114 RID: 28948 RVA: 0x002687B3 File Offset: 0x002669B3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(6m, ValueProp.Move));
			}
		}

		// Token: 0x06007115 RID: 28949 RVA: 0x002687C8 File Offset: 0x002669C8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06007116 RID: 28950 RVA: 0x0026881B File Offset: 0x00266A1B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
