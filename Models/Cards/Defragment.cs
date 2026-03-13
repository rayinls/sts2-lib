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
	// Token: 0x02000916 RID: 2326
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Defragment : CardModel
	{
		// Token: 0x0600698D RID: 27021 RVA: 0x002597E3 File Offset: 0x002579E3
		public Defragment()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001BC3 RID: 7107
		// (get) Token: 0x0600698E RID: 27022 RVA: 0x002597F0 File Offset: 0x002579F0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<FocusPower>());
			}
		}

		// Token: 0x17001BC4 RID: 7108
		// (get) Token: 0x0600698F RID: 27023 RVA: 0x002597FC File Offset: 0x002579FC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<FocusPower>(1m));
			}
		}

		// Token: 0x06006990 RID: 27024 RVA: 0x00259810 File Offset: 0x00257A10
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<FocusPower>(base.Owner.Creature, base.DynamicVars["FocusPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006991 RID: 27025 RVA: 0x00259853 File Offset: 0x00257A53
		protected override void OnUpgrade()
		{
			base.DynamicVars["FocusPower"].UpgradeValueBy(1m);
		}
	}
}
