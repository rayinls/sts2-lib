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
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009B7 RID: 2487
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LightningRod : CardModel
	{
		// Token: 0x06006CED RID: 27885 RVA: 0x0026014F File Offset: 0x0025E34F
		public LightningRod()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001D35 RID: 7477
		// (get) Token: 0x06006CEE RID: 27886 RVA: 0x0026015C File Offset: 0x0025E35C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<LightningOrb>()
				});
			}
		}

		// Token: 0x17001D36 RID: 7478
		// (get) Token: 0x06006CEF RID: 27887 RVA: 0x0026017F File Offset: 0x0025E37F
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D37 RID: 7479
		// (get) Token: 0x06006CF0 RID: 27888 RVA: 0x00260182 File Offset: 0x0025E382
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(4m, ValueProp.Move),
					new PowerVar<LightningRodPower>(2m)
				});
			}
		}

		// Token: 0x06006CF1 RID: 27889 RVA: 0x002601AC File Offset: 0x0025E3AC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<LightningRodPower>(base.Owner.Creature, base.DynamicVars["LightningRodPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006CF2 RID: 27890 RVA: 0x002601F7 File Offset: 0x0025E3F7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
