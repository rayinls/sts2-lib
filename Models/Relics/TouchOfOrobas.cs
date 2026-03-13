using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005AC RID: 1452
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TouchOfOrobas : RelicModel
	{
		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06004FFB RID: 20475 RVA: 0x0021C15B File Offset: 0x0021A35B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06004FFC RID: 20476 RVA: 0x0021C160 File Offset: 0x0021A360
		private static Dictionary<ModelId, RelicModel> RefinementUpgrades
		{
			get
			{
				return new Dictionary<ModelId, RelicModel>
				{
					{
						ModelDb.Relic<BurningBlood>().Id,
						ModelDb.Relic<BlackBlood>()
					},
					{
						ModelDb.Relic<RingOfTheSnake>().Id,
						ModelDb.Relic<RingOfTheDrake>()
					},
					{
						ModelDb.Relic<DivineRight>().Id,
						ModelDb.Relic<DivineDestiny>()
					},
					{
						ModelDb.Relic<BoundPhylactery>().Id,
						ModelDb.Relic<PhylacteryUnbound>()
					},
					{
						ModelDb.Relic<CrackedCore>().Id,
						ModelDb.Relic<InfusedCore>()
					}
				};
			}
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06004FFD RID: 20477 RVA: 0x0021C1DB File Offset: 0x0021A3DB
		// (set) Token: 0x06004FFE RID: 20478 RVA: 0x0021C1E4 File Offset: 0x0021A3E4
		[Nullable(2)]
		[SavedProperty]
		public ModelId StarterRelic
		{
			[NullableContext(2)]
			get
			{
				return this._starterRelic;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				if (this._starterRelic != null)
				{
					throw new InvalidOperationException("Recursive Core setup called twice!");
				}
				this._starterRelic = value;
				if (this._starterRelic != null)
				{
					RelicModel relicModel = SaveUtil.RelicOrDeprecated(this._starterRelic);
					this._extraHoverTips.AddRange(relicModel.HoverTips);
					((StringVar)base.DynamicVars["StarterRelic"]).StringValue = relicModel.Title.GetFormattedText();
				}
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06004FFF RID: 20479 RVA: 0x0021C267 File Offset: 0x0021A467
		// (set) Token: 0x06005000 RID: 20480 RVA: 0x0021C270 File Offset: 0x0021A470
		[Nullable(2)]
		[SavedProperty]
		public ModelId UpgradedRelic
		{
			[NullableContext(2)]
			get
			{
				return this._upgradedRelic;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				if (this._upgradedRelic != null)
				{
					throw new InvalidOperationException("Recursive Core setup called twice!");
				}
				this._upgradedRelic = value;
				if (this._upgradedRelic != null)
				{
					RelicModel relicModel = SaveUtil.RelicOrDeprecated(this._upgradedRelic);
					this._extraHoverTips.AddRange(relicModel.HoverTips);
					((StringVar)base.DynamicVars["UpgradedRelic"]).StringValue = relicModel.Title.GetFormattedText();
				}
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06005001 RID: 20481 RVA: 0x0021C2F3 File Offset: 0x0021A4F3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return this._extraHoverTips;
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06005002 RID: 20482 RVA: 0x0021C2FB File Offset: 0x0021A4FB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("StarterRelic", ""),
					new StringVar("UpgradedRelic", "")
				});
			}
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0021C32C File Offset: 0x0021A52C
		protected override void AfterCloned()
		{
			base.AfterCloned();
			this._extraHoverTips = new List<IHoverTip>();
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0021C33F File Offset: 0x0021A53F
		[return: Nullable(2)]
		private RelicModel GetStarterRelic(Player p)
		{
			return p.Relics.FirstOrDefault((RelicModel r) => r.Rarity == RelicRarity.Starter);
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x0021C36C File Offset: 0x0021A56C
		public RelicModel GetUpgradedStarterRelic(RelicModel starterRelic)
		{
			RelicModel relicModel;
			if (TouchOfOrobas.RefinementUpgrades.TryGetValue(starterRelic.Id, out relicModel))
			{
				return relicModel;
			}
			return ModelDb.Relic<Circlet>().ToMutable();
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x0021C39C File Offset: 0x0021A59C
		public bool SetupForPlayer(Player player)
		{
			base.AssertMutable();
			RelicModel starterRelic = this.GetStarterRelic(player);
			if (starterRelic != null)
			{
				this.StarterRelic = starterRelic.Id;
				this.UpgradedRelic = this.GetUpgradedStarterRelic(starterRelic).Id;
				return true;
			}
			return false;
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0021C3DB File Offset: 0x0021A5DB
		public void SetupForTests(ModelId starterRelic, ModelId upgradedRelic)
		{
			base.AssertMutable();
			this.StarterRelic = starterRelic;
			this.UpgradedRelic = upgradedRelic;
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x0021C3F4 File Offset: 0x0021A5F4
		public override async Task AfterObtained()
		{
			ModelId modelId;
			if ((modelId = this.StarterRelic) == null)
			{
				modelId = base.Owner.Relics.First((RelicModel r) => r.Rarity == RelicRarity.Starter).Id;
			}
			ModelId modelId2 = modelId;
			RelicModel relicById = base.Owner.GetRelicById(modelId2);
			ModelId modelId3 = this.UpgradedRelic ?? this.GetUpgradedStarterRelic(relicById).Id;
			RelicModel relicModel = ModelDb.GetById<RelicModel>(modelId3).ToMutable();
			await RelicCmd.Replace(relicById, relicModel);
		}

		// Token: 0x0400222C RID: 8748
		private const string _starterRelicKey = "StarterRelic";

		// Token: 0x0400222D RID: 8749
		private const string _upgradedRelicKey = "UpgradedRelic";

		// Token: 0x0400222E RID: 8750
		[Nullable(2)]
		private ModelId _starterRelic;

		// Token: 0x0400222F RID: 8751
		[Nullable(2)]
		private ModelId _upgradedRelic;

		// Token: 0x04002230 RID: 8752
		private List<IHoverTip> _extraHoverTips = new List<IHoverTip>();
	}
}
