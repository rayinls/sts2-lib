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
	// Token: 0x0200093B RID: 2363
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Eradicate : CardModel
	{
		// Token: 0x06006A54 RID: 27220 RVA: 0x0025AD3B File Offset: 0x00258F3B
		public Eradicate()
			: base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C20 RID: 7200
		// (get) Token: 0x06006A55 RID: 27221 RVA: 0x0025AD48 File Offset: 0x00258F48
		protected override bool HasEnergyCostX
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C21 RID: 7201
		// (get) Token: 0x06006A56 RID: 27222 RVA: 0x0025AD4B File Offset: 0x00258F4B
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Retain);
			}
		}

		// Token: 0x17001C22 RID: 7202
		// (get) Token: 0x06006A57 RID: 27223 RVA: 0x0025AD53 File Offset: 0x00258F53
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(11m, ValueProp.Move));
			}
		}

		// Token: 0x06006A58 RID: 27224 RVA: 0x0025AD68 File Offset: 0x00258F68
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.ResolveEnergyXValue()).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x06006A59 RID: 27225 RVA: 0x0025ADBB File Offset: 0x00258FBB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
