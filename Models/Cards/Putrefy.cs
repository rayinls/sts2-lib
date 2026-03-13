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
	// Token: 0x02000A10 RID: 2576
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Putrefy : CardModel
	{
		// Token: 0x06006ECF RID: 28367 RVA: 0x0026402B File Offset: 0x0026222B
		public Putrefy()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DFB RID: 7675
		// (get) Token: 0x06006ED0 RID: 28368 RVA: 0x00264038 File Offset: 0x00262238
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Power", 2m));
			}
		}

		// Token: 0x17001DFC RID: 7676
		// (get) Token: 0x06006ED1 RID: 28369 RVA: 0x0026404F File Offset: 0x0026224F
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001DFD RID: 7677
		// (get) Token: 0x06006ED2 RID: 28370 RVA: 0x00264057 File Offset: 0x00262257
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<WeakPower>(),
					HoverTipFactory.FromPower<VulnerablePower>()
				});
			}
		}

		// Token: 0x06006ED3 RID: 28371 RVA: 0x00264074 File Offset: 0x00262274
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			int amount = base.DynamicVars["Power"].IntValue;
			await PowerCmd.Apply<WeakPower>(cardPlay.Target, amount, base.Owner.Creature, this, false);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, amount, base.Owner.Creature, this, false);
		}

		// Token: 0x06006ED4 RID: 28372 RVA: 0x002640BF File Offset: 0x002622BF
		protected override void OnUpgrade()
		{
			base.DynamicVars["Power"].UpgradeValueBy(1m);
		}

		// Token: 0x040025C0 RID: 9664
		private const string _powerKey = "Power";
	}
}
