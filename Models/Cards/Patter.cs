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
	// Token: 0x020009F5 RID: 2549
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Patter : CardModel
	{
		// Token: 0x06006E49 RID: 28233 RVA: 0x00262DEF File Offset: 0x00260FEF
		public Patter()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001DC7 RID: 7623
		// (get) Token: 0x06006E4A RID: 28234 RVA: 0x00262DFC File Offset: 0x00260FFC
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001DC8 RID: 7624
		// (get) Token: 0x06006E4B RID: 28235 RVA: 0x00262DFF File Offset: 0x00260FFF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(8m, ValueProp.Move),
					new PowerVar<VigorPower>(2m)
				});
			}
		}

		// Token: 0x17001DC9 RID: 7625
		// (get) Token: 0x06006E4C RID: 28236 RVA: 0x00262E29 File Offset: 0x00261029
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VigorPower>());
			}
		}

		// Token: 0x06006E4D RID: 28237 RVA: 0x00262E38 File Offset: 0x00261038
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<VigorPower>(base.Owner.Creature, base.DynamicVars["VigorPower"].IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E4E RID: 28238 RVA: 0x00262E83 File Offset: 0x00261083
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
			base.DynamicVars["VigorPower"].UpgradeValueBy(1m);
		}
	}
}
