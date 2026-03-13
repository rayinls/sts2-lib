using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005B2 RID: 1458
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TuningFork : RelicModel
	{
		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x0600502E RID: 20526 RVA: 0x0021C820 File Offset: 0x0021AA20
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x0600502F RID: 20527 RVA: 0x0021C823 File Offset: 0x0021AA23
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x0021C826 File Offset: 0x0021AA26
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.SkillsPlayed;
				}
				return base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x0021C847 File Offset: 0x0021AA47
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06005032 RID: 20530 RVA: 0x0021C859 File Offset: 0x0021AA59
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(10),
					new BlockVar(7m, ValueProp.Unpowered)
				});
			}
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06005033 RID: 20531 RVA: 0x0021C87F File Offset: 0x0021AA7F
		// (set) Token: 0x06005034 RID: 20532 RVA: 0x0021C887 File Offset: 0x0021AA87
		private bool IsActivating
		{
			get
			{
				return this._isActivating;
			}
			set
			{
				base.AssertMutable();
				this._isActivating = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06005035 RID: 20533 RVA: 0x0021C89C File Offset: 0x0021AA9C
		// (set) Token: 0x06005036 RID: 20534 RVA: 0x0021C8A4 File Offset: 0x0021AAA4
		[SavedProperty]
		public int SkillsPlayed
		{
			get
			{
				return this._skillsPlayed;
			}
			private set
			{
				base.AssertMutable();
				if (this._skillsPlayed != value)
				{
					this._skillsPlayed = value;
					this.UpdateDisplay();
				}
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06005037 RID: 20535 RVA: 0x0021C8C2 File Offset: 0x0021AAC2
		private int SkillsThreshold
		{
			get
			{
				return base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x0021C8D4 File Offset: 0x0021AAD4
		private void UpdateDisplay()
		{
			if (this.IsActivating)
			{
				base.Status = RelicStatus.Normal;
			}
			else
			{
				base.Status = ((this.SkillsPlayed == this.SkillsThreshold - 1) ? RelicStatus.Active : RelicStatus.Normal);
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x0021C908 File Offset: 0x0021AB08
		public void NotifySkillPlayed()
		{
			int skillsPlayed = this.SkillsPlayed;
			this.SkillsPlayed = skillsPlayed + 1;
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x0021C928 File Offset: 0x0021AB28
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (cardPlay.Card.Type == CardType.Skill)
				{
					int skillsPlayed = this.SkillsPlayed;
					this.SkillsPlayed = skillsPlayed + 1;
					if (this.SkillsPlayed >= this.SkillsThreshold)
					{
						TaskHelper.RunSafely(this.DoActivateVisuals());
						await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
						this.SkillsPlayed -= this.SkillsThreshold;
					}
				}
			}
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x0021C974 File Offset: 0x0021AB74
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x04002236 RID: 8758
		private bool _isActivating;

		// Token: 0x04002237 RID: 8759
		private int _skillsPlayed;
	}
}
