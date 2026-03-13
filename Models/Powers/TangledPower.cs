using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Afflictions;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006B8 RID: 1720
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TangledPower : PowerModel
	{
		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x0600561B RID: 22043 RVA: 0x00227BA7 File Offset: 0x00225DA7
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x0600561C RID: 22044 RVA: 0x00227BAA File Offset: 0x00225DAA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x0600561D RID: 22045 RVA: 0x00227BAD File Offset: 0x00225DAD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x0600561E RID: 22046 RVA: 0x00227BBC File Offset: 0x00225DBC
		[NullableContext(2)]
		[return: Nullable(1)]
		public override async Task AfterApplied(Creature applier, CardModel cardSource)
		{
			IEnumerable<CardModel> enumerable = base.Owner.Player.PlayerCombatState.AllCards.Where((CardModel c) => c.Type == CardType.Attack);
			foreach (CardModel cardModel in enumerable)
			{
				await CardCmd.Afflict<Entangled>(cardModel, 1m);
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x0600561F RID: 22047 RVA: 0x00227C00 File Offset: 0x00225E00
		public override async Task AfterCardEnteredCombat(CardModel card)
		{
			if (card.Owner == base.Owner.Player)
			{
				if (card.Affliction == null)
				{
					if (card.Type == CardType.Attack)
					{
						await CardCmd.Afflict<Entangled>(card, 1m);
					}
				}
			}
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x00227C4C File Offset: 0x00225E4C
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await PowerCmd.Remove(this);
			}
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x00227C98 File Offset: 0x00225E98
		public override Task AfterRemoved(Creature oldOwner)
		{
			IEnumerable<CardModel> enumerable = oldOwner.Player.PlayerCombatState.AllCards.Where((CardModel c) => c.Affliction is Entangled);
			foreach (CardModel cardModel in enumerable)
			{
				CardCmd.ClearAffliction(cardModel);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x00227D1C File Offset: 0x00225F1C
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			if (!(card.Affliction is Entangled) || card.Owner != base.Owner.Player)
			{
				modifiedCost = originalCost;
				return false;
			}
			modifiedCost = originalCost + base.Amount;
			return true;
		}
	}
}
