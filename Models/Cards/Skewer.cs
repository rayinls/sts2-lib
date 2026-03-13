using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A50 RID: 2640
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Skewer : CardModel
	{
		// Token: 0x0600702C RID: 28716 RVA: 0x00266A65 File Offset: 0x00264C65
		public Skewer()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E90 RID: 7824
		// (get) Token: 0x0600702D RID: 28717 RVA: 0x00266A72 File Offset: 0x00264C72
		protected override bool HasEnergyCostX
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E91 RID: 7825
		// (get) Token: 0x0600702E RID: 28718 RVA: 0x00266A75 File Offset: 0x00264C75
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x0600702F RID: 28719 RVA: 0x00266A88 File Offset: 0x00264C88
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.ResolveEnergyXValue()).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitVfxNode((Creature t) => NStabVfx.Create(t, true, VfxColor.Gold))
				.Execute(choiceContext);
		}

		// Token: 0x06007030 RID: 28720 RVA: 0x00266ADB File Offset: 0x00264CDB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
