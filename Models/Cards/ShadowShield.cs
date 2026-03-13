using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A44 RID: 2628
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ShadowShield : CardModel
	{
		// Token: 0x06006FE3 RID: 28643 RVA: 0x00266181 File Offset: 0x00264381
		public ShadowShield()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E6D RID: 7789
		// (get) Token: 0x06006FE4 RID: 28644 RVA: 0x0026618E File Offset: 0x0026438E
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E6E RID: 7790
		// (get) Token: 0x06006FE5 RID: 28645 RVA: 0x00266191 File Offset: 0x00264391
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(11m, ValueProp.Move));
			}
		}

		// Token: 0x17001E6F RID: 7791
		// (get) Token: 0x06006FE6 RID: 28646 RVA: 0x002661A5 File Offset: 0x002643A5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<DarkOrb>()
				});
			}
		}

		// Token: 0x06006FE7 RID: 28647 RVA: 0x002661C8 File Offset: 0x002643C8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await OrbCmd.Channel<DarkOrb>(choiceContext, base.Owner);
		}

		// Token: 0x06006FE8 RID: 28648 RVA: 0x0026621B File Offset: 0x0026441B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(4m);
		}
	}
}
