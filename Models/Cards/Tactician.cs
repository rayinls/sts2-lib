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
	// Token: 0x02000A89 RID: 2697
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Tactician : CardModel
	{
		// Token: 0x06007162 RID: 29026 RVA: 0x002691FB File Offset: 0x002673FB
		public Tactician()
			: base(3, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001F09 RID: 7945
		// (get) Token: 0x06007163 RID: 29027 RVA: 0x00269208 File Offset: 0x00267408
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17001F0A RID: 7946
		// (get) Token: 0x06007164 RID: 29028 RVA: 0x00269215 File Offset: 0x00267415
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Sly);
			}
		}

		// Token: 0x17001F0B RID: 7947
		// (get) Token: 0x06007165 RID: 29029 RVA: 0x0026921D File Offset: 0x0026741D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x06007166 RID: 29030 RVA: 0x0026922C File Offset: 0x0026742C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
		}

		// Token: 0x06007167 RID: 29031 RVA: 0x0026926F File Offset: 0x0026746F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}
