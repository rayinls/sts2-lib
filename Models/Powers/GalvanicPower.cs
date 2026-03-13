using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200062F RID: 1583
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GalvanicPower : PowerModel
	{
		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x060052EA RID: 21226 RVA: 0x00221DE7 File Offset: 0x0021FFE7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x060052EB RID: 21227 RVA: 0x00221DEA File Offset: 0x0021FFEA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x060052EC RID: 21228 RVA: 0x00221DED File Offset: 0x0021FFED
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StringVar("AfflictionTitle", ModelDb.Affliction<Galvanized>().Title.GetFormattedText()));
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x060052ED RID: 21229 RVA: 0x00221E0D File Offset: 0x0022000D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromAffliction<Galvanized>(base.Amount);
			}
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x00221E1C File Offset: 0x0022001C
		public override async Task BeforeCombatStart()
		{
			foreach (Creature creature in base.Owner.CombatState.Allies.ToList<Creature>())
			{
				if (creature.IsPlayer)
				{
					IEnumerable<CardModel> enumerable = creature.Player.PlayerCombatState.AllCards.Where((CardModel c) => c.Type == CardType.Power);
					foreach (CardModel cardModel in enumerable)
					{
						await CardCmd.Afflict<Galvanized>(cardModel, base.Amount);
					}
					IEnumerator<CardModel> enumerator2 = null;
				}
			}
			List<Creature>.Enumerator enumerator = default(List<Creature>.Enumerator);
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x00221E60 File Offset: 0x00220060
		public override async Task AfterCardEnteredCombat(CardModel card)
		{
			if (card.Affliction == null)
			{
				if (card.Type == CardType.Power)
				{
					await CardCmd.Afflict<Galvanized>(card, base.Amount);
				}
			}
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x00221EAC File Offset: 0x002200AC
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Affliction is Galvanized)
			{
				VfxCmd.PlayOnCreature(cardPlay.Card.Owner.Creature, "vfx/vfx_attack_lightning");
				await CreatureCmd.Damage(context, cardPlay.Card.Owner.Creature, base.Amount, ValueProp.Unpowered | ValueProp.Move, null, null);
			}
		}
	}
}
