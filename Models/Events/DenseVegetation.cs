using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007CC RID: 1996
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DenseVegetation : EventModel
	{
		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x0600614E RID: 24910 RVA: 0x00245858 File Offset: 0x00243A58
		public override bool IsShared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x0600614F RID: 24911 RVA: 0x0024585B File Offset: 0x00243A5B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HealVar(0m),
					new HpLossVar(11m)
				});
			}
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x00245884 File Offset: 0x00243A84
		public override void CalculateVars()
		{
			base.DynamicVars.Heal.BaseValue = ((base.Owner != null) ? HealRestSiteOption.GetHealAmount(base.Owner) : 0m);
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x002458B0 File Offset: 0x00243AB0
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.TrudgeOn), "DENSE_VEGETATION.pages.INITIAL.options.TRUDGE_ON", Array.Empty<IHoverTip>()).ThatDoesDamage(base.DynamicVars.HpLoss.BaseValue),
				new EventOption(this, new Func<Task>(this.Rest), "DENSE_VEGETATION.pages.INITIAL.options.REST", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x06006152 RID: 24914 RVA: 0x0024591C File Offset: 0x00243B1C
		private async Task TrudgeOn()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForRemoval(base.Owner, cardSelectorPrefs, null);
			List<CardModel> list = enumerable.ToList<CardModel>();
			await CardPileCmd.RemoveFromDeck(list, true);
			NEventRoom instance = NEventRoom.Instance;
			Control container = ((instance != null) ? instance.VfxContainer : null);
			if (LocalContext.IsMe(base.Owner) && container != null)
			{
				for (int i = 0; i < 3; i++)
				{
					Vector2 vector = new Vector2(container.Size.X * 0.25f, container.Size.Y * 0.6f);
					Vector2 vector2 = new Vector2(Rng.Chaotic.NextFloat(-100f, 100f), Rng.Chaotic.NextFloat(-200f, 200f));
					Node2D node2D = VfxCmd.PlayNonCombatVfx(container, vector + vector2, "vfx/vfx_attack_slash");
					Node2D node2D2 = VfxCmd.PlayNonCombatVfx(container, vector + vector2, "vfx/events/dense_vegetation_slice_vfx");
					NDebugAudioManager.Instance.Play("slash_attack.mp3", 0.8f, PitchVariance.Medium);
					node2D.RotationDegrees = -Rng.Chaotic.NextFloat(1f) * 180f;
					node2D2.RotationDegrees = -Rng.Chaotic.NextFloat(1f) * 180f;
					await Cmd.CustomScaledWait(0.2f, 0.4f, false, default(CancellationToken));
				}
			}
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
			base.SetEventFinished(base.L10NLookup("DENSE_VEGETATION.pages.TRUDGE_ON.description"));
		}

		// Token: 0x06006153 RID: 24915 RVA: 0x00245960 File Offset: 0x00243B60
		private async Task Rest()
		{
			await PlayerCmd.MimicRestSiteHeal(base.Owner, false);
			if (LocalContext.IsMe(base.Owner))
			{
				int restHandle = NDebugAudioManager.Instance.Play("sleep.tres", 0.8f, PitchVariance.None);
				await Cmd.CustomScaledWait(0.7f, 1.5f, false, default(CancellationToken));
				NDebugAudioManager.Instance.Stop(restHandle, 0.5f);
				NDebugAudioManager.Instance.Play("hiss.mp3", 0.8f, PitchVariance.Large);
				NGame.Instance.ScreenRumble(ShakeStrength.Medium, ShakeDuration.Normal, RumbleStyle.Rumble);
			}
			this.SetEventState(base.L10NLookup("DENSE_VEGETATION.pages.REST.description"), new <>z__ReadOnlySingleElementList<EventOption>(new EventOption(this, new Func<Task>(this.Fight), "DENSE_VEGETATION.pages.REST.options.FIGHT", Array.Empty<IHoverTip>())));
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x002459A3 File Offset: 0x00243BA3
		private Task Fight()
		{
			base.EnterCombatWithoutExitingEvent<DenseVegetationEventEncounter>(Array.Empty<Reward>(), false);
			return Task.CompletedTask;
		}
	}
}
