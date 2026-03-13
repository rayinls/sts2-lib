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
	// Token: 0x020008A3 RID: 2211
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Bash : CardModel
	{
		// Token: 0x06006743 RID: 26435 RVA: 0x0025505D File Offset: 0x0025325D
		public Bash()
			: base(2, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001ACD RID: 6861
		// (get) Token: 0x06006744 RID: 26436 RVA: 0x0025506A File Offset: 0x0025326A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x17001ACE RID: 6862
		// (get) Token: 0x06006745 RID: 26437 RVA: 0x00255076 File Offset: 0x00253276
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(8m, ValueProp.Move),
					new PowerVar<VulnerablePower>(2m)
				});
			}
		}

		// Token: 0x06006746 RID: 26438 RVA: 0x002550A0 File Offset: 0x002532A0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006747 RID: 26439 RVA: 0x002550F3 File Offset: 0x002532F3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars.Vulnerable.UpgradeValueBy(1m);
		}
	}
}
