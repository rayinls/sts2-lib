using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008B3 RID: 2227
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BloodWall : CardModel
	{
		// Token: 0x06006794 RID: 26516 RVA: 0x00255A52 File Offset: 0x00253C52
		public BloodWall()
			: base(2, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001AEF RID: 6895
		// (get) Token: 0x06006795 RID: 26517 RVA: 0x00255A5F File Offset: 0x00253C5F
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<string>(SceneHelper.GetScenePath("vfx/vfx_blood_wall"));
			}
		}

		// Token: 0x17001AF0 RID: 6896
		// (get) Token: 0x06006796 RID: 26518 RVA: 0x00255A70 File Offset: 0x00253C70
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001AF1 RID: 6897
		// (get) Token: 0x06006797 RID: 26519 RVA: 0x00255A73 File Offset: 0x00253C73
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HpLossVar(2m),
					new BlockVar(16m, ValueProp.Move)
				});
			}
		}

		// Token: 0x06006798 RID: 26520 RVA: 0x00255AA0 File Offset: 0x00253CA0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			VfxCmd.PlayOnCreatureCenter(base.Owner.Creature, "vfx/vfx_bloody_impact");
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			SfxCmd.Play("event:/sfx/characters/ironclad/ironclad_bloodwall", 1f);
			VfxCmd.PlayOnCreature(base.Owner.Creature, "vfx/vfx_blood_wall");
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006799 RID: 26521 RVA: 0x00255AF3 File Offset: 0x00253CF3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(4m);
		}

		// Token: 0x0400255A RID: 9562
		private const string _bloodWallVfxPath = "vfx/vfx_blood_wall";

		// Token: 0x0400255B RID: 9563
		private const string _bloodWallSfx = "event:/sfx/characters/ironclad/ironclad_bloodwall";
	}
}
