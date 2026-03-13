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
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000945 RID: 2373
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Fasten : CardModel
	{
		// Token: 0x06006A8D RID: 27277 RVA: 0x0025B44F File Offset: 0x0025964F
		public Fasten()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C39 RID: 7225
		// (get) Token: 0x06006A8E RID: 27278 RVA: 0x0025B45C File Offset: 0x0025965C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				CardModel cardModel = null;
				if (base.IsMutable && base.Owner != null)
				{
					cardModel = base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint).First((CardModel c) => c.Tags.Contains(CardTag.Defend));
				}
				if (cardModel == null)
				{
					cardModel = ModelDb.Card<DefendIronclad>();
				}
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromCard(cardModel, false)
				});
			}
		}

		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x06006A8F RID: 27279 RVA: 0x0025B4FC File Offset: 0x002596FC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("ExtraBlock", 5m));
			}
		}

		// Token: 0x06006A90 RID: 27280 RVA: 0x0025B514 File Offset: 0x00259714
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<FastenPower>(base.Owner.Creature, base.DynamicVars["ExtraBlock"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006A91 RID: 27281 RVA: 0x0025B557 File Offset: 0x00259757
		protected override void OnUpgrade()
		{
			base.DynamicVars["ExtraBlock"].UpgradeValueBy(2m);
		}

		// Token: 0x0400257A RID: 9594
		private const string _extraBlockKey = "ExtraBlock";
	}
}
