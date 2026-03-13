using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200098B RID: 2443
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Hellraiser : CardModel
	{
		// Token: 0x06006C0A RID: 27658 RVA: 0x0025E58C File Offset: 0x0025C78C
		public Hellraiser()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001CD9 RID: 7385
		// (get) Token: 0x06006C0B RID: 27659 RVA: 0x0025E599 File Offset: 0x0025C799
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NHellraiserVfx.AssetPaths;
			}
		}

		// Token: 0x06006C0C RID: 27660 RVA: 0x0025E5A0 File Offset: 0x0025C7A0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<HellraiserPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006C0D RID: 27661 RVA: 0x0025E5E4 File Offset: 0x0025C7E4
		public override async Task OnEnqueuePlayVfx([Nullable(2)] Creature target)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NHellraiserVfx.Create(base.Owner.Creature));
			}
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
		}

		// Token: 0x06006C0E RID: 27662 RVA: 0x0025E627 File Offset: 0x0025C827
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
