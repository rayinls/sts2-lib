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
	// Token: 0x02000A78 RID: 2680
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StrikeNecrobinder : CardModel
	{
		// Token: 0x0600710D RID: 28941 RVA: 0x002686FB File Offset: 0x002668FB
		public StrikeNecrobinder()
			: base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EE8 RID: 7912
		// (get) Token: 0x0600710E RID: 28942 RVA: 0x00268708 File Offset: 0x00266908
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001EE9 RID: 7913
		// (get) Token: 0x0600710F RID: 28943 RVA: 0x00268717 File Offset: 0x00266917
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(6m, ValueProp.Move));
			}
		}

		// Token: 0x06007110 RID: 28944 RVA: 0x0026872C File Offset: 0x0026692C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06007111 RID: 28945 RVA: 0x0026877F File Offset: 0x0026697F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
