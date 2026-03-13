using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009DA RID: 2522
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NegativePulse : CardModel
	{
		// Token: 0x06006DBC RID: 28092 RVA: 0x00261CBF File Offset: 0x0025FEBF
		public NegativePulse()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001D8C RID: 7564
		// (get) Token: 0x06006DBD RID: 28093 RVA: 0x00261CCC File Offset: 0x0025FECC
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D8D RID: 7565
		// (get) Token: 0x06006DBE RID: 28094 RVA: 0x00261CCF File Offset: 0x0025FECF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(5m, ValueProp.Move),
					new PowerVar<DoomPower>(7m)
				});
			}
		}

		// Token: 0x17001D8E RID: 7566
		// (get) Token: 0x06006DBF RID: 28095 RVA: 0x00261CF9 File Offset: 0x0025FEF9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x06006DC0 RID: 28096 RVA: 0x00261D08 File Offset: 0x0025FF08
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			foreach (Creature creature in base.CombatState.HittableEnemies)
			{
				await PowerCmd.Apply<DoomPower>(creature, base.DynamicVars.Doom.BaseValue, base.Owner.Creature, this, false);
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06006DC1 RID: 28097 RVA: 0x00261D53 File Offset: 0x0025FF53
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(1m);
			base.DynamicVars.Doom.UpgradeValueBy(4m);
		}
	}
}
