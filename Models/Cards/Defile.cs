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
	// Token: 0x02000914 RID: 2324
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Defile : CardModel
	{
		// Token: 0x06006983 RID: 27011 RVA: 0x002596C7 File Offset: 0x002578C7
		public Defile()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001BBF RID: 7103
		// (get) Token: 0x06006984 RID: 27012 RVA: 0x002596D4 File Offset: 0x002578D4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(13m, ValueProp.Move));
			}
		}

		// Token: 0x17001BC0 RID: 7104
		// (get) Token: 0x06006985 RID: 27013 RVA: 0x002596E8 File Offset: 0x002578E8
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Ethereal);
			}
		}

		// Token: 0x06006986 RID: 27014 RVA: 0x002596F0 File Offset: 0x002578F0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006987 RID: 27015 RVA: 0x00259743 File Offset: 0x00257943
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}
