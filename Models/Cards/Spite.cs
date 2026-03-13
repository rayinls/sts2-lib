using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A64 RID: 2660
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Spite : CardModel
	{
		// Token: 0x060070A0 RID: 28832 RVA: 0x00267813 File Offset: 0x00265A13
		public Spite()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EC0 RID: 7872
		// (get) Token: 0x060070A1 RID: 28833 RVA: 0x00267820 File Offset: 0x00265A20
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return this.TookDamageThisTurn;
			}
		}

		// Token: 0x17001EC1 RID: 7873
		// (get) Token: 0x060070A2 RID: 28834 RVA: 0x00267828 File Offset: 0x00265A28
		private bool TookDamageThisTurn
		{
			get
			{
				return CombatManager.Instance.History.Entries.OfType<DamageReceivedEntry>().Any((DamageReceivedEntry e) => e.HappenedThisTurn(base.CombatState) && e.Receiver == base.Owner.Creature && e.Result.UnblockedDamage > 0 && e.CurrentSide == CombatSide.Player);
			}
		}

		// Token: 0x17001EC2 RID: 7874
		// (get) Token: 0x060070A3 RID: 28835 RVA: 0x0026784F File Offset: 0x00265A4F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(6m, ValueProp.Move),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x060070A4 RID: 28836 RVA: 0x00267874 File Offset: 0x00265A74
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			if (this.TookDamageThisTurn)
			{
				await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.IntValue, base.Owner, false);
			}
		}

		// Token: 0x060070A5 RID: 28837 RVA: 0x002678C7 File Offset: 0x00265AC7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
