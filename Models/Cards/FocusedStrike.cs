using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000959 RID: 2393
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FocusedStrike : CardModel
	{
		// Token: 0x06006AFD RID: 27389 RVA: 0x0025C3BB File Offset: 0x0025A5BB
		public FocusedStrike()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C67 RID: 7271
		// (get) Token: 0x06006AFE RID: 27390 RVA: 0x0025C3C8 File Offset: 0x0025A5C8
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001C68 RID: 7272
		// (get) Token: 0x06006AFF RID: 27391 RVA: 0x0025C3D7 File Offset: 0x0025A5D7
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<FocusPower>());
			}
		}

		// Token: 0x17001C69 RID: 7273
		// (get) Token: 0x06006B00 RID: 27392 RVA: 0x0025C3E3 File Offset: 0x0025A5E3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(9m, ValueProp.Move),
					new PowerVar<FocusPower>(1m)
				});
			}
		}

		// Token: 0x06006B01 RID: 27393 RVA: 0x0025C410 File Offset: 0x0025A610
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<FocusedStrikePower>(base.Owner.Creature, base.DynamicVars["FocusPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006B02 RID: 27394 RVA: 0x0025C463 File Offset: 0x0025A663
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars["FocusPower"].UpgradeValueBy(1m);
		}
	}
}
