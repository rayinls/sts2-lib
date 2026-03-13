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
	// Token: 0x020008C0 RID: 2240
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Break : CardModel
	{
		// Token: 0x060067DC RID: 26588 RVA: 0x002563C2 File Offset: 0x002545C2
		public Break()
			: base(2, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B0D RID: 6925
		// (get) Token: 0x060067DD RID: 26589 RVA: 0x002563CF File Offset: 0x002545CF
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x17001B0E RID: 6926
		// (get) Token: 0x060067DE RID: 26590 RVA: 0x002563DB File Offset: 0x002545DB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(20m, ValueProp.Move),
					new PowerVar<VulnerablePower>(5m)
				});
			}
		}

		// Token: 0x060067DF RID: 26591 RVA: 0x00256408 File Offset: 0x00254608
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060067E0 RID: 26592 RVA: 0x0025645B File Offset: 0x0025465B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
			base.DynamicVars.Vulnerable.UpgradeValueBy(2m);
		}
	}
}
