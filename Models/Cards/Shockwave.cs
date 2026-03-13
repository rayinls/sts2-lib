using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A4B RID: 2635
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Shockwave : CardModel
	{
		// Token: 0x06007010 RID: 28688 RVA: 0x00266717 File Offset: 0x00264917
		public Shockwave()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001E83 RID: 7811
		// (get) Token: 0x06007011 RID: 28689 RVA: 0x00266724 File Offset: 0x00264924
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Power", 3m));
			}
		}

		// Token: 0x17001E84 RID: 7812
		// (get) Token: 0x06007012 RID: 28690 RVA: 0x0026673B File Offset: 0x0026493B
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001E85 RID: 7813
		// (get) Token: 0x06007013 RID: 28691 RVA: 0x00266743 File Offset: 0x00264943
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<WeakPower>(),
					HoverTipFactory.FromPower<VulnerablePower>()
				});
			}
		}

		// Token: 0x06007014 RID: 28692 RVA: 0x00266760 File Offset: 0x00264960
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			VfxCmd.PlayOnCreatureCenter(base.Owner.Creature, "vfx/vfx_flying_slash");
			int amount = base.DynamicVars["Power"].IntValue;
			foreach (Creature enemy in base.CombatState.HittableEnemies)
			{
				await PowerCmd.Apply<WeakPower>(enemy, amount, base.Owner.Creature, this, false);
				await PowerCmd.Apply<VulnerablePower>(enemy, amount, base.Owner.Creature, this, false);
				enemy = null;
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06007015 RID: 28693 RVA: 0x002667A3 File Offset: 0x002649A3
		protected override void OnUpgrade()
		{
			base.DynamicVars["Power"].UpgradeValueBy(2m);
		}

		// Token: 0x040025CD RID: 9677
		private const string _powerKey = "Power";
	}
}
