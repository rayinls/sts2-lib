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
	// Token: 0x02000A6A RID: 2666
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Squash : CardModel
	{
		// Token: 0x060070C8 RID: 28872 RVA: 0x00267D6D File Offset: 0x00265F6D
		public Squash()
			: base(1, CardType.Attack, CardRarity.Event, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001ED0 RID: 7888
		// (get) Token: 0x060070C9 RID: 28873 RVA: 0x00267D7A File Offset: 0x00265F7A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x17001ED1 RID: 7889
		// (get) Token: 0x060070CA RID: 28874 RVA: 0x00267D86 File Offset: 0x00265F86
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(10m, ValueProp.Move),
					new PowerVar<VulnerablePower>(2m)
				});
			}
		}

		// Token: 0x060070CB RID: 28875 RVA: 0x00267DB4 File Offset: 0x00265FB4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060070CC RID: 28876 RVA: 0x00267E07 File Offset: 0x00266007
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars.Vulnerable.UpgradeValueBy(1m);
		}
	}
}
