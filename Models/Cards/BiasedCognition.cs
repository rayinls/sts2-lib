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
	// Token: 0x020008AC RID: 2220
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BiasedCognition : CardModel
	{
		// Token: 0x0600676F RID: 26479 RVA: 0x00255616 File Offset: 0x00253816
		public BiasedCognition()
			: base(1, CardType.Power, CardRarity.Ancient, TargetType.Self, true)
		{
		}

		// Token: 0x17001ADF RID: 6879
		// (get) Token: 0x06006770 RID: 26480 RVA: 0x00255623 File Offset: 0x00253823
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<FocusPower>());
			}
		}

		// Token: 0x17001AE0 RID: 6880
		// (get) Token: 0x06006771 RID: 26481 RVA: 0x0025562F File Offset: 0x0025382F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<FocusPower>(4m),
					new PowerVar<BiasedCognitionPower>(1m)
				});
			}
		}

		// Token: 0x06006772 RID: 26482 RVA: 0x00255658 File Offset: 0x00253858
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<FocusPower>(base.Owner.Creature, base.DynamicVars["FocusPower"].BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<BiasedCognitionPower>(base.Owner.Creature, base.DynamicVars["BiasedCognitionPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006773 RID: 26483 RVA: 0x0025569B File Offset: 0x0025389B
		protected override void OnUpgrade()
		{
			base.DynamicVars["FocusPower"].UpgradeValueBy(1m);
		}
	}
}
