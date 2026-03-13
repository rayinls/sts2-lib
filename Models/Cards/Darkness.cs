using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000902 RID: 2306
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Darkness : CardModel
	{
		// Token: 0x06006921 RID: 26913 RVA: 0x00258C11 File Offset: 0x00256E11
		public Darkness()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B8E RID: 7054
		// (get) Token: 0x06006922 RID: 26914 RVA: 0x00258C1E File Offset: 0x00256E1E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<DarkOrb>()
				});
			}
		}

		// Token: 0x06006923 RID: 26915 RVA: 0x00258C44 File Offset: 0x00256E44
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OrbCmd.Channel<DarkOrb>(choiceContext, base.Owner);
			IEnumerable<OrbModel> enumerable = base.Owner.PlayerCombatState.OrbQueue.Orbs.Where((OrbModel orb) => orb is DarkOrb);
			int triggerCount = (base.IsUpgraded ? 2 : 1);
			foreach (OrbModel darknessOrb in enumerable)
			{
				for (int i = 0; i < triggerCount; i++)
				{
					await OrbCmd.Passive(choiceContext, darknessOrb, null);
				}
				darknessOrb = null;
			}
			IEnumerator<OrbModel> enumerator = null;
		}
	}
}
