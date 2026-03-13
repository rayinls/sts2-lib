using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009CF RID: 2511
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MinionStrike : CardModel
	{
		// Token: 0x06006D83 RID: 28035 RVA: 0x002615C7 File Offset: 0x0025F7C7
		public MinionStrike()
			: base(0, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D74 RID: 7540
		// (get) Token: 0x06006D84 RID: 28036 RVA: 0x002615D4 File Offset: 0x0025F7D4
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag>
				{
					CardTag.Strike,
					CardTag.Minion
				};
			}
		}

		// Token: 0x17001D75 RID: 7541
		// (get) Token: 0x06006D85 RID: 28037 RVA: 0x002615EB File Offset: 0x0025F7EB
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001D76 RID: 7542
		// (get) Token: 0x06006D86 RID: 28038 RVA: 0x002615F3 File Offset: 0x0025F7F3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(7m, ValueProp.Move),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x06006D87 RID: 28039 RVA: 0x00261618 File Offset: 0x0025F818
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06006D88 RID: 28040 RVA: 0x0026166B File Offset: 0x0025F86B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
