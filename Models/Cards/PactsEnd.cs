using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009ED RID: 2541
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PactsEnd : CardModel
	{
		// Token: 0x06006E20 RID: 28192 RVA: 0x00262916 File Offset: 0x00260B16
		public PactsEnd()
			: base(0, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001DB7 RID: 7607
		// (get) Token: 0x06006E21 RID: 28193 RVA: 0x00262923 File Offset: 0x00260B23
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(17m, ValueProp.Move),
					new CardsVar(3)
				});
			}
		}

		// Token: 0x17001DB8 RID: 7608
		// (get) Token: 0x06006E22 RID: 28194 RVA: 0x00262949 File Offset: 0x00260B49
		protected override bool IsPlayable
		{
			get
			{
				return CardPile.GetCards(base.Owner, new PileType[] { PileType.Exhaust }).Count<CardModel>() >= base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x06006E23 RID: 28195 RVA: 0x0026297C File Offset: 0x00260B7C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006E24 RID: 28196 RVA: 0x002629C7 File Offset: 0x00260BC7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(6m);
		}
	}
}
