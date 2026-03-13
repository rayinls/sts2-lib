using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009CD RID: 2509
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MinionDiveBomb : CardModel
	{
		// Token: 0x06006D76 RID: 28022 RVA: 0x00261483 File Offset: 0x0025F683
		public MinionDiveBomb()
			: base(1, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D6D RID: 7533
		// (get) Token: 0x06006D77 RID: 28023 RVA: 0x00261490 File Offset: 0x0025F690
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Minion };
			}
		}

		// Token: 0x17001D6E RID: 7534
		// (get) Token: 0x06006D78 RID: 28024 RVA: 0x0026149F File Offset: 0x0025F69F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(13m, ValueProp.Move));
			}
		}

		// Token: 0x17001D6F RID: 7535
		// (get) Token: 0x06006D79 RID: 28025 RVA: 0x002614B3 File Offset: 0x0025F6B3
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006D7A RID: 28026 RVA: 0x002614BC File Offset: 0x0025F6BC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).WithAttackerAnim("Cast", 1f, null)
				.Targeting(cardPlay.Target)
				.WithAttackerFx(() => NMinionDiveBombVfx.Create(this.Owner.Creature, cardPlay.Target))
				.Execute(choiceContext);
		}

		// Token: 0x06006D7B RID: 28027 RVA: 0x0026150F File Offset: 0x0025F70F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
