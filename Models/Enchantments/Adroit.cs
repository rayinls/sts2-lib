using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000865 RID: 2149
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Adroit : EnchantmentModel
	{
		// Token: 0x170019F4 RID: 6644
		// (get) Token: 0x060065B4 RID: 26036 RVA: 0x00252A95 File Offset: 0x00250C95
		public override bool HasExtraCardText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019F5 RID: 6645
		// (get) Token: 0x060065B5 RID: 26037 RVA: 0x00252A98 File Offset: 0x00250C98
		public override bool ShowAmount
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019F6 RID: 6646
		// (get) Token: 0x060065B6 RID: 26038 RVA: 0x00252A9B File Offset: 0x00250C9B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(0m, ValueProp.Move));
			}
		}

		// Token: 0x060065B7 RID: 26039 RVA: 0x00252AB0 File Offset: 0x00250CB0
		public override async Task OnPlay(PlayerChoiceContext choiceContext, [Nullable(2)] CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Card.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x060065B8 RID: 26040 RVA: 0x00252AFB File Offset: 0x00250CFB
		public override void RecalculateValues()
		{
			base.DynamicVars.Block.BaseValue = base.Amount;
		}
	}
}
