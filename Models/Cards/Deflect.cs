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
	// Token: 0x02000915 RID: 2325
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Deflect : CardModel
	{
		// Token: 0x06006988 RID: 27016 RVA: 0x0025975B File Offset: 0x0025795B
		public Deflect()
			: base(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001BC1 RID: 7105
		// (get) Token: 0x06006989 RID: 27017 RVA: 0x00259768 File Offset: 0x00257968
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BC2 RID: 7106
		// (get) Token: 0x0600698A RID: 27018 RVA: 0x0025976B File Offset: 0x0025796B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(4m, ValueProp.Move));
			}
		}

		// Token: 0x0600698B RID: 27019 RVA: 0x00259780 File Offset: 0x00257980
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x0600698C RID: 27020 RVA: 0x002597CB File Offset: 0x002579CB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
