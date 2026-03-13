using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000993 RID: 2451
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HowlFromBeyond : CardModel
	{
		// Token: 0x06006C33 RID: 27699 RVA: 0x0025EAAB File Offset: 0x0025CCAB
		public HowlFromBeyond()
			: base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001CEA RID: 7402
		// (get) Token: 0x06006C34 RID: 27700 RVA: 0x0025EAB8 File Offset: 0x0025CCB8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(16m, ValueProp.Move));
			}
		}

		// Token: 0x17001CEB RID: 7403
		// (get) Token: 0x06006C35 RID: 27701 RVA: 0x0025EACC File Offset: 0x0025CCCC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x06006C36 RID: 27702 RVA: 0x0025EADC File Offset: 0x0025CCDC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithAttackerAnim("Cast", base.Owner.Character.CastAnimDelay, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, "heavy_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006C37 RID: 27703 RVA: 0x0025EB28 File Offset: 0x0025CD28
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			CardPile pile = base.Pile;
			if (pile != null && pile.Type == PileType.Exhaust)
			{
				if (player == base.Owner)
				{
					await CardCmd.AutoPlay(choiceContext, this, null, AutoPlayType.Default, false, false);
				}
			}
		}

		// Token: 0x06006C38 RID: 27704 RVA: 0x0025EB7B File Offset: 0x0025CD7B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}
	}
}
