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
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008EC RID: 2284
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Conflagration : CardModel
	{
		// Token: 0x060068B4 RID: 26804 RVA: 0x00257E77 File Offset: 0x00256077
		public Conflagration()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001B64 RID: 7012
		// (get) Token: 0x060068B5 RID: 26805 RVA: 0x00257E84 File Offset: 0x00256084
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NGroundFireVfx.AssetPaths;
			}
		}

		// Token: 0x17001B65 RID: 7013
		// (get) Token: 0x060068B6 RID: 26806 RVA: 0x00257E8C File Offset: 0x0025608C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(8m);
				array[1] = new ExtraDamageVar(2m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature _) => CombatManager.Instance.History.CardPlaysFinished.Count((CardPlayFinishedEntry e) => e.HappenedThisTurn(card.CombatState) && e.CardPlay.Card.Type == CardType.Attack && e.CardPlay.Card.Owner == card.Owner));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x00257EF0 File Offset: 0x002560F0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IReadOnlyList<Creature> hittableEnemies = base.CombatState.HittableEnemies;
			foreach (Creature creature in hittableEnemies)
			{
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance != null)
				{
					instance.CombatVfxContainer.AddChildSafely(NGroundFireVfx.Create(creature, VfxColor.Red));
				}
			}
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_blunt", null, "heavy_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x060068B8 RID: 26808 RVA: 0x00257F3B File Offset: 0x0025613B
		protected override void OnUpgrade()
		{
			base.DynamicVars.CalculationBase.UpgradeValueBy(1m);
			base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
		}
	}
}
