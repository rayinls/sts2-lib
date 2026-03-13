using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009D3 RID: 2515
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MoltenFist : CardModel
	{
		// Token: 0x06006D99 RID: 28057 RVA: 0x002618A2 File Offset: 0x0025FAA2
		public MoltenFist()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D7D RID: 7549
		// (get) Token: 0x06006D9A RID: 28058 RVA: 0x002618AF File Offset: 0x0025FAAF
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<string>(SceneHelper.GetScenePath("vfx/vfx_molten_fist"));
			}
		}

		// Token: 0x17001D7E RID: 7550
		// (get) Token: 0x06006D9B RID: 28059 RVA: 0x002618C0 File Offset: 0x0025FAC0
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001D7F RID: 7551
		// (get) Token: 0x06006D9C RID: 28060 RVA: 0x002618C8 File Offset: 0x0025FAC8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(10m, ValueProp.Move));
			}
		}

		// Token: 0x17001D80 RID: 7552
		// (get) Token: 0x06006D9D RID: 28061 RVA: 0x002618DC File Offset: 0x0025FADC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x06006D9E RID: 28062 RVA: 0x002618E8 File Offset: 0x0025FAE8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_molten_fist", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			int num = (cardPlay.Target.IsAlive ? cardPlay.Target.GetPowerAmount<VulnerablePower>() : 0);
			if (num > 0)
			{
				await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, num, base.Owner.Creature, this, false);
			}
		}

		// Token: 0x06006D9F RID: 28063 RVA: 0x0026193B File Offset: 0x0025FB3B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}

		// Token: 0x040025B2 RID: 9650
		private const string _moltenFistVfxPath = "vfx/vfx_molten_fist";
	}
}
