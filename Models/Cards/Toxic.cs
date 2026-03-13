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
	// Token: 0x02000A9F RID: 2719
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Toxic : CardModel
	{
		// Token: 0x060071DB RID: 29147 RVA: 0x0026A053 File Offset: 0x00268253
		public Toxic()
			: base(1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001F38 RID: 7992
		// (get) Token: 0x060071DC RID: 29148 RVA: 0x0026A060 File Offset: 0x00268260
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001F39 RID: 7993
		// (get) Token: 0x060071DD RID: 29149 RVA: 0x0026A063 File Offset: 0x00268263
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001F3A RID: 7994
		// (get) Token: 0x060071DE RID: 29150 RVA: 0x0026A06B File Offset: 0x0026826B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(5m, ValueProp.Unpowered | ValueProp.Move));
			}
		}

		// Token: 0x17001F3B RID: 7995
		// (get) Token: 0x060071DF RID: 29151 RVA: 0x0026A07F File Offset: 0x0026827F
		public override bool HasTurnEndInHandEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060071E0 RID: 29152 RVA: 0x0026A084 File Offset: 0x00268284
		public override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
		{
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.Damage, this);
		}
	}
}
