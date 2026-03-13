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
	// Token: 0x020009E3 RID: 2531
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NoxiousFumes : CardModel
	{
		// Token: 0x06006DEE RID: 28142 RVA: 0x0026231D File Offset: 0x0026051D
		public NoxiousFumes()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DA3 RID: 7587
		// (get) Token: 0x06006DEF RID: 28143 RVA: 0x0026232A File Offset: 0x0026052A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("PoisonPerTurn", 2m));
			}
		}

		// Token: 0x17001DA4 RID: 7588
		// (get) Token: 0x06006DF0 RID: 28144 RVA: 0x00262341 File Offset: 0x00260541
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x06006DF1 RID: 28145 RVA: 0x00262350 File Offset: 0x00260550
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<NoxiousFumesPower>(base.Owner.Creature, base.DynamicVars["PoisonPerTurn"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006DF2 RID: 28146 RVA: 0x00262393 File Offset: 0x00260593
		protected override void OnUpgrade()
		{
			base.DynamicVars["PoisonPerTurn"].UpgradeValueBy(1m);
		}

		// Token: 0x040025B9 RID: 9657
		private const string _poisonPerTurnKey = "PoisonPerTurn";
	}
}
