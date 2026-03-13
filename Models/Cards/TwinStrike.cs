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
	// Token: 0x02000AA6 RID: 2726
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TwinStrike : CardModel
	{
		// Token: 0x060071FF RID: 29183 RVA: 0x0026A3FE File Offset: 0x002685FE
		public TwinStrike()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F48 RID: 8008
		// (get) Token: 0x06007200 RID: 29184 RVA: 0x0026A40B File Offset: 0x0026860B
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001F49 RID: 8009
		// (get) Token: 0x06007201 RID: 29185 RVA: 0x0026A41A File Offset: 0x0026861A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(5m, ValueProp.Move));
			}
		}

		// Token: 0x06007202 RID: 29186 RVA: 0x0026A430 File Offset: 0x00268630
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(2).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06007203 RID: 29187 RVA: 0x0026A483 File Offset: 0x00268683
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}
