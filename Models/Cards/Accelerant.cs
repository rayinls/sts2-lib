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
	// Token: 0x02000885 RID: 2181
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Accelerant : CardModel
	{
		// Token: 0x060066AB RID: 26283 RVA: 0x00253E14 File Offset: 0x00252014
		public Accelerant()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001A8F RID: 6799
		// (get) Token: 0x060066AC RID: 26284 RVA: 0x00253E21 File Offset: 0x00252021
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x17001A90 RID: 6800
		// (get) Token: 0x060066AD RID: 26285 RVA: 0x00253E2D File Offset: 0x0025202D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Accelerant", 1m));
			}
		}

		// Token: 0x060066AE RID: 26286 RVA: 0x00253E44 File Offset: 0x00252044
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<AccelerantPower>(base.Owner.Creature, base.DynamicVars["Accelerant"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060066AF RID: 26287 RVA: 0x00253E87 File Offset: 0x00252087
		protected override void OnUpgrade()
		{
			base.DynamicVars["Accelerant"].UpgradeValueBy(1m);
		}

		// Token: 0x04002557 RID: 9559
		private const string _powerVarName = "Accelerant";
	}
}
