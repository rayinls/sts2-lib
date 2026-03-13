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
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Settings;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009DD RID: 2525
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Neutralize : CardModel
	{
		// Token: 0x06006DCD RID: 28109 RVA: 0x00261EE6 File Offset: 0x002600E6
		public Neutralize()
			: base(0, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D94 RID: 7572
		// (get) Token: 0x06006DCE RID: 28110 RVA: 0x00261EF3 File Offset: 0x002600F3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x17001D95 RID: 7573
		// (get) Token: 0x06006DCF RID: 28111 RVA: 0x00261EFF File Offset: 0x002600FF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(3m, ValueProp.Move),
					new PowerVar<WeakPower>(1m)
				});
			}
		}

		// Token: 0x06006DD0 RID: 28112 RVA: 0x00261F28 File Offset: 0x00260128
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NThinSliceVfx.Create(cardPlay.Target, VfxColor.Cyan));
			}
			float num = base.Owner.Character.AttackAnimDelay;
			if (SaveManager.Instance.PrefsSave.FastMode == FastModeType.Normal)
			{
				num += 0.2f;
			}
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithAttackerAnim("Attack", num, null)
				.Execute(choiceContext);
			await PowerCmd.Apply<WeakPower>(cardPlay.Target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006DD1 RID: 28113 RVA: 0x00261F7B File Offset: 0x0026017B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars.Weak.UpgradeValueBy(1m);
		}
	}
}
