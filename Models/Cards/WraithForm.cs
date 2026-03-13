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
	// Token: 0x02000AC1 RID: 2753
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WraithForm : CardModel
	{
		// Token: 0x06007286 RID: 29318 RVA: 0x0026B2F4 File Offset: 0x002694F4
		public WraithForm()
			: base(3, CardType.Power, CardRarity.Ancient, TargetType.Self, true)
		{
		}

		// Token: 0x17001F81 RID: 8065
		// (get) Token: 0x06007287 RID: 29319 RVA: 0x0026B301 File Offset: 0x00269501
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<IntangiblePower>(2m),
					new PowerVar<WraithFormPower>(1m)
				});
			}
		}

		// Token: 0x17001F82 RID: 8066
		// (get) Token: 0x06007288 RID: 29320 RVA: 0x0026B329 File Offset: 0x00269529
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<IntangiblePower>(),
					HoverTipFactory.FromPower<DexterityPower>()
				});
			}
		}

		// Token: 0x06007289 RID: 29321 RVA: 0x0026B348 File Offset: 0x00269548
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<IntangiblePower>(base.Owner.Creature, base.DynamicVars["IntangiblePower"].BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<WraithFormPower>(base.Owner.Creature, base.DynamicVars["WraithFormPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600728A RID: 29322 RVA: 0x0026B38B File Offset: 0x0026958B
		protected override void OnUpgrade()
		{
			base.DynamicVars["IntangiblePower"].UpgradeValueBy(1m);
		}
	}
}
