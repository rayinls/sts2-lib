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
	// Token: 0x02000987 RID: 2439
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Hegemony : CardModel
	{
		// Token: 0x06006BF8 RID: 27640 RVA: 0x0025E2D3 File Offset: 0x0025C4D3
		public Hegemony()
			: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CD3 RID: 7379
		// (get) Token: 0x06006BF9 RID: 27641 RVA: 0x0025E2E0 File Offset: 0x0025C4E0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(15m, ValueProp.Move),
					new EnergyVar(2)
				});
			}
		}

		// Token: 0x17001CD4 RID: 7380
		// (get) Token: 0x06006BFA RID: 27642 RVA: 0x0025E306 File Offset: 0x0025C506
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x06006BFB RID: 27643 RVA: 0x0025E314 File Offset: 0x0025C514
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			await PowerCmd.Apply<EnergyNextTurnPower>(base.Owner.Creature, base.DynamicVars.Energy.IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006BFC RID: 27644 RVA: 0x0025E367 File Offset: 0x0025C567
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}
