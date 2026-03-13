using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008D4 RID: 2260
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Caltrops : CardModel
	{
		// Token: 0x06006839 RID: 26681 RVA: 0x00256F1C File Offset: 0x0025511C
		public Caltrops()
			: base(1, CardType.Power, CardRarity.Event, TargetType.Self, true)
		{
		}

		// Token: 0x17001B30 RID: 6960
		// (get) Token: 0x0600683A RID: 26682 RVA: 0x00256F29 File Offset: 0x00255129
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<ThornsPower>());
			}
		}

		// Token: 0x17001B31 RID: 6961
		// (get) Token: 0x0600683B RID: 26683 RVA: 0x00256F35 File Offset: 0x00255135
		public override CardPoolModel VisualCardPool
		{
			get
			{
				return ModelDb.CardPool<SilentCardPool>();
			}
		}

		// Token: 0x17001B32 RID: 6962
		// (get) Token: 0x0600683C RID: 26684 RVA: 0x00256F3C File Offset: 0x0025513C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<ThornsPower>(3m));
			}
		}

		// Token: 0x0600683D RID: 26685 RVA: 0x00256F50 File Offset: 0x00255150
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<ThornsPower>(base.Owner.Creature, base.DynamicVars["ThornsPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600683E RID: 26686 RVA: 0x00256F93 File Offset: 0x00255193
		protected override void OnUpgrade()
		{
			base.DynamicVars["ThornsPower"].UpgradeValueBy(2m);
		}
	}
}
