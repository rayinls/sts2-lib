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
	// Token: 0x02000974 RID: 2420
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GrandFinale : CardModel
	{
		// Token: 0x06006B96 RID: 27542 RVA: 0x0025D6EC File Offset: 0x0025B8EC
		public GrandFinale()
			: base(0, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x06006B97 RID: 27543 RVA: 0x0025D6F9 File Offset: 0x0025B8F9
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return this.IsPlayable;
			}
		}

		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x06006B98 RID: 27544 RVA: 0x0025D701 File Offset: 0x0025B901
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(50m, ValueProp.Move));
			}
		}

		// Token: 0x06006B99 RID: 27545 RVA: 0x0025D718 File Offset: 0x0025B918
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_slash", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x06006B9A RID: 27546 RVA: 0x0025D763 File Offset: 0x0025B963
		protected override bool IsPlayable
		{
			get
			{
				return PileType.Draw.GetPile(base.Owner).Cards.Count == 0;
			}
		}

		// Token: 0x06006B9B RID: 27547 RVA: 0x0025D77E File Offset: 0x0025B97E
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(10m);
		}
	}
}
