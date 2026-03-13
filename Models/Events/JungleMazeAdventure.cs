using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007D8 RID: 2008
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class JungleMazeAdventure : EventModel
	{
		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x060061AE RID: 25006 RVA: 0x00246F9F File Offset: 0x0024519F
		public override bool IsShared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x00246FA4 File Offset: 0x002451A4
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.DontNeedHelp), "JUNGLE_MAZE_ADVENTURE.pages.INITIAL.options.SOLO_QUEST", Array.Empty<IHoverTip>()).ThatDoesDamage(base.DynamicVars["SoloHp"].BaseValue),
				new EventOption(this, new Func<Task>(this.SafetyInNumbers), "JUNGLE_MAZE_ADVENTURE.pages.INITIAL.options.JOIN_FORCES", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x060061B0 RID: 25008 RVA: 0x00247014 File Offset: 0x00245214
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("SoloGold", 150m),
					new DamageVar("SoloHp", 18m, ValueProp.Unblockable | ValueProp.Unpowered),
					new DynamicVar("JoinForcesGold", 50m)
				});
			}
		}

		// Token: 0x060061B1 RID: 25009 RVA: 0x0024706C File Offset: 0x0024526C
		public override void CalculateVars()
		{
			base.DynamicVars["SoloGold"].BaseValue += (decimal)base.Rng.NextFloat(-15f, 15f);
			base.DynamicVars["JoinForcesGold"].BaseValue += (decimal)base.Rng.NextFloat(-15f, 15f);
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x002470F0 File Offset: 0x002452F0
		private async Task DontNeedHelp()
		{
			List<ValueTuple<string, string>> shuffledFx = JungleMazeAdventure._fx.ToList<ValueTuple<string, string>>().StableShuffle(base.Rng);
			for (int i = 0; i < 3; i++)
			{
				NEventRoom instance = NEventRoom.Instance;
				Control control = ((instance != null) ? instance.VfxContainer : null);
				if (LocalContext.IsMe(base.Owner) && control != null)
				{
					VfxCmd.PlayNonCombatVfx(control, new Vector2(control.Size.X * 0.25f, control.Size.Y * 0.5f), shuffledFx[i].Item1);
					NDebugAudioManager.Instance.Play(shuffledFx[i].Item2, 1f, PitchVariance.None);
				}
				if (i < 2)
				{
					await Cmd.CustomScaledWait(0.25f, 0.5f, false, default(CancellationToken));
				}
			}
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars["SoloHp"].BaseValue, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
			await PlayerCmd.GainGold(base.DynamicVars["SoloGold"].BaseValue, base.Owner, false);
			base.SetEventFinished(base.L10NLookup("JUNGLE_MAZE_ADVENTURE.pages.SOLO_QUEST.description"));
		}

		// Token: 0x060061B3 RID: 25011 RVA: 0x00247134 File Offset: 0x00245334
		private async Task SafetyInNumbers()
		{
			NDebugAudioManager.Instance.Play("hey.mp3", 1f, PitchVariance.None);
			await Cmd.CustomScaledWait(0f, 0.2f, false, default(CancellationToken));
			await PlayerCmd.GainGold(base.DynamicVars["JoinForcesGold"].BaseValue, base.Owner, false);
			base.SetEventFinished(base.L10NLookup("JUNGLE_MAZE_ADVENTURE.pages.JOIN_FORCES.description"));
		}

		// Token: 0x060061B5 RID: 25013 RVA: 0x00247180 File Offset: 0x00245380
		// Note: this type is marked as 'beforefieldinit'.
		unsafe static JungleMazeAdventure()
		{
			int num = 3;
			List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>(num);
			CollectionsMarshal.SetCount<ValueTuple<string, string>>(list, num);
			Span<ValueTuple<string, string>> span = CollectionsMarshal.AsSpan<ValueTuple<string, string>>(list);
			int num2 = 0;
			*span[num2] = new ValueTuple<string, string>("vfx/vfx_attack_blunt", "blunt_attack.mp3");
			num2++;
			*span[num2] = new ValueTuple<string, string>("vfx/vfx_attack_slash", "slash_attack.mp3");
			num2++;
			*span[num2] = new ValueTuple<string, string>("vfx/vfx_heavy_blunt", "heavy_attack.mp3");
			JungleMazeAdventure._fx = list;
		}

		// Token: 0x0400249E RID: 9374
		private const string _soloGoldKey = "SoloGold";

		// Token: 0x0400249F RID: 9375
		private const string _soloHpKey = "SoloHp";

		// Token: 0x040024A0 RID: 9376
		private const string _joinForcesGoldKey = "JoinForcesGold";

		// Token: 0x040024A1 RID: 9377
		[Nullable(new byte[] { 1, 0, 1, 1 })]
		private static readonly List<ValueTuple<string, string>> _fx;
	}
}
