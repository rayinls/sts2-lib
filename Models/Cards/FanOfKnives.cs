using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
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
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000944 RID: 2372
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FanOfKnives : CardModel
	{
		// Token: 0x06006A86 RID: 27270 RVA: 0x0025B377 File Offset: 0x00259577
		public FanOfKnives()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001C36 RID: 7222
		// (get) Token: 0x06006A87 RID: 27271 RVA: 0x0025B384 File Offset: 0x00259584
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar("Shivs", 4));
			}
		}

		// Token: 0x17001C37 RID: 7223
		// (get) Token: 0x06006A88 RID: 27272 RVA: 0x0025B396 File Offset: 0x00259596
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(false));
			}
		}

		// Token: 0x17001C38 RID: 7224
		// (get) Token: 0x06006A89 RID: 27273 RVA: 0x0025B3A3 File Offset: 0x002595A3
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NFanOfKnivesVfx.AssetPaths;
			}
		}

		// Token: 0x06006A8A RID: 27274 RVA: 0x0025B3AC File Offset: 0x002595AC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<FanOfKnivesPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
			for (int i = 0; i < base.DynamicVars["Shivs"].IntValue; i++)
			{
				await Shiv.CreateInHand(base.Owner, base.CombatState);
				await Cmd.CustomScaledWait(0.1f, 0.2f, false, default(CancellationToken));
			}
		}

		// Token: 0x06006A8B RID: 27275 RVA: 0x0025B3F0 File Offset: 0x002595F0
		public override async Task OnEnqueuePlayVfx([Nullable(2)] Creature target)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.BackCombatVfxContainer.AddChildSafely(NFanOfKnivesVfx.Create(base.Owner.Creature));
			}
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
		}

		// Token: 0x06006A8C RID: 27276 RVA: 0x0025B433 File Offset: 0x00259633
		protected override void OnUpgrade()
		{
			base.DynamicVars["Shivs"].UpgradeValueBy(1m);
		}

		// Token: 0x04002579 RID: 9593
		private const string _shivsKey = "Shivs";
	}
}
