using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000906 RID: 2310
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DeadlyPoison : CardModel
	{
		// Token: 0x06006932 RID: 26930 RVA: 0x00258E16 File Offset: 0x00257016
		public DeadlyPoison()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B96 RID: 7062
		// (get) Token: 0x06006933 RID: 26931 RVA: 0x00258E23 File Offset: 0x00257023
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NSmokePuffVfx.AssetPaths;
			}
		}

		// Token: 0x17001B97 RID: 7063
		// (get) Token: 0x06006934 RID: 26932 RVA: 0x00258E2A File Offset: 0x0025702A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PoisonPower>(5m));
			}
		}

		// Token: 0x17001B98 RID: 7064
		// (get) Token: 0x06006935 RID: 26933 RVA: 0x00258E3C File Offset: 0x0025703C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x06006936 RID: 26934 RVA: 0x00258E48 File Offset: 0x00257048
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			NPoisonImpactVfx npoisonImpactVfx = NPoisonImpactVfx.Create(cardPlay.Target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(npoisonImpactVfx);
			}
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<PoisonPower>(cardPlay.Target, base.DynamicVars.Poison.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006937 RID: 26935 RVA: 0x00258E93 File Offset: 0x00257093
		protected override void OnUpgrade()
		{
			base.DynamicVars.Poison.UpgradeValueBy(2m);
		}
	}
}
