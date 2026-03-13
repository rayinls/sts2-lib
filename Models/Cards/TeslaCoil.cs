using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A90 RID: 2704
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TeslaCoil : CardModel
	{
		// Token: 0x06007184 RID: 29060 RVA: 0x00269638 File Offset: 0x00267838
		public TeslaCoil()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F17 RID: 7959
		// (get) Token: 0x06007185 RID: 29061 RVA: 0x00269645 File Offset: 0x00267845
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromOrb<LightningOrb>());
			}
		}

		// Token: 0x17001F18 RID: 7960
		// (get) Token: 0x06007186 RID: 29062 RVA: 0x00269651 File Offset: 0x00267851
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(3m, ValueProp.Move));
			}
		}

		// Token: 0x06007187 RID: 29063 RVA: 0x00269664 File Offset: 0x00267864
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			List<LightningOrb> list = base.Owner.PlayerCombatState.OrbQueue.Orbs.OfType<LightningOrb>().ToList<LightningOrb>();
			foreach (LightningOrb lightningOrb in list)
			{
				await OrbCmd.Passive(choiceContext, lightningOrb, cardPlay.Target);
			}
			List<LightningOrb>.Enumerator enumerator = default(List<LightningOrb>.Enumerator);
		}

		// Token: 0x06007188 RID: 29064 RVA: 0x002696B7 File Offset: 0x002678B7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
