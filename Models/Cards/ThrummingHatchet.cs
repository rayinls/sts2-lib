using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A99 RID: 2713
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ThrummingHatchet : CardModel
	{
		// Token: 0x060071BC RID: 29116 RVA: 0x00269C14 File Offset: 0x00267E14
		public ThrummingHatchet()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F2D RID: 7981
		// (get) Token: 0x060071BD RID: 29117 RVA: 0x00269C21 File Offset: 0x00267E21
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(11m, ValueProp.Move));
			}
		}

		// Token: 0x060071BE RID: 29118 RVA: 0x00269C38 File Offset: 0x00267E38
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x060071BF RID: 29119 RVA: 0x00269C8C File Offset: 0x00267E8C
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner)
			{
				bool flag = CombatManager.Instance.History.CardPlaysFinished.Any((CardPlayFinishedEntry e) => e.RoundNumber == base.CombatState.RoundNumber - 1 && e.CardPlay.Card == this);
				if (flag)
				{
					CardPile pile = base.Pile;
					if (pile == null || pile.Type != PileType.Hand)
					{
						await CardPileCmd.Add(this, PileType.Hand, CardPilePosition.Bottom, null, false);
					}
				}
			}
		}

		// Token: 0x060071C0 RID: 29120 RVA: 0x00269CD7 File Offset: 0x00267ED7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
