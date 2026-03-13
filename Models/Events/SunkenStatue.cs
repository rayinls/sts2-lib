using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007ED RID: 2029
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SunkenStatue : EventModel
	{
		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x06006278 RID: 25208 RVA: 0x0024AF18 File Offset: 0x00249118
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("Relic", ModelDb.Relic<SwordOfStone>().Title.GetFormattedText()),
					new GoldVar(111),
					new DynamicVar("HpLoss", 7m)
				});
			}
		}

		// Token: 0x06006279 RID: 25209 RVA: 0x0024AF69 File Offset: 0x00249169
		public override void CalculateVars()
		{
			base.DynamicVars.Gold.BaseValue += base.Rng.NextInt(-10, 11);
		}

		// Token: 0x0600627A RID: 25210 RVA: 0x0024AF9C File Offset: 0x0024919C
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.GrabSword), "SUNKEN_STATUE.pages.INITIAL.options.GRAB_SWORD", HoverTipFactory.FromRelic<SwordOfStone>()),
				new EventOption(this, new Func<Task>(this.DiveIntoWater), "SUNKEN_STATUE.pages.INITIAL.options.DIVE_INTO_WATER", Array.Empty<IHoverTip>()).ThatDoesDamage(base.DynamicVars["HpLoss"].BaseValue)
			});
		}

		// Token: 0x0600627B RID: 25211 RVA: 0x0024B00C File Offset: 0x0024920C
		private async Task GrabSword()
		{
			await RelicCmd.Obtain<SwordOfStone>(base.Owner);
			base.SetEventFinished(base.L10NLookup("SUNKEN_STATUE.pages.GRAB_SWORD.description"));
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x0024B050 File Offset: 0x00249250
		private async Task DiveIntoWater()
		{
			await PlayerCmd.GainGold(base.DynamicVars.Gold.BaseValue, base.Owner, false);
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars["HpLoss"].BaseValue, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
			base.SetEventFinished(base.L10NLookup("SUNKEN_STATUE.pages.DIVE_INTO_WATER.description"));
		}

		// Token: 0x040024D3 RID: 9427
		private const string _relicKey = "Relic";

		// Token: 0x040024D4 RID: 9428
		private const string _hpLossKey = "HpLoss";

		// Token: 0x040024D5 RID: 9429
		private const int _goldVariance = 10;
	}
}
