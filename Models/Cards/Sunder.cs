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
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A7E RID: 2686
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Sunder : CardModel
	{
		// Token: 0x06007129 RID: 28969 RVA: 0x00268A7F File Offset: 0x00266C7F
		public Sunder()
			: base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EF2 RID: 7922
		// (get) Token: 0x0600712A RID: 28970 RVA: 0x00268A8C File Offset: 0x00266C8C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(24m, ValueProp.Move),
					new EnergyVar(3)
				});
			}
		}

		// Token: 0x17001EF3 RID: 7923
		// (get) Token: 0x0600712B RID: 28971 RVA: 0x00268AB2 File Offset: 0x00266CB2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x0600712C RID: 28972 RVA: 0x00268AC0 File Offset: 0x00266CC0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			AttackCommand attackCommand = await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, "slash_attack.mp3")
				.Execute(choiceContext);
			AttackCommand attackCommand2 = attackCommand;
			if (attackCommand2.Results.Any((DamageResult r) => r.WasTargetKilled))
			{
				await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
			}
		}

		// Token: 0x0600712D RID: 28973 RVA: 0x00268B13 File Offset: 0x00266D13
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(8m);
		}
	}
}
