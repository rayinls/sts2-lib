using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AA2 RID: 2722
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TrashToTreasure : CardModel
	{
		// Token: 0x060071EB RID: 29163 RVA: 0x0026A1D4 File Offset: 0x002683D4
		public TrashToTreasure()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001F40 RID: 8000
		// (get) Token: 0x060071EC RID: 29164 RVA: 0x0026A1E1 File Offset: 0x002683E1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x060071ED RID: 29165 RVA: 0x0026A1F4 File Offset: 0x002683F4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<TrashToTreasurePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x060071EE RID: 29166 RVA: 0x0026A237 File Offset: 0x00268437
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Innate);
		}
	}
}
