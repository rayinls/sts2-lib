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
	// Token: 0x02000904 RID: 2308
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Dash : CardModel
	{
		// Token: 0x0600692A RID: 26922 RVA: 0x00258D30 File Offset: 0x00256F30
		public Dash()
			: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B92 RID: 7058
		// (get) Token: 0x0600692B RID: 26923 RVA: 0x00258D3D File Offset: 0x00256F3D
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B93 RID: 7059
		// (get) Token: 0x0600692C RID: 26924 RVA: 0x00258D40 File Offset: 0x00256F40
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(10m, ValueProp.Move),
					new BlockVar(10m, ValueProp.Move)
				});
			}
		}

		// Token: 0x0600692D RID: 26925 RVA: 0x00258D70 File Offset: 0x00256F70
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_flying_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x00258DC3 File Offset: 0x00256FC3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}
