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
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AAC RID: 2732
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Unmovable : CardModel
	{
		// Token: 0x0600721E RID: 29214 RVA: 0x0026A7BF File Offset: 0x002689BF
		public Unmovable()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001F55 RID: 8021
		// (get) Token: 0x0600721F RID: 29215 RVA: 0x0026A7CC File Offset: 0x002689CC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06007220 RID: 29216 RVA: 0x0026A7E0 File Offset: 0x002689E0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			NPowerUpVfx.CreateNormal(base.Owner.Creature);
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<UnmovablePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06007221 RID: 29217 RVA: 0x0026A823 File Offset: 0x00268A23
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
