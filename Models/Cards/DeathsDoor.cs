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
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000909 RID: 2313
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DeathsDoor : CardModel
	{
		// Token: 0x06006941 RID: 26945 RVA: 0x0025904B File Offset: 0x0025724B
		public DeathsDoor()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B9C RID: 7068
		// (get) Token: 0x06006942 RID: 26946 RVA: 0x00259058 File Offset: 0x00257258
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B9D RID: 7069
		// (get) Token: 0x06006943 RID: 26947 RVA: 0x0025905B File Offset: 0x0025725B
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return this.WasDoomAppliedThisTurn;
			}
		}

		// Token: 0x17001B9E RID: 7070
		// (get) Token: 0x06006944 RID: 26948 RVA: 0x00259063 File Offset: 0x00257263
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(6m, ValueProp.Move),
					new RepeatVar(2)
				});
			}
		}

		// Token: 0x17001B9F RID: 7071
		// (get) Token: 0x06006945 RID: 26949 RVA: 0x00259088 File Offset: 0x00257288
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x06006946 RID: 26950 RVA: 0x00259094 File Offset: 0x00257294
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			int blockGains = 1;
			if (this.WasDoomAppliedThisTurn)
			{
				blockGains += base.DynamicVars.Repeat.IntValue;
			}
			for (int i = 0; i < blockGains; i++)
			{
				await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			}
		}

		// Token: 0x06006947 RID: 26951 RVA: 0x002590DF File Offset: 0x002572DF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(1m);
		}

		// Token: 0x17001BA0 RID: 7072
		// (get) Token: 0x06006948 RID: 26952 RVA: 0x002590F6 File Offset: 0x002572F6
		private bool WasDoomAppliedThisTurn
		{
			get
			{
				return CombatManager.Instance.History.Entries.OfType<PowerReceivedEntry>().Any((PowerReceivedEntry e) => e.HappenedThisTurn(base.CombatState) && e.Power is DoomPower && e.Applier == base.Owner.Creature);
			}
		}
	}
}
