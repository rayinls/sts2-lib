using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008EA RID: 2282
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Compact : CardModel
	{
		// Token: 0x060068AA RID: 26794 RVA: 0x00257CD0 File Offset: 0x00255ED0
		public Compact()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B60 RID: 7008
		// (get) Token: 0x060068AB RID: 26795 RVA: 0x00257CDD File Offset: 0x00255EDD
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B61 RID: 7009
		// (get) Token: 0x060068AC RID: 26796 RVA: 0x00257CE0 File Offset: 0x00255EE0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromCard<Fuel>(base.IsUpgraded),
					HoverTipFactory.Static(StaticHoverTip.Transform, Array.Empty<DynamicVar>()),
					HoverTipFactory.ForEnergy(this)
				});
			}
		}

		// Token: 0x17001B62 RID: 7010
		// (get) Token: 0x060068AD RID: 26797 RVA: 0x00257D12 File Offset: 0x00255F12
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(6m, ValueProp.Move));
			}
		}

		// Token: 0x060068AE RID: 26798 RVA: 0x00257D28 File Offset: 0x00255F28
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.IsTransformable && c.Type == CardType.Status).ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				CardModel cardModel2 = base.CombatState.CreateCard<Fuel>(base.Owner);
				if (base.IsUpgraded)
				{
					CardCmd.Upgrade(cardModel2, CardPreviewStyle.HorizontalLayout);
				}
				await CardCmd.Transform(cardModel, cardModel2, CardPreviewStyle.HorizontalLayout);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
		}

		// Token: 0x060068AF RID: 26799 RVA: 0x00257D73 File Offset: 0x00255F73
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(1m);
		}
	}
}
