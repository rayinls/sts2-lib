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
	// Token: 0x02000A47 RID: 2631
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SharedFate : CardModel
	{
		// Token: 0x06006FF4 RID: 28660 RVA: 0x0026632B File Offset: 0x0026452B
		public SharedFate()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E76 RID: 7798
		// (get) Token: 0x06006FF5 RID: 28661 RVA: 0x00266338 File Offset: 0x00264538
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001E77 RID: 7799
		// (get) Token: 0x06006FF6 RID: 28662 RVA: 0x00266340 File Offset: 0x00264540
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("EnemyStrengthLoss", 2m),
					new DynamicVar("PlayerStrengthLoss", 2m)
				});
			}
		}

		// Token: 0x17001E78 RID: 7800
		// (get) Token: 0x06006FF7 RID: 28663 RVA: 0x00266373 File Offset: 0x00264573
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06006FF8 RID: 28664 RVA: 0x00266380 File Offset: 0x00264580
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, -base.DynamicVars["PlayerStrengthLoss"].BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<StrengthPower>(cardPlay.Target, -base.DynamicVars["EnemyStrengthLoss"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006FF9 RID: 28665 RVA: 0x002663CB File Offset: 0x002645CB
		protected override void OnUpgrade()
		{
			base.DynamicVars["EnemyStrengthLoss"].UpgradeValueBy(1m);
		}

		// Token: 0x040025CB RID: 9675
		private const string _enemyStrengthLossKey = "EnemyStrengthLoss";

		// Token: 0x040025CC RID: 9676
		private const string _playerStrengthLossKey = "PlayerStrengthLoss";
	}
}
