using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008CF RID: 2255
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ByrdSwoop : CardModel
	{
		// Token: 0x06006825 RID: 26661 RVA: 0x00256CD3 File Offset: 0x00254ED3
		public ByrdSwoop()
			: base(0, CardType.Attack, CardRarity.Event, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B2B RID: 6955
		// (get) Token: 0x06006826 RID: 26662 RVA: 0x00256CE0 File Offset: 0x00254EE0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(14m, ValueProp.Move));
			}
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x00256CF4 File Offset: 0x00254EF4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithAttackerAnim("Attack", base.Owner.Character.AttackAnimDelay, base.Owner.PlayerCombatState.GetPet<Byrdpip>())
				.WithHitFx("vfx/vfx_attack_slash", "event:/sfx/byrdpip/byrdpip_attack", null)
				.Execute(choiceContext);
		}

		// Token: 0x06006828 RID: 26664 RVA: 0x00256D47 File Offset: 0x00254F47
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}

		// Token: 0x04002561 RID: 9569
		public const string attackSfx = "event:/sfx/byrdpip/byrdpip_attack";
	}
}
