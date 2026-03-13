using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200099B RID: 2459
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Infection : CardModel
	{
		// Token: 0x06006C60 RID: 27744 RVA: 0x0025F07C File Offset: 0x0025D27C
		public Infection()
			: base(-1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001CFC RID: 7420
		// (get) Token: 0x06006C61 RID: 27745 RVA: 0x0025F089 File Offset: 0x0025D289
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001CFD RID: 7421
		// (get) Token: 0x06006C62 RID: 27746 RVA: 0x0025F08C File Offset: 0x0025D28C
		public override bool HasBuiltInOverlay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001CFE RID: 7422
		// (get) Token: 0x06006C63 RID: 27747 RVA: 0x0025F08F File Offset: 0x0025D28F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(3m, ValueProp.Unpowered | ValueProp.Move));
			}
		}

		// Token: 0x17001CFF RID: 7423
		// (get) Token: 0x06006C64 RID: 27748 RVA: 0x0025F0A3 File Offset: 0x0025D2A3
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x17001D00 RID: 7424
		// (get) Token: 0x06006C65 RID: 27749 RVA: 0x0025F0AB File Offset: 0x0025D2AB
		public override bool HasTurnEndInHandEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006C66 RID: 27750 RVA: 0x0025F0B0 File Offset: 0x0025D2B0
		public override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
		{
			VfxCmd.PlayOnCreatureCenter(base.Owner.Creature, "vfx/vfx_bloody_impact");
			NWormyImpactVfx nwormyImpactVfx = NWormyImpactVfx.Create(base.Owner.Creature);
			if (nwormyImpactVfx != null)
			{
				NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(nwormyImpactVfx);
			}
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.Damage, this);
		}
	}
}
