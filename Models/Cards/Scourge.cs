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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A34 RID: 2612
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Scourge : CardModel
	{
		// Token: 0x06006F94 RID: 28564 RVA: 0x00265816 File Offset: 0x00263A16
		public Scourge()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E4E RID: 7758
		// (get) Token: 0x06006F95 RID: 28565 RVA: 0x00265823 File Offset: 0x00263A23
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<DoomPower>(13m),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x17001E4F RID: 7759
		// (get) Token: 0x06006F96 RID: 28566 RVA: 0x00265848 File Offset: 0x00263A48
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x06006F97 RID: 28567 RVA: 0x00265854 File Offset: 0x00263A54
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<DoomPower>(cardPlay.Target, base.DynamicVars.Doom.BaseValue, base.Owner.Creature, this, false);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06006F98 RID: 28568 RVA: 0x002658A7 File Offset: 0x00263AA7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Doom.UpgradeValueBy(3m);
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
