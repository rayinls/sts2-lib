using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A2A RID: 2602
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RightHandHand : CardModel
	{
		// Token: 0x06006F5E RID: 28510 RVA: 0x002651C2 File Offset: 0x002633C2
		public RightHandHand()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E38 RID: 7736
		// (get) Token: 0x06006F5F RID: 28511 RVA: 0x002651CF File Offset: 0x002633CF
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001E39 RID: 7737
		// (get) Token: 0x06006F60 RID: 28512 RVA: 0x002651DE File Offset: 0x002633DE
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001E3A RID: 7738
		// (get) Token: 0x06006F61 RID: 28513 RVA: 0x002651EB File Offset: 0x002633EB
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x17001E3B RID: 7739
		// (get) Token: 0x06006F62 RID: 28514 RVA: 0x002651F8 File Offset: 0x002633F8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new OstyDamageVar(4m, ValueProp.Move),
					new EnergyVar(2)
				});
			}
		}

		// Token: 0x06006F63 RID: 28515 RVA: 0x00265220 File Offset: 0x00263420
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			if (!Osty.CheckMissingWithAnim(base.Owner))
			{
				await DamageCmd.Attack(base.DynamicVars.OstyDamage.BaseValue).FromOsty(base.Owner.Osty, this).Targeting(cardPlay.Target)
					.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
					.Execute(choiceContext);
			}
		}

		// Token: 0x06006F64 RID: 28516 RVA: 0x00265274 File Offset: 0x00263474
		public override async Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (cardPlay.Resources.EnergyValue >= base.DynamicVars.Energy.IntValue)
				{
					CardPile pile = base.Pile;
					if (pile != null && pile.Type == PileType.Discard)
					{
						await CardPileCmd.Add(this, PileType.Hand, CardPilePosition.Bottom, null, false);
					}
				}
			}
		}

		// Token: 0x06006F65 RID: 28517 RVA: 0x002652BF File Offset: 0x002634BF
		protected override void OnUpgrade()
		{
			base.DynamicVars.OstyDamage.UpgradeValueBy(2m);
		}
	}
}
