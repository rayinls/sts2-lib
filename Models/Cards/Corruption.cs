using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008F4 RID: 2292
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Corruption : CardModel
	{
		// Token: 0x060068DD RID: 26845 RVA: 0x002583A7 File Offset: 0x002565A7
		public Corruption()
			: base(3, CardType.Power, CardRarity.Ancient, TargetType.Self, true)
		{
		}

		// Token: 0x17001B75 RID: 7029
		// (get) Token: 0x060068DE RID: 26846 RVA: 0x002583B4 File Offset: 0x002565B4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Power", 1m));
			}
		}

		// Token: 0x17001B76 RID: 7030
		// (get) Token: 0x060068DF RID: 26847 RVA: 0x002583CA File Offset: 0x002565CA
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x060068E0 RID: 26848 RVA: 0x002583D8 File Offset: 0x002565D8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			NPowerUpVfx.CreateNormal(base.Owner.Creature);
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<CorruptionPower>(base.Owner.Creature, base.DynamicVars["Power"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x0025841B File Offset: 0x0025661B
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}

		// Token: 0x04002569 RID: 9577
		private const string _powerVarName = "Power";
	}
}
