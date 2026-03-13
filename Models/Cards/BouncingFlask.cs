using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008BE RID: 2238
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BouncingFlask : CardModel
	{
		// Token: 0x060067D2 RID: 26578 RVA: 0x00256266 File Offset: 0x00254466
		public BouncingFlask()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.RandomEnemy, true)
		{
		}

		// Token: 0x17001B09 RID: 6921
		// (get) Token: 0x060067D3 RID: 26579 RVA: 0x00256283 File Offset: 0x00254483
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<PoisonPower>(3m),
					new RepeatVar(3)
				});
			}
		}

		// Token: 0x17001B0A RID: 6922
		// (get) Token: 0x060067D4 RID: 26580 RVA: 0x002562A7 File Offset: 0x002544A7
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x060067D5 RID: 26581 RVA: 0x002562B4 File Offset: 0x002544B4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			Vector2 lastPos = Vector2.Zero;
			for (int i = 0; i < base.DynamicVars.Repeat.IntValue; i++)
			{
				Creature enemy = base.Owner.RunState.Rng.CombatTargets.NextItem<Creature>(base.CombatState.HittableEnemies);
				if (enemy != null)
				{
					if (TestMode.IsOff)
					{
						if (i == 0)
						{
							lastPos = NCombatRoom.Instance.GetCreatureNode(base.Owner.Creature).VfxSpawnPosition;
						}
						NCreature targetNode = NCombatRoom.Instance.GetCreatureNode(enemy);
						if (targetNode != null)
						{
							NItemThrowVfx nitemThrowVfx = NItemThrowVfx.Create(lastPos, targetNode.GetBottomOfHitbox(), ModelDb.Potion<PoisonPotion>().Image, null);
							NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(nitemThrowVfx);
							lastPos = targetNode.VfxSpawnPosition;
							await Cmd.Wait(0.5f, false);
							NSplashVfx nsplashVfx = NSplashVfx.Create(targetNode.VfxSpawnPosition, this._vfxTint);
							NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(nsplashVfx);
							NLiquidOverlayVfx nliquidOverlayVfx = NLiquidOverlayVfx.Create(enemy, this._vfxTint);
							NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(nliquidOverlayVfx);
							NGaseousImpactVfx ngaseousImpactVfx = NGaseousImpactVfx.Create(targetNode.VfxSpawnPosition, this._vfxTint);
							NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(ngaseousImpactVfx);
						}
						targetNode = null;
					}
					await PowerCmd.Apply<PoisonPower>(enemy, base.DynamicVars.Poison.BaseValue, base.Owner.Creature, this, false);
				}
				enemy = null;
			}
		}

		// Token: 0x060067D6 RID: 26582 RVA: 0x002562F7 File Offset: 0x002544F7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Repeat.UpgradeValueBy(1m);
		}

		// Token: 0x0400255E RID: 9566
		private readonly Color _vfxTint = new Color("83eb85");
	}
}
