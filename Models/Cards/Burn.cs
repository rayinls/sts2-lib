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
	// Token: 0x020008CA RID: 2250
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Burn : CardModel
	{
		// Token: 0x0600680D RID: 26637 RVA: 0x00256A82 File Offset: 0x00254C82
		public Burn()
			: base(-1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001B20 RID: 6944
		// (get) Token: 0x0600680E RID: 26638 RVA: 0x00256A8F File Offset: 0x00254C8F
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001B21 RID: 6945
		// (get) Token: 0x0600680F RID: 26639 RVA: 0x00256A92 File Offset: 0x00254C92
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(2m, ValueProp.Unpowered | ValueProp.Move));
			}
		}

		// Token: 0x17001B22 RID: 6946
		// (get) Token: 0x06006810 RID: 26640 RVA: 0x00256AA6 File Offset: 0x00254CA6
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x17001B23 RID: 6947
		// (get) Token: 0x06006811 RID: 26641 RVA: 0x00256AAE File Offset: 0x00254CAE
		public override bool HasTurnEndInHandEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B24 RID: 6948
		// (get) Token: 0x06006812 RID: 26642 RVA: 0x00256AB1 File Offset: 0x00254CB1
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NGroundFireVfx.AssetPaths;
			}
		}

		// Token: 0x06006813 RID: 26643 RVA: 0x00256AB8 File Offset: 0x00254CB8
		public override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NGroundFireVfx.Create(base.Owner.Creature, VfxColor.Red));
			}
			SfxCmd.Play("event:/sfx/characters/attack_fire", 1f);
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.Damage, this);
		}
	}
}
