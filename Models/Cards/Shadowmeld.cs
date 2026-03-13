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
	// Token: 0x02000A43 RID: 2627
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Shadowmeld : CardModel
	{
		// Token: 0x06006FDE RID: 28638 RVA: 0x002660FB File Offset: 0x002642FB
		public Shadowmeld()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E6B RID: 7787
		// (get) Token: 0x06006FDF RID: 28639 RVA: 0x00266108 File Offset: 0x00264308
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Power", 1m));
			}
		}

		// Token: 0x17001E6C RID: 7788
		// (get) Token: 0x06006FE0 RID: 28640 RVA: 0x0026611E File Offset: 0x0026431E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06006FE1 RID: 28641 RVA: 0x00266130 File Offset: 0x00264330
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<ShadowmeldPower>(base.Owner.Creature, base.DynamicVars["Power"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006FE2 RID: 28642 RVA: 0x00266173 File Offset: 0x00264373
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}

		// Token: 0x040025C9 RID: 9673
		private const string _powerKey = "Power";
	}
}
