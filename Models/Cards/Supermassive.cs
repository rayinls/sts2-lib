using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A80 RID: 2688
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Supermassive : CardModel
	{
		// Token: 0x06007134 RID: 28980 RVA: 0x00268BB7 File Offset: 0x00266DB7
		public Supermassive()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EF7 RID: 7927
		// (get) Token: 0x06007135 RID: 28981 RVA: 0x00268BC4 File Offset: 0x00266DC4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(5m);
				array[1] = new ExtraDamageVar(3m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature _) => CombatManager.Instance.History.Entries.OfType<CardGeneratedEntry>().Count((CardGeneratedEntry c) => c.Actor.Player == card.Owner && c.GeneratedByPlayer));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06007136 RID: 28982 RVA: 0x00268C28 File Offset: 0x00266E28
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, "heavy_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06007137 RID: 28983 RVA: 0x00268C7B File Offset: 0x00266E7B
		protected override void OnUpgrade()
		{
			base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
		}
	}
}
