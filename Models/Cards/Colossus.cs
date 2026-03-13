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
	// Token: 0x020008E8 RID: 2280
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Colossus : CardModel
	{
		// Token: 0x0600689E RID: 26782 RVA: 0x00257B4B File Offset: 0x00255D4B
		public Colossus()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B5A RID: 7002
		// (get) Token: 0x0600689F RID: 26783 RVA: 0x00257B58 File Offset: 0x00255D58
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x17001B5B RID: 7003
		// (get) Token: 0x060068A0 RID: 26784 RVA: 0x00257B64 File Offset: 0x00255D64
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B5C RID: 7004
		// (get) Token: 0x060068A1 RID: 26785 RVA: 0x00257B67 File Offset: 0x00255D67
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(5m, ValueProp.Move),
					new DynamicVar("Colossus", 1m)
				});
			}
		}

		// Token: 0x060068A2 RID: 26786 RVA: 0x00257B98 File Offset: 0x00255D98
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<ColossusPower>(base.Owner.Creature, base.DynamicVars["Colossus"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060068A3 RID: 26787 RVA: 0x00257BE3 File Offset: 0x00255DE3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}

		// Token: 0x04002566 RID: 9574
		private const string _powerVarName = "Colossus";
	}
}
