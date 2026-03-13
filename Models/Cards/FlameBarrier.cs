using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000953 RID: 2387
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FlameBarrier : CardModel
	{
		// Token: 0x06006ADB RID: 27355 RVA: 0x0025BF18 File Offset: 0x0025A118
		public FlameBarrier()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C5B RID: 7259
		// (get) Token: 0x06006ADC RID: 27356 RVA: 0x0025BF25 File Offset: 0x0025A125
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C5C RID: 7260
		// (get) Token: 0x06006ADD RID: 27357 RVA: 0x0025BF28 File Offset: 0x0025A128
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(12m, ValueProp.Move),
					new DynamicVar("DamageBack", 4m)
				});
			}
		}

		// Token: 0x06006ADE RID: 27358 RVA: 0x0025BF58 File Offset: 0x0025A158
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			NFireBurningVfx nfireBurningVfx = NFireBurningVfx.Create(base.Owner.Creature, 0.75f, false);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(nfireBurningVfx);
			}
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<FlameBarrierPower>(base.Owner.Creature, base.DynamicVars["DamageBack"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006ADF RID: 27359 RVA: 0x0025BFA3 File Offset: 0x0025A1A3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(4m);
			base.DynamicVars["DamageBack"].UpgradeValueBy(2m);
		}

		// Token: 0x0400257F RID: 9599
		private const string _damageBackKey = "DamageBack";
	}
}
