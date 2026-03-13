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
	// Token: 0x02000A00 RID: 2560
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PommelStrike : CardModel
	{
		// Token: 0x06006E84 RID: 28292 RVA: 0x0026362F File Offset: 0x0026182F
		public PommelStrike()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DDD RID: 7645
		// (get) Token: 0x06006E85 RID: 28293 RVA: 0x0026363C File Offset: 0x0026183C
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001DDE RID: 7646
		// (get) Token: 0x06006E86 RID: 28294 RVA: 0x0026364B File Offset: 0x0026184B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(9m, ValueProp.Move),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x06006E87 RID: 28295 RVA: 0x00263674 File Offset: 0x00261874
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06006E88 RID: 28296 RVA: 0x002636C7 File Offset: 0x002618C7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
