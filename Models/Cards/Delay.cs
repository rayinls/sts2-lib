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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000918 RID: 2328
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Delay : CardModel
	{
		// Token: 0x06006999 RID: 27033 RVA: 0x00259933 File Offset: 0x00257B33
		public Delay()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001BC9 RID: 7113
		// (get) Token: 0x0600699A RID: 27034 RVA: 0x00259940 File Offset: 0x00257B40
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BCA RID: 7114
		// (get) Token: 0x0600699B RID: 27035 RVA: 0x00259943 File Offset: 0x00257B43
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(11m, ValueProp.Move),
					new EnergyVar(1)
				});
			}
		}

		// Token: 0x17001BCB RID: 7115
		// (get) Token: 0x0600699C RID: 27036 RVA: 0x00259969 File Offset: 0x00257B69
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x0600699D RID: 27037 RVA: 0x00259978 File Offset: 0x00257B78
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<EnergyNextTurnPower>(base.Owner.Creature, base.DynamicVars.Energy.IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600699E RID: 27038 RVA: 0x002599C3 File Offset: 0x00257BC3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}
