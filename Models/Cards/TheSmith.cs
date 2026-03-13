using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A96 RID: 2710
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheSmith : CardModel
	{
		// Token: 0x060071A9 RID: 29097 RVA: 0x00269A40 File Offset: 0x00267C40
		public TheSmith()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001F25 RID: 7973
		// (get) Token: 0x060071AA RID: 29098 RVA: 0x00269A4D File Offset: 0x00267C4D
		public override int CanonicalStarCost
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17001F26 RID: 7974
		// (get) Token: 0x060071AB RID: 29099 RVA: 0x00269A50 File Offset: 0x00267C50
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new ForgeVar(30));
			}
		}

		// Token: 0x17001F27 RID: 7975
		// (get) Token: 0x060071AC RID: 29100 RVA: 0x00269A5E File Offset: 0x00267C5E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x060071AD RID: 29101 RVA: 0x00269A68 File Offset: 0x00267C68
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await ForgeCmd.Forge(base.DynamicVars.Forge.IntValue, base.Owner, this);
		}

		// Token: 0x060071AE RID: 29102 RVA: 0x00269AAB File Offset: 0x00267CAB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Forge.UpgradeValueBy(10m);
		}
	}
}
