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
	// Token: 0x020009A3 RID: 2467
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class IronWave : CardModel
	{
		// Token: 0x06006C88 RID: 27784 RVA: 0x0025F497 File Offset: 0x0025D697
		public IronWave()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D0E RID: 7438
		// (get) Token: 0x06006C89 RID: 27785 RVA: 0x0025F4A4 File Offset: 0x0025D6A4
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D0F RID: 7439
		// (get) Token: 0x06006C8A RID: 27786 RVA: 0x0025F4A7 File Offset: 0x0025D6A7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(5m, ValueProp.Move),
					new BlockVar(5m, ValueProp.Move)
				});
			}
		}

		// Token: 0x06006C8B RID: 27787 RVA: 0x0025F4D4 File Offset: 0x0025D6D4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_flying_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006C8C RID: 27788 RVA: 0x0025F527 File Offset: 0x0025D727
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars.Block.UpgradeValueBy(2m);
		}
	}
}
