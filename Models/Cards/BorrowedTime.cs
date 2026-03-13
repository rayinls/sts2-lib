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
	// Token: 0x020008BD RID: 2237
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BorrowedTime : CardModel
	{
		// Token: 0x060067CD RID: 26573 RVA: 0x002561BB File Offset: 0x002543BB
		public BorrowedTime()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B07 RID: 6919
		// (get) Token: 0x060067CE RID: 26574 RVA: 0x002561C8 File Offset: 0x002543C8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<DoomPower>(3m),
					new EnergyVar(1)
				});
			}
		}

		// Token: 0x17001B08 RID: 6920
		// (get) Token: 0x060067CF RID: 26575 RVA: 0x002561EC File Offset: 0x002543EC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<DoomPower>(),
					base.EnergyHoverTip
				});
			}
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x0025620C File Offset: 0x0025440C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<DoomPower>(base.Owner.Creature, base.DynamicVars.Doom.BaseValue, base.Owner.Creature, this, false);
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
		}

		// Token: 0x060067D1 RID: 26577 RVA: 0x0025624F File Offset: 0x0025444F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}

		// Token: 0x0400255D RID: 9565
		public const int doomAmount = 3;
	}
}
