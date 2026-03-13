using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AC3 RID: 2755
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WroughtInWar : CardModel
	{
		// Token: 0x0600728E RID: 29326 RVA: 0x0026B3CD File Offset: 0x002695CD
		public WroughtInWar()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F85 RID: 8069
		// (get) Token: 0x0600728F RID: 29327 RVA: 0x0026B3DA File Offset: 0x002695DA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(7m, ValueProp.Move),
					new ForgeVar(5)
				});
			}
		}

		// Token: 0x17001F86 RID: 8070
		// (get) Token: 0x06007290 RID: 29328 RVA: 0x0026B3FF File Offset: 0x002695FF
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x06007291 RID: 29329 RVA: 0x0026B408 File Offset: 0x00269608
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(choiceContext);
			await ForgeCmd.Forge(base.DynamicVars.Forge.IntValue, base.Owner, this);
		}

		// Token: 0x06007292 RID: 29330 RVA: 0x0026B45B File Offset: 0x0026965B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
			base.DynamicVars.Forge.UpgradeValueBy(2m);
		}
	}
}
