using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007E6 RID: 2022
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RoundTeaParty : EventModel
	{
		// Token: 0x0600623D RID: 25149 RVA: 0x0024A048 File Offset: 0x00248248
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.EnjoyTea), "ROUND_TEA_PARTY.pages.INITIAL.options.ENJOY_TEA", HoverTipFactory.FromRelic<RoyalPoison>()),
				new EventOption(this, new Func<Task>(this.PickFight), "ROUND_TEA_PARTY.pages.INITIAL.options.PICK_FIGHT", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x0600623E RID: 25150 RVA: 0x0024A09E File Offset: 0x0024829E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(11m, ValueProp.Unblockable | ValueProp.Unpowered),
					new StringVar("Relic", ModelDb.Relic<RoyalPoison>().Title.GetFormattedText())
				});
			}
		}

		// Token: 0x0600623F RID: 25151 RVA: 0x0024A0D8 File Offset: 0x002482D8
		private async Task EnjoyTea()
		{
			Creature targetCreature = base.Owner.Creature;
			await RelicCmd.Obtain<RoyalPoison>(base.Owner);
			await CreatureCmd.Heal(targetCreature, targetCreature.MaxHp - targetCreature.CurrentHp, true);
			base.SetEventFinished(base.L10NLookup("ROUND_TEA_PARTY.pages.ENJOY_TEA.description"));
		}

		// Token: 0x06006240 RID: 25152 RVA: 0x0024A11B File Offset: 0x0024831B
		private Task PickFight()
		{
			this.SetEventState(base.L10NLookup("ROUND_TEA_PARTY.pages.PICK_FIGHT.description"), new <>z__ReadOnlySingleElementList<EventOption>(new EventOption(this, new Func<Task>(this.ContinueFight), "ROUND_TEA_PARTY.pages.PICK_FIGHT.options.CONTINUE_FIGHT", Array.Empty<IHoverTip>()).ThatWontSaveToChoiceHistory()));
			return Task.CompletedTask;
		}

		// Token: 0x06006241 RID: 25153 RVA: 0x0024A15C File Offset: 0x0024835C
		private async Task ContinueFight()
		{
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.Damage, null, null);
			await RelicCmd.Obtain(RelicFactory.PullNextRelicFromFront(base.Owner).ToMutable(), base.Owner, -1);
			base.SetEventFinished(base.L10NLookup("ROUND_TEA_PARTY.pages.CONTINUE_FIGHT.description"));
		}
	}
}
