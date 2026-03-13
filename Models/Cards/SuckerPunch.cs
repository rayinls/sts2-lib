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
	// Token: 0x02000A7C RID: 2684
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SuckerPunch : CardModel
	{
		// Token: 0x0600711F RID: 28959 RVA: 0x0026892D File Offset: 0x00266B2D
		public SuckerPunch()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EEE RID: 7918
		// (get) Token: 0x06007120 RID: 28960 RVA: 0x0026893A File Offset: 0x00266B3A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x17001EEF RID: 7919
		// (get) Token: 0x06007121 RID: 28961 RVA: 0x00268946 File Offset: 0x00266B46
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(8m, ValueProp.Move),
					new PowerVar<WeakPower>(1m)
				});
			}
		}

		// Token: 0x06007122 RID: 28962 RVA: 0x00268970 File Offset: 0x00266B70
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			await PowerCmd.Apply<WeakPower>(cardPlay.Target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06007123 RID: 28963 RVA: 0x002689C3 File Offset: 0x00266BC3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars.Weak.UpgradeValueBy(1m);
		}
	}
}
