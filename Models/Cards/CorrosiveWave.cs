using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008F3 RID: 2291
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CorrosiveWave : CardModel
	{
		// Token: 0x060068D8 RID: 26840 RVA: 0x00258317 File Offset: 0x00256517
		public CorrosiveWave()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B73 RID: 7027
		// (get) Token: 0x060068D9 RID: 26841 RVA: 0x00258324 File Offset: 0x00256524
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("CorrosiveWave", 3m));
			}
		}

		// Token: 0x17001B74 RID: 7028
		// (get) Token: 0x060068DA RID: 26842 RVA: 0x0025833B File Offset: 0x0025653B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x060068DB RID: 26843 RVA: 0x00258348 File Offset: 0x00256548
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<CorrosiveWavePower>(base.Owner.Creature, base.DynamicVars["CorrosiveWave"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060068DC RID: 26844 RVA: 0x0025838B File Offset: 0x0025658B
		protected override void OnUpgrade()
		{
			base.DynamicVars["CorrosiveWave"].UpgradeValueBy(1m);
		}

		// Token: 0x04002568 RID: 9576
		private const string _powerKey = "CorrosiveWave";
	}
}
