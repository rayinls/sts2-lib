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
	// Token: 0x02000892 RID: 2194
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Anticipate : CardModel
	{
		// Token: 0x060066EA RID: 26346 RVA: 0x00254524 File Offset: 0x00252724
		public Anticipate()
			: base(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001AA6 RID: 6822
		// (get) Token: 0x060066EB RID: 26347 RVA: 0x00254531 File Offset: 0x00252731
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<DexterityPower>(3m));
			}
		}

		// Token: 0x17001AA7 RID: 6823
		// (get) Token: 0x060066EC RID: 26348 RVA: 0x00254543 File Offset: 0x00252743
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DexterityPower>());
			}
		}

		// Token: 0x060066ED RID: 26349 RVA: 0x00254550 File Offset: 0x00252750
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<AnticipatePower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060066EE RID: 26350 RVA: 0x00254593 File Offset: 0x00252793
		protected override void OnUpgrade()
		{
			base.DynamicVars.Dexterity.UpgradeValueBy(2m);
		}
	}
}
