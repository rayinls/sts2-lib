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
	// Token: 0x0200091E RID: 2334
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DevourLife : CardModel
	{
		// Token: 0x060069B9 RID: 27065 RVA: 0x00259CA0 File Offset: 0x00257EA0
		public DevourLife()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001BD9 RID: 7129
		// (get) Token: 0x060069BA RID: 27066 RVA: 0x00259CAD File Offset: 0x00257EAD
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromCard<Soul>(false),
					HoverTipFactory.Static(StaticHoverTip.SummonStatic, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x17001BDA RID: 7130
		// (get) Token: 0x060069BB RID: 27067 RVA: 0x00259CD2 File Offset: 0x00257ED2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<DevourLifePower>(1m));
			}
		}

		// Token: 0x060069BC RID: 27068 RVA: 0x00259CE4 File Offset: 0x00257EE4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<DevourLifePower>(base.Owner.Creature, base.DynamicVars["DevourLifePower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060069BD RID: 27069 RVA: 0x00259D27 File Offset: 0x00257F27
		protected override void OnUpgrade()
		{
			base.DynamicVars["DevourLifePower"].UpgradeValueBy(1m);
		}
	}
}
