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
	// Token: 0x020009FE RID: 2558
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PoisonedStab : CardModel
	{
		// Token: 0x06006E79 RID: 28281 RVA: 0x002634C4 File Offset: 0x002616C4
		public PoisonedStab()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DD8 RID: 7640
		// (get) Token: 0x06006E7A RID: 28282 RVA: 0x002634D1 File Offset: 0x002616D1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(6m, ValueProp.Move),
					new PowerVar<PoisonPower>(3m)
				});
			}
		}

		// Token: 0x17001DD9 RID: 7641
		// (get) Token: 0x06006E7B RID: 28283 RVA: 0x002634FB File Offset: 0x002616FB
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x06006E7C RID: 28284 RVA: 0x00263508 File Offset: 0x00261708
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_dramatic_stab", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<PoisonPower>(cardPlay.Target, base.DynamicVars.Poison.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E7D RID: 28285 RVA: 0x0026355B File Offset: 0x0026175B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars.Poison.UpgradeValueBy(1m);
		}
	}
}
