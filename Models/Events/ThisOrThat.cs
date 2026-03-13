using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007F8 RID: 2040
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ThisOrThat : EventModel
	{
		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x060062EF RID: 25327 RVA: 0x0024D55B File Offset: 0x0024B75B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HpLossVar(6m),
					new GoldVar(0),
					new StringVar("Curse", ModelDb.Card<Clumsy>().Title)
				});
			}
		}

		// Token: 0x060062F0 RID: 25328 RVA: 0x0024D596 File Offset: 0x0024B796
		public override void CalculateVars()
		{
			base.DynamicVars.Gold.BaseValue = base.Rng.NextInt(41, 69);
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x0024D5BC File Offset: 0x0024B7BC
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Plain), "THIS_OR_THAT.pages.INITIAL.options.PLAIN", Array.Empty<IHoverTip>()).ThatDoesDamage(base.DynamicVars.HpLoss.IntValue),
				new EventOption(this, new Func<Task>(this.Ornate), "THIS_OR_THAT.pages.INITIAL.options.ORNATE", HoverTipFactory.FromCardWithCardHoverTips<Clumsy>(false))
			});
		}

		// Token: 0x060062F2 RID: 25330 RVA: 0x0024D630 File Offset: 0x0024B830
		private async Task Plain()
		{
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.HpLoss.IntValue, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
			await PlayerCmd.GainGold(base.DynamicVars.Gold.IntValue, base.Owner, false);
			base.SetEventFinished(base.L10NLookup("THIS_OR_THAT.pages.PLAIN.description"));
		}

		// Token: 0x060062F3 RID: 25331 RVA: 0x0024D674 File Offset: 0x0024B874
		private async Task Ornate()
		{
			RelicModel relicModel = RelicFactory.PullNextRelicFromFront(base.Owner).ToMutable();
			await RelicCmd.Obtain(relicModel, base.Owner, -1);
			await CardPileCmd.AddCurseToDeck<Clumsy>(base.Owner);
			base.SetEventFinished(base.L10NLookup("THIS_OR_THAT.pages.ORNATE.description"));
		}
	}
}
