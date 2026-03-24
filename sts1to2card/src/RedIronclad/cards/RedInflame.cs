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
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedInflame : CardModel
	{
		public RedInflame()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// (get) Token: 0x06006C74 RID: 27764 RVA: 0x0025F255 File Offset: 0x0025D455
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new PowerVar<StrengthPower>(2m) };
			}
		}

		// (get) Token: 0x06006C75 RID: 27765 RVA: 0x0025F267 File Offset: 0x0025D467
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.FromPower<StrengthPower>() };
			}
		}

		// (get) Token: 0x06006C76 RID: 27766 RVA: 0x0025F273 File Offset: 0x0025D473
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NGroundFireVfx.AssetPaths;
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			NPowerUpVfx.CreateNormal(base.Owner.Creature);
			await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars["StrengthPower"].BaseValue, base.Owner.Creature, this, false);
		}

		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NGroundFireVfx.Create(base.Owner.Creature, VfxColor.Red));
			}
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars["StrengthPower"].UpgradeValueBy(1m);
		}
	}
}
