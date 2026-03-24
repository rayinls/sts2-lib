using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedTwinStrike : CardModel
	{
		public RedTwinStrike()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// (get) Token: 0x06007200 RID: 29184 RVA: 0x0026A40B File Offset: 0x0026860B
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// (get) Token: 0x06007201 RID: 29185 RVA: 0x0026A41A File Offset: 0x0026861A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new DamageVar(5m, ValueProp.Move) };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(2).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}
