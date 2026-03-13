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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A4C RID: 2636
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Shroud : CardModel
	{
		// Token: 0x06007016 RID: 28694 RVA: 0x002667C0 File Offset: 0x002649C0
		public Shroud()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E86 RID: 7814
		// (get) Token: 0x06007017 RID: 28695 RVA: 0x002667CD File Offset: 0x002649CD
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<DoomPower>(),
					HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x17001E87 RID: 7815
		// (get) Token: 0x06007018 RID: 28696 RVA: 0x002667F0 File Offset: 0x002649F0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(2m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06007019 RID: 28697 RVA: 0x00266804 File Offset: 0x00264A04
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<ShroudPower>(base.Owner.Creature, base.DynamicVars.Block.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600701A RID: 28698 RVA: 0x00266847 File Offset: 0x00264A47
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(1m);
		}
	}
}
