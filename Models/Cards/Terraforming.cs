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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A8F RID: 2703
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Terraforming : CardModel
	{
		// Token: 0x0600717F RID: 29055 RVA: 0x002695AB File Offset: 0x002677AB
		public Terraforming()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001F15 RID: 7957
		// (get) Token: 0x06007180 RID: 29056 RVA: 0x002695B8 File Offset: 0x002677B8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<VigorPower>(6m));
			}
		}

		// Token: 0x17001F16 RID: 7958
		// (get) Token: 0x06007181 RID: 29057 RVA: 0x002695CA File Offset: 0x002677CA
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VigorPower>());
			}
		}

		// Token: 0x06007182 RID: 29058 RVA: 0x002695D8 File Offset: 0x002677D8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<VigorPower>(base.Owner.Creature, base.DynamicVars["VigorPower"].IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06007183 RID: 29059 RVA: 0x0026961B File Offset: 0x0026781B
		protected override void OnUpgrade()
		{
			base.DynamicVars["VigorPower"].UpgradeValueBy(2m);
		}
	}
}
