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
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedPerfectedStrike : CardModel
	{
		public RedPerfectedStrike()
			: base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// (get) Token: 0x06006E54 RID: 28244 RVA: 0x00262F5F File Offset: 0x0026115F
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// (get) Token: 0x06006E55 RID: 28245 RVA: 0x00262F70 File Offset: 0x00261170
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(6m);
				array[1] = new ExtraDamageVar(2m);
				return array;
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx(null, null, "heavy_attack.mp3")
				.WithHitVfxNode((Creature t) => NBigSlashVfx.Create(t))
				.WithHitVfxNode((Creature t) => NBigSlashImpactVfx.Create(t))
				.Execute(choiceContext);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
		}
	}
}
