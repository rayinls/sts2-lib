using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A7F RID: 2687
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Supercritical : CardModel
	{
		// Token: 0x0600712E RID: 28974 RVA: 0x00268B2B File Offset: 0x00266D2B
		public Supercritical()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001EF4 RID: 7924
		// (get) Token: 0x0600712F RID: 28975 RVA: 0x00268B38 File Offset: 0x00266D38
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001EF5 RID: 7925
		// (get) Token: 0x06007130 RID: 28976 RVA: 0x00268B40 File Offset: 0x00266D40
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x17001EF6 RID: 7926
		// (get) Token: 0x06007131 RID: 28977 RVA: 0x00268B4D File Offset: 0x00266D4D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(4));
			}
		}

		// Token: 0x06007132 RID: 28978 RVA: 0x00268B5C File Offset: 0x00266D5C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
		}

		// Token: 0x06007133 RID: 28979 RVA: 0x00268B9F File Offset: 0x00266D9F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(2m);
		}
	}
}
