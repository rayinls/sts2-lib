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
	// Token: 0x020008B5 RID: 2229
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Blur : CardModel
	{
		// Token: 0x0600679E RID: 26526 RVA: 0x00255B98 File Offset: 0x00253D98
		public Blur()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001AF3 RID: 6899
		// (get) Token: 0x0600679F RID: 26527 RVA: 0x00255BA5 File Offset: 0x00253DA5
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001AF4 RID: 6900
		// (get) Token: 0x060067A0 RID: 26528 RVA: 0x00255BA8 File Offset: 0x00253DA8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(5m, ValueProp.Move),
					new DynamicVar("Blur", 1m)
				});
			}
		}

		// Token: 0x060067A1 RID: 26529 RVA: 0x00255BD8 File Offset: 0x00253DD8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<BlurPower>(base.Owner.Creature, base.DynamicVars["Blur"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060067A2 RID: 26530 RVA: 0x00255C23 File Offset: 0x00253E23
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}

		// Token: 0x0400255C RID: 9564
		private const string _blurKey = "Blur";
	}
}
