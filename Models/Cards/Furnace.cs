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
	// Token: 0x02000964 RID: 2404
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Furnace : CardModel
	{
		// Token: 0x06006B3E RID: 27454 RVA: 0x0025CBB6 File Offset: 0x0025ADB6
		public Furnace()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x06006B3F RID: 27455 RVA: 0x0025CBC3 File Offset: 0x0025ADC3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x06006B40 RID: 27456 RVA: 0x0025CBCA File Offset: 0x0025ADCA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new ForgeVar(4));
			}
		}

		// Token: 0x06006B41 RID: 27457 RVA: 0x0025CBD8 File Offset: 0x0025ADD8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<FurnacePower>(base.Owner.Creature, base.DynamicVars.Forge.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006B42 RID: 27458 RVA: 0x0025CC1B File Offset: 0x0025AE1B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Forge.UpgradeValueBy(2m);
		}
	}
}
