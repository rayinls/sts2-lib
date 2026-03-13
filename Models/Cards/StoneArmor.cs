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
	// Token: 0x02000A71 RID: 2673
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StoneArmor : CardModel
	{
		// Token: 0x060070EE RID: 28910 RVA: 0x002682F0 File Offset: 0x002664F0
		public StoneArmor()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001EDD RID: 7901
		// (get) Token: 0x060070EF RID: 28911 RVA: 0x002682FD File Offset: 0x002664FD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PlatingPower>(4m));
			}
		}

		// Token: 0x17001EDE RID: 7902
		// (get) Token: 0x060070F0 RID: 28912 RVA: 0x0026830F File Offset: 0x0026650F
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

		// Token: 0x060070F1 RID: 28913 RVA: 0x00268334 File Offset: 0x00266534
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<PlatingPower>(base.Owner.Creature, base.DynamicVars["PlatingPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060070F2 RID: 28914 RVA: 0x00268377 File Offset: 0x00266577
		protected override void OnUpgrade()
		{
			base.DynamicVars["PlatingPower"].UpgradeValueBy(2m);
		}
	}
}
