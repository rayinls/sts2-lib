using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200097F RID: 2431
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HandOfGreed : CardModel
	{
		// Token: 0x06006BCE RID: 27598 RVA: 0x0025DD41 File Offset: 0x0025BF41
		public HandOfGreed()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CC2 RID: 7362
		// (get) Token: 0x06006BCF RID: 27599 RVA: 0x0025DD4E File Offset: 0x0025BF4E
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CC3 RID: 7363
		// (get) Token: 0x06006BD0 RID: 27600 RVA: 0x0025DD51 File Offset: 0x0025BF51
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(20m, ValueProp.Move),
					new DynamicVar("Gold", 20m)
				});
			}
		}

		// Token: 0x17001CC4 RID: 7364
		// (get) Token: 0x06006BD1 RID: 27601 RVA: 0x0025DD82 File Offset: 0x0025BF82
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Fatal, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06006BD2 RID: 27602 RVA: 0x0025DD94 File Offset: 0x0025BF94
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
			base.DynamicVars["Gold"].UpgradeValueBy(5m);
		}

		// Token: 0x06006BD3 RID: 27603 RVA: 0x0025DDC8 File Offset: 0x0025BFC8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			bool shouldTriggerFatal = cardPlay.Target.Powers.All((PowerModel p) => p.ShouldOwnerDeathTriggerFatal());
			Vector2? monsterPos = null;
			if (TestMode.IsOff)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(cardPlay.Target);
				monsterPos = ((creatureNode != null) ? new Vector2?(creatureNode.VfxSpawnPosition) : null);
			}
			AttackCommand attackCommand = await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			AttackCommand attackCommand2 = attackCommand;
			if (shouldTriggerFatal)
			{
				if (attackCommand2.Results.Any((DamageResult r) => r.WasTargetKilled))
				{
					if (monsterPos != null)
					{
						VfxCmd.PlayVfx(monsterPos.Value, "vfx/vfx_coin_explosion_regular");
					}
					await PlayerCmd.GainGold(base.DynamicVars["Gold"].IntValue, base.Owner, false);
				}
			}
		}

		// Token: 0x0400258C RID: 9612
		public const int goldAmount = 20;

		// Token: 0x0400258D RID: 9613
		private const string _goldKey = "Gold";
	}
}
