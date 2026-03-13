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
	// Token: 0x020008D9 RID: 2265
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CelestialMight : CardModel
	{
		// Token: 0x0600684F RID: 26703 RVA: 0x002571C2 File Offset: 0x002553C2
		public CelestialMight()
			: base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B38 RID: 6968
		// (get) Token: 0x06006850 RID: 26704 RVA: 0x002571CF File Offset: 0x002553CF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(6m, ValueProp.Move),
					new RepeatVar(3)
				});
			}
		}

		// Token: 0x06006851 RID: 26705 RVA: 0x002571F4 File Offset: 0x002553F4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.DynamicVars.Repeat.IntValue).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_starry_impact", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x06006852 RID: 26706 RVA: 0x00257247 File Offset: 0x00255447
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}
