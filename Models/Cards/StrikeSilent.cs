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
	// Token: 0x02000A7A RID: 2682
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StrikeSilent : CardModel
	{
		// Token: 0x06007117 RID: 28951 RVA: 0x00268833 File Offset: 0x00266A33
		public StrikeSilent()
			: base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EEC RID: 7916
		// (get) Token: 0x06007118 RID: 28952 RVA: 0x00268840 File Offset: 0x00266A40
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001EED RID: 7917
		// (get) Token: 0x06007119 RID: 28953 RVA: 0x0026884F File Offset: 0x00266A4F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(6m, ValueProp.Move));
			}
		}

		// Token: 0x0600711A RID: 28954 RVA: 0x00268864 File Offset: 0x00266A64
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x0600711B RID: 28955 RVA: 0x002688B7 File Offset: 0x00266AB7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
