using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009AD RID: 2477
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KnockoutBlow : CardModel
	{
		// Token: 0x06006CB8 RID: 27832 RVA: 0x0025FB71 File Offset: 0x0025DD71
		public KnockoutBlow()
			: base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D1D RID: 7453
		// (get) Token: 0x06006CB9 RID: 27833 RVA: 0x0025FB7E File Offset: 0x0025DD7E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(30m, ValueProp.Move),
					new StarsVar(5)
				});
			}
		}

		// Token: 0x06006CBA RID: 27834 RVA: 0x0025FBA4 File Offset: 0x0025DDA4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			AttackCommand attackCommand = await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			AttackCommand attackCommand2 = attackCommand;
			if (attackCommand2.Results.Any((DamageResult r) => r.WasTargetKilled))
			{
				await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
			}
		}

		// Token: 0x06006CBB RID: 27835 RVA: 0x0025FBF7 File Offset: 0x0025DDF7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(8m);
		}
	}
}
