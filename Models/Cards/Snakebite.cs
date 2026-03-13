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
	// Token: 0x02000A57 RID: 2647
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Snakebite : CardModel
	{
		// Token: 0x0600704B RID: 28747 RVA: 0x00266DD8 File Offset: 0x00264FD8
		public Snakebite()
			: base(2, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E9C RID: 7836
		// (get) Token: 0x0600704C RID: 28748 RVA: 0x00266DE5 File Offset: 0x00264FE5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PoisonPower>(7m));
			}
		}

		// Token: 0x17001E9D RID: 7837
		// (get) Token: 0x0600704D RID: 28749 RVA: 0x00266DF7 File Offset: 0x00264FF7
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x17001E9E RID: 7838
		// (get) Token: 0x0600704E RID: 28750 RVA: 0x00266E03 File Offset: 0x00265003
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Retain);
			}
		}

		// Token: 0x0600704F RID: 28751 RVA: 0x00266E0C File Offset: 0x0026500C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			VfxCmd.PlayOnCreatureCenter(cardPlay.Target, "vfx/vfx_bite");
			await PowerCmd.Apply<PoisonPower>(cardPlay.Target, base.DynamicVars.Poison.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06007050 RID: 28752 RVA: 0x00266E57 File Offset: 0x00265057
		protected override void OnUpgrade()
		{
			base.DynamicVars.Poison.UpgradeValueBy(3m);
		}
	}
}
