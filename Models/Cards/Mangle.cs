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
	// Token: 0x020009BF RID: 2495
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Mangle : CardModel
	{
		// Token: 0x06006D2A RID: 27946 RVA: 0x00260A8B File Offset: 0x0025EC8B
		public Mangle()
			: base(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D4E RID: 7502
		// (get) Token: 0x06006D2B RID: 27947 RVA: 0x00260A98 File Offset: 0x0025EC98
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(15m, ValueProp.Move),
					new DynamicVar("StrengthLoss", 10m)
				});
			}
		}

		// Token: 0x17001D4F RID: 7503
		// (get) Token: 0x06006D2C RID: 27948 RVA: 0x00260AC9 File Offset: 0x0025ECC9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06006D2D RID: 27949 RVA: 0x00260AD8 File Offset: 0x0025ECD8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "heavy_attack.mp3")
				.Execute(choiceContext);
			await PowerCmd.Apply<ManglePower>(cardPlay.Target, base.DynamicVars["StrengthLoss"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006D2E RID: 27950 RVA: 0x00260B2B File Offset: 0x0025ED2B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
			base.DynamicVars["StrengthLoss"].UpgradeValueBy(5m);
		}

		// Token: 0x040025AF RID: 9647
		private const string _strengthLossKey = "StrengthLoss";
	}
}
