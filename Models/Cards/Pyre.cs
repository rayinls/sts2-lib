using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A11 RID: 2577
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Pyre : CardModel
	{
		// Token: 0x06006ED5 RID: 28373 RVA: 0x002640DB File Offset: 0x002622DB
		public Pyre()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001DFE RID: 7678
		// (get) Token: 0x06006ED6 RID: 28374 RVA: 0x002640E8 File Offset: 0x002622E8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17001DFF RID: 7679
		// (get) Token: 0x06006ED7 RID: 28375 RVA: 0x002640F5 File Offset: 0x002622F5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x06006ED8 RID: 28376 RVA: 0x00264104 File Offset: 0x00262304
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<PyrePower>(base.Owner.Creature, base.DynamicVars.Energy.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006ED9 RID: 28377 RVA: 0x00264148 File Offset: 0x00262348
		public override async Task OnEnqueuePlayVfx([Nullable(2)] Creature target)
		{
			NFireBurningVfx nfireBurningVfx = NFireBurningVfx.Create(base.Owner.Creature, 1f, false);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(nfireBurningVfx);
			}
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
		}

		// Token: 0x06006EDA RID: 28378 RVA: 0x0026418B File Offset: 0x0026238B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}
