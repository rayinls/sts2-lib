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
	// Token: 0x020008F8 RID: 2296
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CreativeAi : CardModel
	{
		// Token: 0x060068F1 RID: 26865 RVA: 0x002585D7 File Offset: 0x002567D7
		public CreativeAi()
			: base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B7D RID: 7037
		// (get) Token: 0x060068F2 RID: 26866 RVA: 0x002585E4 File Offset: 0x002567E4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("CreativeAi", 1m));
			}
		}

		// Token: 0x060068F3 RID: 26867 RVA: 0x002585FC File Offset: 0x002567FC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<CreativeAiPower>(base.Owner.Creature, base.DynamicVars["CreativeAi"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060068F4 RID: 26868 RVA: 0x0025863F File Offset: 0x0025683F
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}

		// Token: 0x0400256A RID: 9578
		private const string _creativeAiKey = "CreativeAi";
	}
}
