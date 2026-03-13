using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009F4 RID: 2548
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ParticleWall : CardModel
	{
		// Token: 0x06006E42 RID: 28226 RVA: 0x00262D46 File Offset: 0x00260F46
		public ParticleWall()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DC4 RID: 7620
		// (get) Token: 0x06006E43 RID: 28227 RVA: 0x00262D53 File Offset: 0x00260F53
		public override int CanonicalStarCost
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001DC5 RID: 7621
		// (get) Token: 0x06006E44 RID: 28228 RVA: 0x00262D56 File Offset: 0x00260F56
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001DC6 RID: 7622
		// (get) Token: 0x06006E45 RID: 28229 RVA: 0x00262D59 File Offset: 0x00260F59
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(9m, ValueProp.Move));
			}
		}

		// Token: 0x06006E46 RID: 28230 RVA: 0x00262D70 File Offset: 0x00260F70
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await Cmd.Wait(0.25f, false);
		}

		// Token: 0x06006E47 RID: 28231 RVA: 0x00262DBC File Offset: 0x00260FBC
		protected override PileType GetResultPileType()
		{
			PileType resultPileType = base.GetResultPileType();
			if (resultPileType != PileType.Discard)
			{
				return resultPileType;
			}
			return PileType.Hand;
		}

		// Token: 0x06006E48 RID: 28232 RVA: 0x00262DD7 File Offset: 0x00260FD7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
