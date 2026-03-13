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
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008B1 RID: 2225
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BlightStrike : CardModel
	{
		// Token: 0x06006789 RID: 26505 RVA: 0x00255906 File Offset: 0x00253B06
		public BlightStrike()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001AEA RID: 6890
		// (get) Token: 0x0600678A RID: 26506 RVA: 0x00255913 File Offset: 0x00253B13
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001AEB RID: 6891
		// (get) Token: 0x0600678B RID: 26507 RVA: 0x00255922 File Offset: 0x00253B22
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(8m, ValueProp.Move));
			}
		}

		// Token: 0x17001AEC RID: 6892
		// (get) Token: 0x0600678C RID: 26508 RVA: 0x00255935 File Offset: 0x00253B35
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x00255944 File Offset: 0x00253B44
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			AttackCommand attackCommand = await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			AttackCommand attackCommand2 = attackCommand;
			await PowerCmd.Apply<DoomPower>(cardPlay.Target, attackCommand2.Results.Sum((DamageResult r) => r.TotalDamage), base.Owner.Creature, this, false);
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x00255997 File Offset: 0x00253B97
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}
