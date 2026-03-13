using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007C1 RID: 1985
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AbyssalBaths : EventModel
	{
		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x060060F9 RID: 24825 RVA: 0x00244014 File Offset: 0x00242214
		// (set) Token: 0x060060FA RID: 24826 RVA: 0x0024401C File Offset: 0x0024221C
		private int LingerCount
		{
			get
			{
				return this._lingerCount;
			}
			set
			{
				base.AssertMutable();
				this._lingerCount = value;
			}
		}

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x060060FB RID: 24827 RVA: 0x0024402B File Offset: 0x0024222B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new MaxHpVar(2m),
					new DamageVar(3m, ValueProp.Unblockable | ValueProp.Unpowered),
					new HealVar(10m)
				});
			}
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x00244064 File Offset: 0x00242264
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Immerse), "ABYSSAL_BATHS.pages.INITIAL.options.IMMERSE", Array.Empty<IHoverTip>()).ThatDoesDamage(base.DynamicVars.Damage.IntValue - base.DynamicVars.MaxHp.BaseValue),
				new EventOption(this, new Func<Task>(this.Abstain), "ABYSSAL_BATHS.pages.INITIAL.options.ABSTAIN", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x060060FD RID: 24829 RVA: 0x002440EC File Offset: 0x002422EC
		private async Task Immerse()
		{
			await this.OnImmerse();
			this.SetEventState(base.L10NLookup("ABYSSAL_BATHS.pages.IMMERSE.description"), new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Linger), "ABYSSAL_BATHS.pages.ALL.options.LINGER", Array.Empty<IHoverTip>()).ThatDoesDamage(base.DynamicVars.Damage.IntValue - base.DynamicVars.MaxHp.BaseValue),
				new EventOption(this, new Func<Task>(this.ExitBaths), "ABYSSAL_BATHS.pages.ALL.options.EXIT_BATHS", Array.Empty<IHoverTip>())
			}));
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x00244130 File Offset: 0x00242330
		private async Task Abstain()
		{
			await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.IntValue, true);
			base.SetEventFinished(base.L10NLookup("ABYSSAL_BATHS.pages.ABSTAIN.description"));
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x00244174 File Offset: 0x00242374
		private async Task Linger()
		{
			int lingerCount = this.LingerCount;
			this.LingerCount = lingerCount + 1;
			if (this.LingerCount > 9)
			{
				this.LingerCount = 9;
			}
			await this.OnImmerse();
			decimal num = base.DynamicVars.Damage.IntValue - base.DynamicVars.MaxHp.BaseValue;
			bool flag = this.WillKillPlayer(num);
			if (flag)
			{
				this.SetEventState(base.L10NLookup("ABYSSAL_BATHS.pages.DEATH_WARNING.description"), new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					new EventOption(this, new Func<Task>(this.Linger), "ABYSSAL_BATHS.pages.ALL.options.LINGER", Array.Empty<IHoverTip>()).ThatDoesDamage(num),
					new EventOption(this, new Func<Task>(this.ExitBaths), "ABYSSAL_BATHS.pages.ALL.options.EXIT_BATHS", Array.Empty<IHoverTip>())
				}));
			}
			else
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 1);
				defaultInterpolatedStringHandler.AppendLiteral("ABYSSAL_BATHS.pages.LINGER");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.LingerCount);
				defaultInterpolatedStringHandler.AppendLiteral(".description");
				this.SetEventState(base.L10NLookup(defaultInterpolatedStringHandler.ToStringAndClear()), new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					new EventOption(this, new Func<Task>(this.Linger), "ABYSSAL_BATHS.pages.ALL.options.LINGER", Array.Empty<IHoverTip>()).ThatDoesDamage(num),
					new EventOption(this, new Func<Task>(this.ExitBaths), "ABYSSAL_BATHS.pages.ALL.options.EXIT_BATHS", Array.Empty<IHoverTip>())
				}));
			}
		}

		// Token: 0x06006100 RID: 24832 RVA: 0x002441B7 File Offset: 0x002423B7
		private bool WillKillPlayer(decimal damage)
		{
			return base.Owner.Creature.CurrentHp <= damage;
		}

		// Token: 0x06006101 RID: 24833 RVA: 0x002441D4 File Offset: 0x002423D4
		private Task ExitBaths()
		{
			base.SetEventFinished(base.L10NLookup("ABYSSAL_BATHS.pages.EXIT_BATHS.description"));
			return Task.CompletedTask;
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x002441EC File Offset: 0x002423EC
		private async Task OnImmerse()
		{
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.Damage, null, null);
			base.DynamicVars.Damage.BaseValue += 1m;
		}

		// Token: 0x0400246A RID: 9322
		private const int _baseDamage = 3;

		// Token: 0x0400246B RID: 9323
		private const int _damageScaling = 1;

		// Token: 0x0400246C RID: 9324
		private int _lingerCount;
	}
}
