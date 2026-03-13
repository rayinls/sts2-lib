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
	// Token: 0x020008B4 RID: 2228
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Bludgeon : CardModel
	{
		// Token: 0x0600679A RID: 26522 RVA: 0x00255B0B File Offset: 0x00253D0B
		public Bludgeon()
			: base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001AF2 RID: 6898
		// (get) Token: 0x0600679B RID: 26523 RVA: 0x00255B18 File Offset: 0x00253D18
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(32m, ValueProp.Move));
			}
		}

		// Token: 0x0600679C RID: 26524 RVA: 0x00255B2C File Offset: 0x00253D2C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x0600679D RID: 26525 RVA: 0x00255B7F File Offset: 0x00253D7F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(10m);
		}
	}
}
