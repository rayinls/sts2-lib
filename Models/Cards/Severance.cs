using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A42 RID: 2626
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Severance : CardModel
	{
		// Token: 0x06006FD9 RID: 28633 RVA: 0x00266061 File Offset: 0x00264261
		public Severance()
			: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E69 RID: 7785
		// (get) Token: 0x06006FDA RID: 28634 RVA: 0x0026606E File Offset: 0x0026426E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(13m, ValueProp.Move));
			}
		}

		// Token: 0x17001E6A RID: 7786
		// (get) Token: 0x06006FDB RID: 28635 RVA: 0x00266082 File Offset: 0x00264282
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Soul>(false));
			}
		}

		// Token: 0x06006FDC RID: 28636 RVA: 0x00266090 File Offset: 0x00264290
		protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			Severance.<OnPlay>d__5 <OnPlay>d__;
			<OnPlay>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OnPlay>d__.<>4__this = this;
			<OnPlay>d__.choiceContext = choiceContext;
			<OnPlay>d__.cardPlay = cardPlay;
			<OnPlay>d__.<>1__state = -1;
			<OnPlay>d__.<>t__builder.Start<Severance.<OnPlay>d__5>(ref <OnPlay>d__);
			return <OnPlay>d__.<>t__builder.Task;
		}

		// Token: 0x06006FDD RID: 28637 RVA: 0x002660E3 File Offset: 0x002642E3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}
	}
}
