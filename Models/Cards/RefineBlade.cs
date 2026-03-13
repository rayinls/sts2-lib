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
	// Token: 0x02000A20 RID: 2592
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RefineBlade : CardModel
	{
		// Token: 0x06006F22 RID: 28450 RVA: 0x00264A97 File Offset: 0x00262C97
		public RefineBlade()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001E1C RID: 7708
		// (get) Token: 0x06006F23 RID: 28451 RVA: 0x00264AA4 File Offset: 0x00262CA4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new ForgeVar(6),
					new EnergyVar(1)
				});
			}
		}

		// Token: 0x17001E1D RID: 7709
		// (get) Token: 0x06006F24 RID: 28452 RVA: 0x00264AC3 File Offset: 0x00262CC3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x06006F25 RID: 28453 RVA: 0x00264ACC File Offset: 0x00262CCC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await ForgeCmd.Forge(base.DynamicVars.Forge.IntValue, base.Owner, this);
			await PowerCmd.Apply<EnergyNextTurnPower>(base.Owner.Creature, base.DynamicVars.Energy.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006F26 RID: 28454 RVA: 0x00264B0F File Offset: 0x00262D0F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Forge.UpgradeValueBy(4m);
		}
	}
}
