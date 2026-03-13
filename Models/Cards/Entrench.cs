using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000937 RID: 2359
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Entrench : CardModel
	{
		// Token: 0x06006A3F RID: 27199 RVA: 0x0025AB15 File Offset: 0x00258D15
		public Entrench()
			: base(2, CardType.Skill, CardRarity.Event, TargetType.Self, true)
		{
		}

		// Token: 0x17001C17 RID: 7191
		// (get) Token: 0x06006A40 RID: 27200 RVA: 0x0025AB22 File Offset: 0x00258D22
		public override CardPoolModel VisualCardPool
		{
			get
			{
				return ModelDb.CardPool<IroncladCardPool>();
			}
		}

		// Token: 0x17001C18 RID: 7192
		// (get) Token: 0x06006A41 RID: 27201 RVA: 0x0025AB29 File Offset: 0x00258D29
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x0025AB2C File Offset: 0x00258D2C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.Owner.Creature.Block, ValueProp.Unpowered | ValueProp.Move, cardPlay, false);
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x0025AB77 File Offset: 0x00258D77
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
