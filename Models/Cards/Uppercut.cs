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
	// Token: 0x02000AB0 RID: 2736
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Uppercut : CardModel
	{
		// Token: 0x06007233 RID: 29235 RVA: 0x0026A9EA File Offset: 0x00268BEA
		public Uppercut()
			: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F5D RID: 8029
		// (get) Token: 0x06007234 RID: 29236 RVA: 0x0026A9F7 File Offset: 0x00268BF7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(13m, ValueProp.Move),
					new DynamicVar("Power", 1m)
				});
			}
		}

		// Token: 0x17001F5E RID: 8030
		// (get) Token: 0x06007235 RID: 29237 RVA: 0x0026AA26 File Offset: 0x00268C26
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<WeakPower>(),
					HoverTipFactory.FromPower<VulnerablePower>()
				});
			}
		}

		// Token: 0x06007236 RID: 29238 RVA: 0x0026AA44 File Offset: 0x00268C44
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			int amount = base.DynamicVars["Power"].IntValue;
			await PowerCmd.Apply<WeakPower>(cardPlay.Target, amount, base.Owner.Creature, this, false);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, amount, base.Owner.Creature, this, false);
		}

		// Token: 0x06007237 RID: 29239 RVA: 0x0026AA97 File Offset: 0x00268C97
		protected override void OnUpgrade()
		{
			base.DynamicVars["Power"].UpgradeValueBy(1m);
		}

		// Token: 0x040025E3 RID: 9699
		private const string _powerKey = "Power";
	}
}
