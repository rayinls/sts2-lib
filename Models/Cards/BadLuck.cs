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
	// Token: 0x0200089E RID: 2206
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BadLuck : CardModel
	{
		// Token: 0x06006726 RID: 26406 RVA: 0x00254C1B File Offset: 0x00252E1B
		public BadLuck()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001AC1 RID: 6849
		// (get) Token: 0x06006727 RID: 26407 RVA: 0x00254C29 File Offset: 0x00252E29
		public override bool CanBeGeneratedByModifiers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AC2 RID: 6850
		// (get) Token: 0x06006728 RID: 26408 RVA: 0x00254C2C File Offset: 0x00252E2C
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001AC3 RID: 6851
		// (get) Token: 0x06006729 RID: 26409 RVA: 0x00254C2F File Offset: 0x00252E2F
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Eternal,
					CardKeyword.Unplayable
				});
			}
		}

		// Token: 0x17001AC4 RID: 6852
		// (get) Token: 0x0600672A RID: 26410 RVA: 0x00254C44 File Offset: 0x00252E44
		public override bool HasTurnEndInHandEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001AC5 RID: 6853
		// (get) Token: 0x0600672B RID: 26411 RVA: 0x00254C47 File Offset: 0x00252E47
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HpLossVar(13m));
			}
		}

		// Token: 0x0600672C RID: 26412 RVA: 0x00254C5C File Offset: 0x00252E5C
		public override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
		{
			await Cmd.Wait(0.25f, false);
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
		}
	}
}
