using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000ABC RID: 2748
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Whistle : CardModel
	{
		// Token: 0x0600726F RID: 29295 RVA: 0x0026B0E3 File Offset: 0x002692E3
		public Whistle()
			: base(3, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F77 RID: 8055
		// (get) Token: 0x06007270 RID: 29296 RVA: 0x0026B0F0 File Offset: 0x002692F0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(StunIntent.GetStaticHoverTip());
			}
		}

		// Token: 0x17001F78 RID: 8056
		// (get) Token: 0x06007271 RID: 29297 RVA: 0x0026B101 File Offset: 0x00269301
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(33m, ValueProp.Move));
			}
		}

		// Token: 0x17001F79 RID: 8057
		// (get) Token: 0x06007272 RID: 29298 RVA: 0x0026B115 File Offset: 0x00269315
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06007273 RID: 29299 RVA: 0x0026B120 File Offset: 0x00269320
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await CreatureCmd.Stun(cardPlay.Target, null);
		}

		// Token: 0x06007274 RID: 29300 RVA: 0x0026B173 File Offset: 0x00269373
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(11m);
		}
	}
}
