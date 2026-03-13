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
	// Token: 0x02000A15 RID: 2581
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Rage : CardModel
	{
		// Token: 0x06006EE7 RID: 28391 RVA: 0x0026436A File Offset: 0x0026256A
		public Rage()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E04 RID: 7684
		// (get) Token: 0x06006EE8 RID: 28392 RVA: 0x00264377 File Offset: 0x00262577
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Power", 3m));
			}
		}

		// Token: 0x17001E05 RID: 7685
		// (get) Token: 0x06006EE9 RID: 28393 RVA: 0x0026438E File Offset: 0x0026258E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06006EEA RID: 28394 RVA: 0x002643A0 File Offset: 0x002625A0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<RagePower>(base.Owner.Creature, base.DynamicVars["Power"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006EEB RID: 28395 RVA: 0x002643E3 File Offset: 0x002625E3
		protected override void OnUpgrade()
		{
			base.DynamicVars["Power"].UpgradeValueBy(2m);
		}

		// Token: 0x040025C2 RID: 9666
		private const string _powerKey = "Power";
	}
}
