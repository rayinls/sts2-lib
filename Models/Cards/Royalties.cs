using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A2F RID: 2607
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Royalties : CardModel
	{
		// Token: 0x06006F7A RID: 28538 RVA: 0x00265560 File Offset: 0x00263760
		public Royalties()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E43 RID: 7747
		// (get) Token: 0x06006F7B RID: 28539 RVA: 0x0026556D File Offset: 0x0026376D
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001E44 RID: 7748
		// (get) Token: 0x06006F7C RID: 28540 RVA: 0x00265570 File Offset: 0x00263770
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(30));
			}
		}

		// Token: 0x06006F7D RID: 28541 RVA: 0x00265580 File Offset: 0x00263780
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<RoyaltiesPower>(base.Owner.Creature, base.DynamicVars.Gold.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006F7E RID: 28542 RVA: 0x002655C3 File Offset: 0x002637C3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Gold.UpgradeValueBy(5m);
		}
	}
}
