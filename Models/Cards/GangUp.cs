using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000967 RID: 2407
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GangUp : CardModel
	{
		// Token: 0x06006B4D RID: 27469 RVA: 0x0025CD8F File Offset: 0x0025AF8F
		public GangUp()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x06006B4E RID: 27470 RVA: 0x0025CD9C File Offset: 0x0025AF9C
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x06006B4F RID: 27471 RVA: 0x0025CDA0 File Offset: 0x0025AFA0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(5m);
				array[1] = new ExtraDamageVar(5m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature target) => CombatManager.Instance.History.Entries.OfType<DamageReceivedEntry>().Count((DamageReceivedEntry e) => e.Receiver == target && e.Result.Props.IsPoweredAttack() && e.HappenedThisTurn(card.CombatState) && e.Dealer != null && e.Dealer != card.Owner.Creature && e.Dealer.Side == card.Owner.Creature.Side));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006B50 RID: 27472 RVA: 0x0025CE04 File Offset: 0x0025B004
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006B51 RID: 27473 RVA: 0x0025CE57 File Offset: 0x0025B057
		protected override void OnUpgrade()
		{
			base.DynamicVars.ExtraDamage.UpgradeValueBy(2m);
		}
	}
}
