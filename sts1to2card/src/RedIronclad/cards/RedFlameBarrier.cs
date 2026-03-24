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
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedFlameBarrier : CardModel
	{
		public RedFlameBarrier()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// (get) Token: 0x06006ADC RID: 27356 RVA: 0x0025BF25 File Offset: 0x0025A125
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// (get) Token: 0x06006ADD RID: 27357 RVA: 0x0025BF28 File Offset: 0x0025A128
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[]
				{
					new BlockVar(12m, ValueProp.Move),
					new DynamicVar("DamageBack", 4m)
				};
			}
		}

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

		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(4m);
			base.DynamicVars["DamageBack"].UpgradeValueBy(2m);
		}

		private const string _damageBackKey = "DamageBack";
	}
}
