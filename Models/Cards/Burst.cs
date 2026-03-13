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
	// Token: 0x020008CC RID: 2252
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Burst : CardModel
	{
		// Token: 0x06006819 RID: 26649 RVA: 0x00256B8E File Offset: 0x00254D8E
		public Burst()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B27 RID: 6951
		// (get) Token: 0x0600681A RID: 26650 RVA: 0x00256B9B File Offset: 0x00254D9B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Skills", 1m));
			}
		}

		// Token: 0x0600681B RID: 26651 RVA: 0x00256BB4 File Offset: 0x00254DB4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<BurstPower>(base.Owner.Creature, base.DynamicVars["Skills"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600681C RID: 26652 RVA: 0x00256BF7 File Offset: 0x00254DF7
		protected override void OnUpgrade()
		{
			base.DynamicVars["Skills"].UpgradeValueBy(1m);
		}

		// Token: 0x04002560 RID: 9568
		private const string _skills = "Skills";
	}
}
