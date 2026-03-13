using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A21 RID: 2593
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Reflect : CardModel
	{
		// Token: 0x06006F27 RID: 28455 RVA: 0x00264B27 File Offset: 0x00262D27
		public Reflect()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E1E RID: 7710
		// (get) Token: 0x06006F28 RID: 28456 RVA: 0x00264B34 File Offset: 0x00262D34
		public override int CanonicalStarCost
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001E1F RID: 7711
		// (get) Token: 0x06006F29 RID: 28457 RVA: 0x00264B37 File Offset: 0x00262D37
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E20 RID: 7712
		// (get) Token: 0x06006F2A RID: 28458 RVA: 0x00264B3A File Offset: 0x00262D3A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(17m, ValueProp.Move));
			}
		}

		// Token: 0x06006F2B RID: 28459 RVA: 0x00264B50 File Offset: 0x00262D50
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<ReflectPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006F2C RID: 28460 RVA: 0x00264B9B File Offset: 0x00262D9B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(4m);
		}
	}
}
