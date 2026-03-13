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
	// Token: 0x02000919 RID: 2329
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Demesne : CardModel
	{
		// Token: 0x0600699F RID: 27039 RVA: 0x002599F0 File Offset: 0x00257BF0
		public Demesne()
			: base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001BCC RID: 7116
		// (get) Token: 0x060069A0 RID: 27040 RVA: 0x002599FD File Offset: 0x00257BFD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar(1),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x17001BCD RID: 7117
		// (get) Token: 0x060069A1 RID: 27041 RVA: 0x00259A1C File Offset: 0x00257C1C
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Ethereal);
			}
		}

		// Token: 0x17001BCE RID: 7118
		// (get) Token: 0x060069A2 RID: 27042 RVA: 0x00259A24 File Offset: 0x00257C24
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x060069A3 RID: 27043 RVA: 0x00259A34 File Offset: 0x00257C34
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<DemesnePower>(base.Owner.Creature, base.DynamicVars.Cards.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060069A4 RID: 27044 RVA: 0x00259A77 File Offset: 0x00257C77
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
