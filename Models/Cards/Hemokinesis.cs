using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200098C RID: 2444
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Hemokinesis : CardModel
	{
		// Token: 0x06006C0F RID: 27663 RVA: 0x0025E635 File Offset: 0x0025C835
		public Hemokinesis()
			: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CDA RID: 7386
		// (get) Token: 0x06006C10 RID: 27664 RVA: 0x0025E642 File Offset: 0x0025C842
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HpLossVar(2m),
					new DamageVar(14m, ValueProp.Move)
				});
			}
		}

		// Token: 0x06006C11 RID: 27665 RVA: 0x0025E670 File Offset: 0x0025C870
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_bloody_impact", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006C12 RID: 27666 RVA: 0x0025E6C3 File Offset: 0x0025C8C3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}
	}
}
