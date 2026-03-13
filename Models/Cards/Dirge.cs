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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200091F RID: 2335
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Dirge : CardModel
	{
		// Token: 0x060069BE RID: 27070 RVA: 0x00259D43 File Offset: 0x00257F43
		public Dirge()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001BDB RID: 7131
		// (get) Token: 0x060069BF RID: 27071 RVA: 0x00259D50 File Offset: 0x00257F50
		protected override bool HasEnergyCostX
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BDC RID: 7132
		// (get) Token: 0x060069C0 RID: 27072 RVA: 0x00259D53 File Offset: 0x00257F53
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new SummonVar(3m));
			}
		}

		// Token: 0x17001BDD RID: 7133
		// (get) Token: 0x060069C1 RID: 27073 RVA: 0x00259D65 File Offset: 0x00257F65
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new DynamicVar[] { base.DynamicVars.Summon }),
					HoverTipFactory.FromCard<Soul>(base.IsUpgraded)
				});
			}
		}

		// Token: 0x060069C2 RID: 27074 RVA: 0x00259DA0 File Offset: 0x00257FA0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			int xValue = base.ResolveEnergyXValue();
			for (int i = 0; i < xValue; i++)
			{
				await OstyCmd.Summon(choiceContext, base.Owner, base.DynamicVars.Summon.BaseValue, this);
			}
			List<Soul> list = Soul.Create(base.Owner, xValue, base.CombatState).ToList<Soul>();
			if (base.IsUpgraded)
			{
				foreach (Soul soul in list)
				{
					CardCmd.Upgrade(soul, CardPreviewStyle.HorizontalLayout);
				}
			}
			IReadOnlyList<CardPileAddResult> readOnlyList = await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Draw, true, CardPilePosition.Random);
			CardCmd.PreviewCardPileAdd(readOnlyList, 1.2f, CardPreviewStyle.HorizontalLayout);
		}

		// Token: 0x060069C3 RID: 27075 RVA: 0x00259DEB File Offset: 0x00257FEB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Summon.UpgradeValueBy(1m);
		}
	}
}
