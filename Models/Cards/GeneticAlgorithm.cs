using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200096A RID: 2410
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GeneticAlgorithm : CardModel
	{
		// Token: 0x06006B5B RID: 27483 RVA: 0x0025CF8B File Offset: 0x0025B18B
		public GeneticAlgorithm()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x06006B5C RID: 27484 RVA: 0x0025CF9F File Offset: 0x0025B19F
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x06006B5D RID: 27485 RVA: 0x0025CFA2 File Offset: 0x0025B1A2
		// (set) Token: 0x06006B5E RID: 27486 RVA: 0x0025CFAA File Offset: 0x0025B1AA
		[SavedProperty]
		public int CurrentBlock
		{
			get
			{
				return this._currentBlock;
			}
			set
			{
				base.AssertMutable();
				this._currentBlock = value;
				base.DynamicVars.Block.BaseValue = this._currentBlock;
			}
		}

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x06006B5F RID: 27487 RVA: 0x0025CFD4 File Offset: 0x0025B1D4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(this.CurrentBlock, ValueProp.Move),
					new IntVar("Increase", 3m)
				});
			}
		}

		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x06006B60 RID: 27488 RVA: 0x0025D008 File Offset: 0x0025B208
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x06006B61 RID: 27489 RVA: 0x0025D010 File Offset: 0x0025B210
		// (set) Token: 0x06006B62 RID: 27490 RVA: 0x0025D018 File Offset: 0x0025B218
		[SavedProperty]
		public int IncreasedBlock
		{
			get
			{
				return this._increasedBlock;
			}
			set
			{
				base.AssertMutable();
				this._increasedBlock = value;
			}
		}

		// Token: 0x06006B63 RID: 27491 RVA: 0x0025D028 File Offset: 0x0025B228
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			int intValue = base.DynamicVars["Increase"].IntValue;
			this.BuffFromPlay(intValue);
			GeneticAlgorithm geneticAlgorithm = base.DeckVersion as GeneticAlgorithm;
			if (geneticAlgorithm != null)
			{
				geneticAlgorithm.BuffFromPlay(intValue);
			}
		}

		// Token: 0x06006B64 RID: 27492 RVA: 0x0025D073 File Offset: 0x0025B273
		protected override void OnUpgrade()
		{
			base.DynamicVars["Increase"].UpgradeValueBy(1m);
		}

		// Token: 0x06006B65 RID: 27493 RVA: 0x0025D08F File Offset: 0x0025B28F
		protected override void AfterDowngraded()
		{
			this.UpdateBlock();
		}

		// Token: 0x06006B66 RID: 27494 RVA: 0x0025D097 File Offset: 0x0025B297
		private void BuffFromPlay(int extraBlock)
		{
			this.IncreasedBlock += extraBlock;
			this.UpdateBlock();
		}

		// Token: 0x06006B67 RID: 27495 RVA: 0x0025D0AD File Offset: 0x0025B2AD
		private void UpdateBlock()
		{
			this.CurrentBlock = 1 + this.IncreasedBlock;
		}

		// Token: 0x04002583 RID: 9603
		private const string _increaseKey = "Increase";

		// Token: 0x04002584 RID: 9604
		private int _currentBlock = 1;

		// Token: 0x04002585 RID: 9605
		private int _increasedBlock;
	}
}
