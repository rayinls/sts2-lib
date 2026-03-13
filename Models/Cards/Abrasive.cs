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
	// Token: 0x02000884 RID: 2180
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Abrasive : CardModel
	{
		// Token: 0x060066A5 RID: 26277 RVA: 0x00253D57 File Offset: 0x00251F57
		public Abrasive()
			: base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001A8C RID: 6796
		// (get) Token: 0x060066A6 RID: 26278 RVA: 0x00253D64 File Offset: 0x00251F64
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<DexterityPower>(),
					HoverTipFactory.FromPower<ThornsPower>()
				});
			}
		}

		// Token: 0x17001A8D RID: 6797
		// (get) Token: 0x060066A7 RID: 26279 RVA: 0x00253D81 File Offset: 0x00251F81
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Sly);
			}
		}

		// Token: 0x17001A8E RID: 6798
		// (get) Token: 0x060066A8 RID: 26280 RVA: 0x00253D89 File Offset: 0x00251F89
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<ThornsPower>(4m),
					new PowerVar<DexterityPower>(1m)
				});
			}
		}

		// Token: 0x060066A9 RID: 26281 RVA: 0x00253DB4 File Offset: 0x00251FB4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<ThornsPower>(base.Owner.Creature, base.DynamicVars["ThornsPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060066AA RID: 26282 RVA: 0x00253DF7 File Offset: 0x00251FF7
		protected override void OnUpgrade()
		{
			base.DynamicVars["ThornsPower"].UpgradeValueBy(2m);
		}
	}
}
