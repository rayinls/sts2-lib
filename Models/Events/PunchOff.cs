using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.GodotExtensions;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007E1 RID: 2017
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PunchOff : EventModel
	{
		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x06006205 RID: 25093 RVA: 0x0024925B File Offset: 0x0024745B
		public override EventLayoutType LayoutType
		{
			get
			{
				return EventLayoutType.Combat;
			}
		}

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x06006206 RID: 25094 RVA: 0x0024925E File Offset: 0x0024745E
		public override EncounterModel CanonicalEncounter
		{
			get
			{
				return ModelDb.Encounter<PunchOffEventEncounter>();
			}
		}

		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x06006207 RID: 25095 RVA: 0x00249265 File Offset: 0x00247465
		public override bool IsShared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006208 RID: 25096 RVA: 0x00249268 File Offset: 0x00247468
		public override bool IsAllowed(RunState runState)
		{
			return runState.TotalFloor >= 6;
		}

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x06006209 RID: 25097 RVA: 0x00249276 File Offset: 0x00247476
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(0));
			}
		}

		// Token: 0x0600620A RID: 25098 RVA: 0x00249284 File Offset: 0x00247484
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Nab), "PUNCH_OFF.pages.INITIAL.options.NAB", HoverTipFactory.FromCardWithCardHoverTips<Injury>(false)),
				new EventOption(this, new Func<Task>(this.TakeThem), "PUNCH_OFF.pages.INITIAL.options.I_CAN_TAKE_THEM", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x0600620B RID: 25099 RVA: 0x002492DB File Offset: 0x002474DB
		public override Task AfterEventStarted()
		{
			RunManager.Instance.RoomExited += this.OnRoomExited;
			base.Owner.CanRemovePotions = false;
			this._punchCts = new CancellationTokenSource();
			TaskHelper.RunSafely(this.PunchEachOther());
			return Task.CompletedTask;
		}

		// Token: 0x0600620C RID: 25100 RVA: 0x0024931C File Offset: 0x0024751C
		private async Task PunchEachOther()
		{
			Creature leftEnemy = this._combatStateForCombatLayout.Enemies[0];
			Creature rightEnemy = this._combatStateForCombatLayout.Enemies[1];
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature leftEnemyNode = ((instance != null) ? instance.GetCreatureNode(leftEnemy) : null);
			if (leftEnemyNode != null)
			{
				Vector2 originalScale = leftEnemyNode.Scale;
				leftEnemyNode.Scale = new Vector2(-originalScale.X, originalScale.Y);
				NCombatRoom instance2 = NCombatRoom.Instance;
				Control vfxContainer = ((instance2 != null) ? instance2.CombatVfxContainer : null);
				while (!this._punchCts.IsCancellationRequested)
				{
					await CreatureCmd.TriggerAnim(leftEnemy, "Attack", 0f);
					await Cmd.Wait(0.1f, false);
					VfxCmd.PlayOnCreatureCenter(rightEnemy, "vfx/vfx_attack_blunt");
					Control control = vfxContainer;
					if (control != null)
					{
						control.AddChildSafely(NHitSparkVfx.Create(rightEnemy, false));
					}
					await CreatureCmd.TriggerAnim(rightEnemy, "Hit", 0f);
					await Cmd.Wait(1.2f, false);
					if (this._punchCts.IsCancellationRequested)
					{
						break;
					}
					await CreatureCmd.TriggerAnim(rightEnemy, "Attack", 0f);
					await Cmd.Wait(0.1f, false);
					VfxCmd.PlayOnCreatureCenter(leftEnemy, "vfx/vfx_attack_blunt");
					Control control2 = vfxContainer;
					if (control2 != null)
					{
						control2.AddChildSafely(NHitSparkVfx.Create(leftEnemy, false));
					}
					await CreatureCmd.TriggerAnim(leftEnemy, "Hit", 0f);
					await Cmd.Wait(1.2f, false);
				}
				this._punchCts = null;
				if (leftEnemyNode.IsValid())
				{
					leftEnemyNode.Scale = originalScale;
				}
			}
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x0024935F File Offset: 0x0024755F
		public override void CalculateVars()
		{
			base.DynamicVars.Gold.BaseValue = base.Rng.NextInt(91, 99);
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x00249385 File Offset: 0x00247585
		protected override void OnEventFinished()
		{
			base.Owner.CanRemovePotions = true;
		}

		// Token: 0x0600620F RID: 25103 RVA: 0x00249394 File Offset: 0x00247594
		private async Task Nab()
		{
			await CardPileCmd.AddCurseToDeck<Injury>(base.Owner);
			NGame.Instance.ScreenShakeTrauma(ShakeStrength.Strong);
			NDebugAudioManager instance = NDebugAudioManager.Instance;
			if (instance != null)
			{
				instance.Play("blunt_attack.mp3", 1f, PitchVariance.None);
			}
			await Cmd.CustomScaledWait(0.25f, 0.5f, false, default(CancellationToken));
			await RewardsCmd.OfferCustom(base.Owner, new List<Reward>(1)
			{
				new RelicReward(base.Owner)
			});
			base.SetEventFinished(base.L10NLookup("PUNCH_OFF.pages.NAB.description"));
		}

		// Token: 0x06006210 RID: 25104 RVA: 0x002493D8 File Offset: 0x002475D8
		private Task TakeThem()
		{
			CancellationTokenSource punchCts = this._punchCts;
			if (punchCts != null)
			{
				punchCts.Cancel();
			}
			this.SetEventState(base.L10NLookup("PUNCH_OFF.pages.I_CAN_TAKE_THEM.description"), new <>z__ReadOnlySingleElementList<EventOption>(new EventOption(this, new Func<Task>(this.Fight), "PUNCH_OFF.pages.I_CAN_TAKE_THEM.options.FIGHT", Array.Empty<IHoverTip>())));
			return Task.CompletedTask;
		}

		// Token: 0x06006211 RID: 25105 RVA: 0x00249430 File Offset: 0x00247630
		private Task Fight()
		{
			base.Owner.CanRemovePotions = true;
			base.EnterCombatWithoutExitingEvent<PunchOffEventEncounter>(new <>z__ReadOnlyArray<Reward>(new Reward[]
			{
				new RelicReward(base.Owner),
				new PotionReward(base.Owner)
			}), false);
			return Task.CompletedTask;
		}

		// Token: 0x06006212 RID: 25106 RVA: 0x0024947C File Offset: 0x0024767C
		private void OnRoomExited()
		{
			RunManager.Instance.RoomExited -= this.OnRoomExited;
			CancellationTokenSource punchCts = this._punchCts;
			if (punchCts == null)
			{
				return;
			}
			punchCts.Cancel();
		}

		// Token: 0x040024B2 RID: 9394
		[Nullable(2)]
		private CancellationTokenSource _punchCts;
	}
}
