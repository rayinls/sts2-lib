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
	// Token: 0x020009A9 RID: 2473
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KinglyKick : CardModel
	{
		// Token: 0x06006CA2 RID: 27810 RVA: 0x0025F7DC File Offset: 0x0025D9DC
		public KinglyKick()
			: base(4, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D16 RID: 7446
		// (get) Token: 0x06006CA3 RID: 27811 RVA: 0x0025F7E9 File Offset: 0x0025D9E9
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(24m, ValueProp.Move));
			}
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x0025F800 File Offset: 0x0025DA00
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, "heavy_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006CA5 RID: 27813 RVA: 0x0025F853 File Offset: 0x0025DA53
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(6m);
		}

		// Token: 0x06006CA6 RID: 27814 RVA: 0x0025F86B File Offset: 0x0025DA6B
		public override Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (card != this)
			{
				return Task.CompletedTask;
			}
			base.EnergyCost.AddThisCombat(-1, false);
			return Task.CompletedTask;
		}
	}
}
