using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A8A RID: 2698
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TagTeam : CardModel
	{
		// Token: 0x06007168 RID: 29032 RVA: 0x00269286 File Offset: 0x00267486
		public TagTeam()
			: base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F0C RID: 7948
		// (get) Token: 0x06007169 RID: 29033 RVA: 0x00269293 File Offset: 0x00267493
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001F0D RID: 7949
		// (get) Token: 0x0600716A RID: 29034 RVA: 0x00269296 File Offset: 0x00267496
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(11m, ValueProp.Move));
			}
		}

		// Token: 0x0600716B RID: 29035 RVA: 0x002692AC File Offset: 0x002674AC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<TagTeamPower>(cardPlay.Target, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x0600716C RID: 29036 RVA: 0x002692FF File Offset: 0x002674FF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}
