using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A9C RID: 2716
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TimesUp : CardModel
	{
		// Token: 0x060071CC RID: 29132 RVA: 0x00269E5F File Offset: 0x0026805F
		public TimesUp()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F32 RID: 7986
		// (get) Token: 0x060071CD RID: 29133 RVA: 0x00269E6C File Offset: 0x0026806C
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001F33 RID: 7987
		// (get) Token: 0x060071CE RID: 29134 RVA: 0x00269E74 File Offset: 0x00268074
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x17001F34 RID: 7988
		// (get) Token: 0x060071CF RID: 29135 RVA: 0x00269E80 File Offset: 0x00268080
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(0m);
				array[1] = new ExtraDamageVar(1m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel _, [Nullable(2)] Creature target) => (target != null) ? target.GetPowerAmount<DoomPower>() : 0);
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x060071D0 RID: 29136 RVA: 0x00269EE0 File Offset: 0x002680E0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x060071D1 RID: 29137 RVA: 0x00269F33 File Offset: 0x00268133
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Retain);
		}
	}
}
