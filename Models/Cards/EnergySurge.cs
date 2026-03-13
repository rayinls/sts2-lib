using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000933 RID: 2355
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EnergySurge : CardModel
	{
		// Token: 0x06006A29 RID: 27177 RVA: 0x0025A8F3 File Offset: 0x00258AF3
		public EnergySurge()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllAllies, true)
		{
		}

		// Token: 0x17001C0B RID: 7179
		// (get) Token: 0x06006A2A RID: 27178 RVA: 0x0025A900 File Offset: 0x00258B00
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001C0C RID: 7180
		// (get) Token: 0x06006A2B RID: 27179 RVA: 0x0025A903 File Offset: 0x00258B03
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x17001C0D RID: 7181
		// (get) Token: 0x06006A2C RID: 27180 RVA: 0x0025A910 File Offset: 0x00258B10
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001C0E RID: 7182
		// (get) Token: 0x06006A2D RID: 27181 RVA: 0x0025A918 File Offset: 0x00258B18
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(2));
			}
		}

		// Token: 0x06006A2E RID: 27182 RVA: 0x0025A928 File Offset: 0x00258B28
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<Creature> enumerable = from c in base.CombatState.GetTeammatesOf(base.Owner.Creature)
				where c != null && c.IsAlive && c.IsPlayer
				select c;
			foreach (Creature creature in enumerable)
			{
				await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, creature.Player);
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06006A2F RID: 27183 RVA: 0x0025A96B File Offset: 0x00258B6B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}
