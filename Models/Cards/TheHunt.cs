using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A93 RID: 2707
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheHunt : CardModel
	{
		// Token: 0x06007192 RID: 29074 RVA: 0x002697FC File Offset: 0x002679FC
		public TheHunt()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F1C RID: 7964
		// (get) Token: 0x06007193 RID: 29075 RVA: 0x00269809 File Offset: 0x00267A09
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F1D RID: 7965
		// (get) Token: 0x06007194 RID: 29076 RVA: 0x0026980C File Offset: 0x00267A0C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(10m, ValueProp.Move));
			}
		}

		// Token: 0x17001F1E RID: 7966
		// (get) Token: 0x06007195 RID: 29077 RVA: 0x00269820 File Offset: 0x00267A20
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001F1F RID: 7967
		// (get) Token: 0x06007196 RID: 29078 RVA: 0x00269828 File Offset: 0x00267A28
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Fatal, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06007197 RID: 29079 RVA: 0x0026983C File Offset: 0x00267A3C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			AbstractRoom currentRoom = base.CombatState.RunState.CurrentRoom;
			CombatRoom combatRoom = currentRoom as CombatRoom;
			if (combatRoom != null)
			{
				ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
				bool shouldTriggerFatal = cardPlay.Target.Powers.All((PowerModel p) => p.ShouldOwnerDeathTriggerFatal());
				AttackCommand attackCommand = await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
					.WithHitFx("vfx/vfx_attack_slash", null, null)
					.Execute(choiceContext);
				AttackCommand attackCommand2 = attackCommand;
				if (shouldTriggerFatal)
				{
					if (attackCommand2.Results.Any((DamageResult r) => r.WasTargetKilled))
					{
						combatRoom.AddExtraReward(base.Owner, new CardReward(CardCreationOptions.ForRoom(base.Owner, combatRoom.RoomType), 3, base.Owner));
						await PowerCmd.Apply<TheHuntPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
					}
				}
			}
		}

		// Token: 0x06007198 RID: 29080 RVA: 0x0026988F File Offset: 0x00267A8F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}
	}
}
