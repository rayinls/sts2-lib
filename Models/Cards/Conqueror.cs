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
	// Token: 0x020008ED RID: 2285
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Conqueror : CardModel
	{
		// Token: 0x060068B9 RID: 26809 RVA: 0x00257F67 File Offset: 0x00256167
		public Conqueror()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B66 RID: 7014
		// (get) Token: 0x060068BA RID: 26810 RVA: 0x00257F74 File Offset: 0x00256174
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new ForgeVar(3));
			}
		}

		// Token: 0x17001B67 RID: 7015
		// (get) Token: 0x060068BB RID: 26811 RVA: 0x00257F81 File Offset: 0x00256181
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x00257F88 File Offset: 0x00256188
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await ForgeCmd.Forge(base.DynamicVars.Forge.IntValue, base.Owner, this);
			await PowerCmd.Apply<ConquerorPower>(cardPlay.Target, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x00257FD3 File Offset: 0x002561D3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Forge.UpgradeValueBy(2m);
		}
	}
}
