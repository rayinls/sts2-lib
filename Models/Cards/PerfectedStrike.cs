using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009F7 RID: 2551
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PerfectedStrike : CardModel
	{
		// Token: 0x06006E53 RID: 28243 RVA: 0x00262F52 File Offset: 0x00261152
		public PerfectedStrike()
			: base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DCB RID: 7627
		// (get) Token: 0x06006E54 RID: 28244 RVA: 0x00262F5F File Offset: 0x0026115F
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001DCC RID: 7628
		// (get) Token: 0x06006E55 RID: 28245 RVA: 0x00262F70 File Offset: 0x00261170
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(6m);
				array[1] = new ExtraDamageVar(2m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, [Nullable(2)] Creature _) => card.Owner.PlayerCombatState.AllCards.Count((CardModel c) => c.Tags.Contains(CardTag.Strike)));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006E56 RID: 28246 RVA: 0x00262FD4 File Offset: 0x002611D4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx(null, null, "heavy_attack.mp3")
				.WithHitVfxNode((Creature t) => NBigSlashVfx.Create(t))
				.WithHitVfxNode((Creature t) => NBigSlashImpactVfx.Create(t))
				.Execute(choiceContext);
		}

		// Token: 0x06006E57 RID: 28247 RVA: 0x00263027 File Offset: 0x00261227
		protected override void OnUpgrade()
		{
			base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
		}
	}
}
