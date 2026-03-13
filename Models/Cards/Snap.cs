using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A58 RID: 2648
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Snap : CardModel
	{
		// Token: 0x06007051 RID: 28753 RVA: 0x00266E6F File Offset: 0x0026506F
		public Snap()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E9F RID: 7839
		// (get) Token: 0x06007052 RID: 28754 RVA: 0x00266E7C File Offset: 0x0026507C
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return base.Owner.IsOstyMissing;
			}
		}

		// Token: 0x17001EA0 RID: 7840
		// (get) Token: 0x06007053 RID: 28755 RVA: 0x00266E89 File Offset: 0x00265089
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.OstyAttack };
			}
		}

		// Token: 0x17001EA1 RID: 7841
		// (get) Token: 0x06007054 RID: 28756 RVA: 0x00266E98 File Offset: 0x00265098
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Retain));
			}
		}

		// Token: 0x17001EA2 RID: 7842
		// (get) Token: 0x06007055 RID: 28757 RVA: 0x00266EA5 File Offset: 0x002650A5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new OstyDamageVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x06007056 RID: 28758 RVA: 0x00266EB8 File Offset: 0x002650B8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			if (!Osty.CheckMissingWithAnim(base.Owner))
			{
				await DamageCmd.Attack(base.DynamicVars.OstyDamage.BaseValue).FromOsty(base.Owner.Osty, this).Targeting(cardPlay.Target)
					.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
					.Execute(choiceContext);
			}
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, (CardModel c) => !c.Keywords.Contains(CardKeyword.Retain), this);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.ApplyKeyword(cardModel, new CardKeyword[] { CardKeyword.Retain });
			}
		}

		// Token: 0x06007057 RID: 28759 RVA: 0x00266F0B File Offset: 0x0026510B
		protected override void OnUpgrade()
		{
			base.DynamicVars.OstyDamage.UpgradeValueBy(3m);
		}
	}
}
