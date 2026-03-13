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
	// Token: 0x02000AB4 RID: 2740
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Vicious : CardModel
	{
		// Token: 0x06007245 RID: 29253 RVA: 0x0026AC52 File Offset: 0x00268E52
		public Vicious()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001F63 RID: 8035
		// (get) Token: 0x06007246 RID: 29254 RVA: 0x0026AC5F File Offset: 0x00268E5F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x17001F64 RID: 8036
		// (get) Token: 0x06007247 RID: 29255 RVA: 0x0026AC6B File Offset: 0x00268E6B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x06007248 RID: 29256 RVA: 0x0026AC78 File Offset: 0x00268E78
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<ViciousPower>(base.Owner.Creature, base.DynamicVars.Cards.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06007249 RID: 29257 RVA: 0x0026ACBB File Offset: 0x00268EBB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
