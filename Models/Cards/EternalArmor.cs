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
	// Token: 0x0200093D RID: 2365
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EternalArmor : CardModel
	{
		// Token: 0x06006A5F RID: 27231 RVA: 0x0025AE63 File Offset: 0x00259063
		public EternalArmor()
			: base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001C25 RID: 7205
		// (get) Token: 0x06006A60 RID: 27232 RVA: 0x0025AE70 File Offset: 0x00259070
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<PlatingPower>(),
					HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x17001C26 RID: 7206
		// (get) Token: 0x06006A61 RID: 27233 RVA: 0x0025AE93 File Offset: 0x00259093
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PlatingPower>(7m));
			}
		}

		// Token: 0x06006A62 RID: 27234 RVA: 0x0025AEA8 File Offset: 0x002590A8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<PlatingPower>(base.Owner.Creature, base.DynamicVars["PlatingPower"].IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006A63 RID: 27235 RVA: 0x0025AEEB File Offset: 0x002590EB
		protected override void OnUpgrade()
		{
			base.DynamicVars["PlatingPower"].UpgradeValueBy(2m);
		}
	}
}
