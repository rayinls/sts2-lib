using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AB2 RID: 2738
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Veilpiercer : CardModel
	{
		// Token: 0x0600723C RID: 29244 RVA: 0x0026AB3F File Offset: 0x00268D3F
		public Veilpiercer()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F60 RID: 8032
		// (get) Token: 0x0600723D RID: 29245 RVA: 0x0026AB4C File Offset: 0x00268D4C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(10m, ValueProp.Move));
			}
		}

		// Token: 0x17001F61 RID: 8033
		// (get) Token: 0x0600723E RID: 29246 RVA: 0x0026AB60 File Offset: 0x00268D60
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Ethereal));
			}
		}

		// Token: 0x0600723F RID: 29247 RVA: 0x0026AB70 File Offset: 0x00268D70
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<VeilpiercerPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06007240 RID: 29248 RVA: 0x0026ABC3 File Offset: 0x00268DC3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
