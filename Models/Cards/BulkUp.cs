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
	// Token: 0x020008C5 RID: 2245
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BulkUp : CardModel
	{
		// Token: 0x060067F5 RID: 26613 RVA: 0x00256733 File Offset: 0x00254933
		public BulkUp()
			: base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B17 RID: 6935
		// (get) Token: 0x060067F6 RID: 26614 RVA: 0x00256740 File Offset: 0x00254940
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrengthPower>(),
					HoverTipFactory.FromPower<DexterityPower>()
				});
			}
		}

		// Token: 0x17001B18 RID: 6936
		// (get) Token: 0x060067F7 RID: 26615 RVA: 0x0025675D File Offset: 0x0025495D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("OrbSlots", 1m),
					new PowerVar<StrengthPower>(2m),
					new PowerVar<DexterityPower>(2m)
				});
			}
		}

		// Token: 0x060067F8 RID: 26616 RVA: 0x00256798 File Offset: 0x00254998
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			OrbCmd.RemoveSlots(base.Owner, base.DynamicVars["OrbSlots"].IntValue);
			await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060067F9 RID: 26617 RVA: 0x002567DB File Offset: 0x002549DB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Strength.UpgradeValueBy(1m);
			base.DynamicVars.Dexterity.UpgradeValueBy(1m);
		}

		// Token: 0x0400255F RID: 9567
		private const string _orbSlotVar = "OrbSlots";
	}
}
