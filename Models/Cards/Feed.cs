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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000947 RID: 2375
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Feed : CardModel
	{
		// Token: 0x06006A98 RID: 27288 RVA: 0x0025B63F File Offset: 0x0025983F
		public Feed()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x06006A99 RID: 27289 RVA: 0x0025B64C File Offset: 0x0025984C
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x06006A9A RID: 27290 RVA: 0x0025B64F File Offset: 0x0025984F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(10m, ValueProp.Move),
					new MaxHpVar(3m)
				});
			}
		}

		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x06006A9B RID: 27291 RVA: 0x0025B67A File Offset: 0x0025987A
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001C41 RID: 7233
		// (get) Token: 0x06006A9C RID: 27292 RVA: 0x0025B682 File Offset: 0x00259882
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Fatal, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06006A9D RID: 27293 RVA: 0x0025B694 File Offset: 0x00259894
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

		// Token: 0x06006A9E RID: 27294 RVA: 0x0025B6E7 File Offset: 0x002598E7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars.MaxHp.UpgradeValueBy(1m);
		}
	}
}
