using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000970 RID: 2416
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Glitterstream : CardModel
	{
		// Token: 0x06006B83 RID: 27523 RVA: 0x0025D3D6 File Offset: 0x0025B5D6
		public Glitterstream()
			: base(2, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x06006B84 RID: 27524 RVA: 0x0025D3E3 File Offset: 0x0025B5E3
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x06006B85 RID: 27525 RVA: 0x0025D3E6 File Offset: 0x0025B5E6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(11m, ValueProp.Move),
					new BlockVar("BlockNextTurn", 4m, ValueProp.Move)
				});
			}
		}

		// Token: 0x06006B86 RID: 27526 RVA: 0x0025D418 File Offset: 0x0025B618
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			BlockVar blockVar = (BlockVar)base.DynamicVars["BlockNextTurn"];
			IEnumerable<AbstractModel> enumerable;
			decimal blockNextTurnAmount = Hook.ModifyBlock(base.CombatState, base.Owner.Creature, blockVar.BaseValue, blockVar.Props, this, cardPlay, out enumerable);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<BlockNextTurnPower>(base.Owner.Creature, blockNextTurnAmount, base.Owner.Creature, this, false);
		}

		// Token: 0x06006B87 RID: 27527 RVA: 0x0025D463 File Offset: 0x0025B663
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
			base.DynamicVars["BlockNextTurn"].UpgradeValueBy(2m);
		}

		// Token: 0x04002587 RID: 9607
		private const string _blockNextTurnKey = "BlockNextTurn";
	}
}
