using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000513 RID: 1299
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HandDrill : RelicModel
	{
		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06004C13 RID: 19475 RVA: 0x00214F0F File Offset: 0x0021310F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x00214F12 File Offset: 0x00213112
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<VulnerablePower>(2m));
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06004C15 RID: 19477 RVA: 0x00214F24 File Offset: 0x00213124
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x06004C16 RID: 19478 RVA: 0x00214F30 File Offset: 0x00213130
		public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, [Nullable(2)] Creature dealer, DamageResult result, ValueProp props, Creature target, [Nullable(2)] CardModel cardSource)
		{
			if (dealer == base.Owner.Creature || ((dealer != null) ? dealer.PetOwner : null) == base.Owner)
			{
				if (result.WasBlockBroken)
				{
					base.Flash();
					await PowerCmd.Apply<VulnerablePower>(target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, null, false);
				}
			}
		}
	}
}
