using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009E0 RID: 2528
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NoEscape : CardModel
	{
		// Token: 0x06006DDD RID: 28125 RVA: 0x002620AD File Offset: 0x002602AD
		public NoEscape()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D9B RID: 7579
		// (get) Token: 0x06006DDE RID: 28126 RVA: 0x002620BC File Offset: 0x002602BC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[4];
				array[0] = new DynamicVar("DoomThreshold", 10m);
				array[1] = new CalculationBaseVar(10m);
				array[2] = new CalculationExtraVar(5m);
				array[3] = new CalculatedVar("CalculatedDoom").WithMultiplier(delegate(CardModel card, [Nullable(2)] Creature target)
				{
					int num = ((target != null) ? target.GetPowerAmount<DoomPower>() : 0);
					decimal baseValue = card.DynamicVars["DoomThreshold"].BaseValue;
					return Math.Floor(num / baseValue);
				});
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x17001D9C RID: 7580
		// (get) Token: 0x06006DDF RID: 28127 RVA: 0x00262136 File Offset: 0x00260336
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x06006DE0 RID: 28128 RVA: 0x00262144 File Offset: 0x00260344
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await PowerCmd.Apply<DoomPower>(cardPlay.Target, ((CalculatedVar)base.DynamicVars["CalculatedDoom"]).Calculate(cardPlay.Target), base.Owner.Creature, this, false);
		}

		// Token: 0x06006DE1 RID: 28129 RVA: 0x0026218F File Offset: 0x0026038F
		protected override void OnUpgrade()
		{
			base.DynamicVars.CalculationBase.UpgradeValueBy(5m);
		}

		// Token: 0x040025B5 RID: 9653
		private const string _calculatedDoomKey = "CalculatedDoom";

		// Token: 0x040025B6 RID: 9654
		private const string _doomThresholdKey = "DoomThreshold";
	}
}
