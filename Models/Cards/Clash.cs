using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008E0 RID: 2272
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Clash : CardModel
	{
		// Token: 0x06006871 RID: 26737 RVA: 0x002575EB File Offset: 0x002557EB
		public Clash()
			: base(0, CardType.Attack, CardRarity.Event, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B46 RID: 6982
		// (get) Token: 0x06006872 RID: 26738 RVA: 0x002575F8 File Offset: 0x002557F8
		public override CardPoolModel VisualCardPool
		{
			get
			{
				return ModelDb.CardPool<IroncladCardPool>();
			}
		}

		// Token: 0x17001B47 RID: 6983
		// (get) Token: 0x06006873 RID: 26739 RVA: 0x002575FF File Offset: 0x002557FF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(14m, ValueProp.Move));
			}
		}

		// Token: 0x17001B48 RID: 6984
		// (get) Token: 0x06006874 RID: 26740 RVA: 0x00257613 File Offset: 0x00255813
		protected override bool IsPlayable
		{
			get
			{
				return CardPile.GetCards(base.Owner, new PileType[] { PileType.Hand }).All((CardModel c) => c.Type == CardType.Attack);
			}
		}

		// Token: 0x17001B49 RID: 6985
		// (get) Token: 0x06006875 RID: 26741 RVA: 0x0025764E File Offset: 0x0025584E
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return this.IsPlayable;
			}
		}

		// Token: 0x06006876 RID: 26742 RVA: 0x00257658 File Offset: 0x00255858
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006877 RID: 26743 RVA: 0x002576AB File Offset: 0x002558AB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}
