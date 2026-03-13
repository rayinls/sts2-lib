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
	// Token: 0x02000946 RID: 2374
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Fear : CardModel
	{
		// Token: 0x06006A92 RID: 27282 RVA: 0x0025B574 File Offset: 0x00259774
		public Fear()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x06006A93 RID: 27283 RVA: 0x0025B581 File Offset: 0x00259781
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(7m, ValueProp.Move),
					new PowerVar<VulnerablePower>(1m)
				});
			}
		}

		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x06006A94 RID: 27284 RVA: 0x0025B5AA File Offset: 0x002597AA
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Ethereal);
			}
		}

		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x06006A95 RID: 27285 RVA: 0x0025B5B2 File Offset: 0x002597B2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x06006A96 RID: 27286 RVA: 0x0025B5C0 File Offset: 0x002597C0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006A97 RID: 27287 RVA: 0x0025B613 File Offset: 0x00259813
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars.Vulnerable.UpgradeValueBy(1m);
		}
	}
}
