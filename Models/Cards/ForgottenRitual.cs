using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200095F RID: 2399
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ForgottenRitual : CardModel
	{
		// Token: 0x06006B1C RID: 27420 RVA: 0x0025C766 File Offset: 0x0025A966
		public ForgottenRitual()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x06006B1D RID: 27421 RVA: 0x0025C773 File Offset: 0x0025A973
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return this.WasCardExhaustedThisTurn;
			}
		}

		// Token: 0x17001C76 RID: 7286
		// (get) Token: 0x06006B1E RID: 27422 RVA: 0x0025C77B File Offset: 0x0025A97B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(3));
			}
		}

		// Token: 0x17001C77 RID: 7287
		// (get) Token: 0x06006B1F RID: 27423 RVA: 0x0025C788 File Offset: 0x0025A988
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					base.EnergyHoverTip,
					HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
				});
			}
		}

		// Token: 0x17001C78 RID: 7288
		// (get) Token: 0x06006B20 RID: 27424 RVA: 0x0025C7A7 File Offset: 0x0025A9A7
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NGroundFireVfx.AssetPaths;
			}
		}

		// Token: 0x06006B21 RID: 27425 RVA: 0x0025C7B0 File Offset: 0x0025A9B0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NGroundFireVfx.Create(base.Owner.Creature, VfxColor.Purple));
			}
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			if (this.WasCardExhaustedThisTurn)
			{
				await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
			}
		}

		// Token: 0x06006B22 RID: 27426 RVA: 0x0025C7F3 File Offset: 0x0025A9F3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}

		// Token: 0x17001C79 RID: 7289
		// (get) Token: 0x06006B23 RID: 27427 RVA: 0x0025C80A File Offset: 0x0025AA0A
		private bool WasCardExhaustedThisTurn
		{
			get
			{
				return CombatManager.Instance.History.Entries.OfType<CardExhaustedEntry>().Any((CardExhaustedEntry e) => e.HappenedThisTurn(base.CombatState) && e.Card.Owner == base.Owner);
			}
		}
	}
}
