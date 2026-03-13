using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A5D RID: 2653
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SoulStorm : CardModel
	{
		// Token: 0x0600706F RID: 28783 RVA: 0x002671A2 File Offset: 0x002653A2
		public SoulStorm()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EAE RID: 7854
		// (get) Token: 0x06007070 RID: 28784 RVA: 0x002671AF File Offset: 0x002653AF
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Soul>(false));
			}
		}

		// Token: 0x17001EAF RID: 7855
		// (get) Token: 0x06007071 RID: 28785 RVA: 0x002671BC File Offset: 0x002653BC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(9m);
				array[1] = new ExtraDamageVar(2m);
				array[2] = new CalculatedDamageVar(ValueProp.Move).WithMultiplier(delegate(CardModel card, [Nullable(2)] Creature _)
				{
					PlayerCombatState playerCombatState = card.Owner.PlayerCombatState;
					int num;
					if (playerCombatState == null)
					{
						num = 0;
					}
					else
					{
						num = playerCombatState.ExhaustPile.Cards.Count((CardModel c) => c is Soul);
					}
					return num;
				});
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06007072 RID: 28786 RVA: 0x00267220 File Offset: 0x00265420
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06007073 RID: 28787 RVA: 0x00267273 File Offset: 0x00265473
		protected override void OnUpgrade()
		{
			base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
		}
	}
}
