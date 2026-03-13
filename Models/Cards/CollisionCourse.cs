using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008E7 RID: 2279
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CollisionCourse : CardModel
	{
		// Token: 0x06006899 RID: 26777 RVA: 0x00257AAF File Offset: 0x00255CAF
		public CollisionCourse()
			: base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B58 RID: 7000
		// (get) Token: 0x0600689A RID: 26778 RVA: 0x00257ABC File Offset: 0x00255CBC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Debris>(false));
			}
		}

		// Token: 0x17001B59 RID: 7001
		// (get) Token: 0x0600689B RID: 26779 RVA: 0x00257AC9 File Offset: 0x00255CC9
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(9m, ValueProp.Move));
			}
		}

		// Token: 0x0600689C RID: 26780 RVA: 0x00257AE0 File Offset: 0x00255CE0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			await CardPileCmd.AddGeneratedCardToCombat(base.CombatState.CreateCard<Debris>(base.Owner), PileType.Hand, true, CardPilePosition.Bottom);
			await Cmd.Wait(0.5f, false);
		}

		// Token: 0x0600689D RID: 26781 RVA: 0x00257B33 File Offset: 0x00255D33
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
