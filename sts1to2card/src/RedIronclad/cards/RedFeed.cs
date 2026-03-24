using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedFeed : CardModel
	{
		public RedFeed()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// (get) Token: 0x06006A99 RID: 27289 RVA: 0x0025B64C File Offset: 0x0025984C
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// (get) Token: 0x06006A9A RID: 27290 RVA: 0x0025B64F File Offset: 0x0025984F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[]
				{
					new DamageVar(10m, ValueProp.Move),
					new MaxHpVar(3m)
				};
			}
		}

		// (get) Token: 0x06006A9B RID: 27291 RVA: 0x0025B67A File Offset: 0x0025987A
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new CardKeyword[] { CardKeyword.Exhaust };
			}
		}

		// (get) Token: 0x06006A9C RID: 27292 RVA: 0x0025B682 File Offset: 0x00259882
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.Static(StaticHoverTip.Fatal, Array.Empty<DynamicVar>()) };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			bool shouldTriggerFatal = cardPlay.Target.Powers.All((PowerModel p) => p.ShouldOwnerDeathTriggerFatal());
			AttackCommand attackCommand = await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_bite", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			AttackCommand attackCommand2 = attackCommand;
			if (shouldTriggerFatal)
			{
				if (attackCommand2.Results.Any((DamageResult r) => r.WasTargetKilled))
				{
					await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.IntValue);
				}
			}
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars.MaxHp.UpgradeValueBy(1m);
		}
	}
}
