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
	// Token: 0x0200091D RID: 2333
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Devastate : CardModel
	{
		// Token: 0x060069B4 RID: 27060 RVA: 0x00259C0D File Offset: 0x00257E0D
		public Devastate()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001BD7 RID: 7127
		// (get) Token: 0x060069B5 RID: 27061 RVA: 0x00259C1A File Offset: 0x00257E1A
		public override int CanonicalStarCost
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17001BD8 RID: 7128
		// (get) Token: 0x060069B6 RID: 27062 RVA: 0x00259C1D File Offset: 0x00257E1D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(30m, ValueProp.Move));
			}
		}

		// Token: 0x060069B7 RID: 27063 RVA: 0x00259C34 File Offset: 0x00257E34
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x060069B8 RID: 27064 RVA: 0x00259C87 File Offset: 0x00257E87
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(10m);
		}
	}
}
