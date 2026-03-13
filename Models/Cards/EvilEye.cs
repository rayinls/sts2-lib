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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200093E RID: 2366
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EvilEye : CardModel
	{
		// Token: 0x06006A64 RID: 27236 RVA: 0x0025AF08 File Offset: 0x00259108
		public EvilEye()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C27 RID: 7207
		// (get) Token: 0x06006A65 RID: 27237 RVA: 0x0025AF15 File Offset: 0x00259115
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C28 RID: 7208
		// (get) Token: 0x06006A66 RID: 27238 RVA: 0x0025AF18 File Offset: 0x00259118
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return this.WasCardExhaustedThisTurn;
			}
		}

		// Token: 0x17001C29 RID: 7209
		// (get) Token: 0x06006A67 RID: 27239 RVA: 0x0025AF20 File Offset: 0x00259120
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(8m, ValueProp.Move));
			}
		}

		// Token: 0x17001C2A RID: 7210
		// (get) Token: 0x06006A68 RID: 27240 RVA: 0x0025AF33 File Offset: 0x00259133
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x06006A69 RID: 27241 RVA: 0x0025AF40 File Offset: 0x00259140
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			VfxCmd.PlayOnCreatureCenter(base.Owner.Creature, "vfx/vfx_gaze");
			int blockGains = (this.WasCardExhaustedThisTurn ? 2 : 1);
			for (int i = 0; i < blockGains; i++)
			{
				await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			}
		}

		// Token: 0x06006A6A RID: 27242 RVA: 0x0025AF8B File Offset: 0x0025918B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}

		// Token: 0x17001C2B RID: 7211
		// (get) Token: 0x06006A6B RID: 27243 RVA: 0x0025AFA3 File Offset: 0x002591A3
		private bool WasCardExhaustedThisTurn
		{
			get
			{
				return CombatManager.Instance.History.Entries.OfType<CardExhaustedEntry>().Any((CardExhaustedEntry e) => e.HappenedThisTurn(base.CombatState) && e.Card.Owner == base.Owner);
			}
		}
	}
}
