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
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008FA RID: 2298
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CrimsonMantle : CardModel
	{
		// Token: 0x060068FA RID: 26874 RVA: 0x0025872E File Offset: 0x0025692E
		public CrimsonMantle()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B80 RID: 7040
		// (get) Token: 0x060068FB RID: 26875 RVA: 0x0025873B File Offset: 0x0025693B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<CrimsonMantlePower>(8m));
			}
		}

		// Token: 0x17001B81 RID: 7041
		// (get) Token: 0x060068FC RID: 26876 RVA: 0x0025874D File Offset: 0x0025694D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x060068FD RID: 26877 RVA: 0x00258760 File Offset: 0x00256960
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			NPowerUpVfx.CreateNormal(base.Owner.Creature);
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			CrimsonMantlePower crimsonMantlePower = await PowerCmd.Apply<CrimsonMantlePower>(base.Owner.Creature, base.DynamicVars["CrimsonMantlePower"].BaseValue, base.Owner.Creature, this, false);
			if (crimsonMantlePower != null)
			{
				crimsonMantlePower.IncrementSelfDamage();
			}
		}

		// Token: 0x060068FE RID: 26878 RVA: 0x002587A3 File Offset: 0x002569A3
		protected override void OnUpgrade()
		{
			base.DynamicVars["CrimsonMantlePower"].UpgradeValueBy(2m);
		}
	}
}
