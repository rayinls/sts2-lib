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
	// Token: 0x02000A48 RID: 2632
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Shatter : CardModel
	{
		// Token: 0x06006FFA RID: 28666 RVA: 0x002663E7 File Offset: 0x002645E7
		public Shatter()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001E79 RID: 7801
		// (get) Token: 0x06006FFB RID: 28667 RVA: 0x002663F4 File Offset: 0x002645F4
		public override OrbEvokeType OrbEvokeType
		{
			get
			{
				return OrbEvokeType.All;
			}
		}

		// Token: 0x17001E7A RID: 7802
		// (get) Token: 0x06006FFC RID: 28668 RVA: 0x002663F7 File Offset: 0x002645F7
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Evoke, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001E7B RID: 7803
		// (get) Token: 0x06006FFD RID: 28669 RVA: 0x00266409 File Offset: 0x00264609
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(11m, ValueProp.Move));
			}
		}

		// Token: 0x06006FFE RID: 28670 RVA: 0x00266420 File Offset: 0x00264620
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			int orbCount = base.Owner.PlayerCombatState.OrbQueue.Orbs.Count;
			for (int i = 0; i < orbCount; i++)
			{
				await OrbCmd.EvokeNext(choiceContext, base.Owner, true);
			}
		}

		// Token: 0x06006FFF RID: 28671 RVA: 0x0026646B File Offset: 0x0026466B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}
	}
}
