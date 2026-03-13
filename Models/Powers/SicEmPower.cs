using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000696 RID: 1686
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SicEmPower : PowerModel
	{
		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06005552 RID: 21842 RVA: 0x0022648B File Offset: 0x0022468B
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06005553 RID: 21843 RVA: 0x0022648E File Offset: 0x0022468E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06005554 RID: 21844 RVA: 0x00226491 File Offset: 0x00224691
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonStatic, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x002264A4 File Offset: 0x002246A4
		public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, [Nullable(2)] Creature dealer, DamageResult result, ValueProp props, Creature target, [Nullable(2)] CardModel cardSource)
		{
			Osty osty = ((dealer != null) ? dealer.Monster : null) as Osty;
			if (osty != null)
			{
				if (osty.Creature.PetOwner != null && base.Applier != null)
				{
					if (osty.Creature.PetOwner.Creature == base.Applier)
					{
						if (target == base.Owner)
						{
							await OstyCmd.Summon(choiceContext, dealer.PetOwner, base.Amount, this);
						}
					}
				}
			}
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x00226500 File Offset: 0x00224700
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Remove(this);
			}
		}
	}
}
