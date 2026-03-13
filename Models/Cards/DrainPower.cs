using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000928 RID: 2344
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DrainPower : CardModel
	{
		// Token: 0x060069F1 RID: 27121 RVA: 0x0025A26F File Offset: 0x0025846F
		public DrainPower()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001BF4 RID: 7156
		// (get) Token: 0x060069F2 RID: 27122 RVA: 0x0025A27C File Offset: 0x0025847C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(10m, ValueProp.Move),
					new CardsVar(2)
				});
			}
		}

		// Token: 0x060069F3 RID: 27123 RVA: 0x0025A2A4 File Offset: 0x002584A4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			IEnumerable<CardModel> enumerable = PileType.Discard.GetPile(base.Owner).Cards.Where((CardModel c) => c.IsUpgradable).TakeRandom(base.DynamicVars.Cards.IntValue, base.Owner.RunState.Rng.CombatCardSelection);
			foreach (CardModel cardModel in enumerable)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
				CardCmd.Preview(cardModel, 1.2f, CardPreviewStyle.HorizontalLayout);
			}
		}

		// Token: 0x060069F4 RID: 27124 RVA: 0x0025A2F7 File Offset: 0x002584F7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
