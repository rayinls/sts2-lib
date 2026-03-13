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
	// Token: 0x0200099F RID: 2463
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Inflame : CardModel
	{
		// Token: 0x06006C73 RID: 27763 RVA: 0x0025F248 File Offset: 0x0025D448
		public Inflame()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001D04 RID: 7428
		// (get) Token: 0x06006C74 RID: 27764 RVA: 0x0025F255 File Offset: 0x0025D455
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(2m));
			}
		}

		// Token: 0x17001D05 RID: 7429
		// (get) Token: 0x06006C75 RID: 27765 RVA: 0x0025F267 File Offset: 0x0025D467
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17001D06 RID: 7430
		// (get) Token: 0x06006C76 RID: 27766 RVA: 0x0025F273 File Offset: 0x0025D473
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NGroundFireVfx.AssetPaths;
			}
		}

		// Token: 0x06006C77 RID: 27767 RVA: 0x0025F27C File Offset: 0x0025D47C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			NPowerUpVfx.CreateNormal(base.Owner.Creature);
			await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars["StrengthPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006C78 RID: 27768 RVA: 0x0025F2C0 File Offset: 0x0025D4C0
		public override async Task OnEnqueuePlayVfx([Nullable(2)] Creature target)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NGroundFireVfx.Create(base.Owner.Creature, VfxColor.Red));
			}
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
		}

		// Token: 0x06006C79 RID: 27769 RVA: 0x0025F303 File Offset: 0x0025D503
		protected override void OnUpgrade()
		{
			base.DynamicVars["StrengthPower"].UpgradeValueBy(1m);
		}
	}
}
