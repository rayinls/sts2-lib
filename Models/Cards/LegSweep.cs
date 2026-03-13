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
	// Token: 0x020009B4 RID: 2484
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LegSweep : CardModel
	{
		// Token: 0x06006CDC RID: 27868 RVA: 0x0025FF7B File Offset: 0x0025E17B
		public LegSweep()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D2D RID: 7469
		// (get) Token: 0x06006CDD RID: 27869 RVA: 0x0025FF88 File Offset: 0x0025E188
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D2E RID: 7470
		// (get) Token: 0x06006CDE RID: 27870 RVA: 0x0025FF8B File Offset: 0x0025E18B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x17001D2F RID: 7471
		// (get) Token: 0x06006CDF RID: 27871 RVA: 0x0025FF97 File Offset: 0x0025E197
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(11m, ValueProp.Move),
					new PowerVar<WeakPower>(2m)
				});
			}
		}

		// Token: 0x06006CE0 RID: 27872 RVA: 0x0025FFC4 File Offset: 0x0025E1C4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<WeakPower>(cardPlay.Target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006CE1 RID: 27873 RVA: 0x0026000F File Offset: 0x0025E20F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
			base.DynamicVars.Weak.UpgradeValueBy(1m);
		}
	}
}
