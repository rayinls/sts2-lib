using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Settings;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A53 RID: 2643
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Slice : CardModel
	{
		// Token: 0x06007039 RID: 28729 RVA: 0x00266BF4 File Offset: 0x00264DF4
		public Slice()
			: base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001E94 RID: 7828
		// (get) Token: 0x0600703A RID: 28730 RVA: 0x00266C01 File Offset: 0x00264E01
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(6m, ValueProp.Move));
			}
		}

		// Token: 0x0600703B RID: 28731 RVA: 0x00266C14 File Offset: 0x00264E14
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NThinSliceVfx.Create(cardPlay.Target, VfxColor.Red));
			}
			float num = base.Owner.Character.AttackAnimDelay;
			if (SaveManager.Instance.PrefsSave.FastMode == FastModeType.Normal)
			{
				num += 0.2f;
			}
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithAttackerAnim("Attack", num, null)
				.WithHitFx("vfx/vfx_attack_slash", null, "slash_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x0600703C RID: 28732 RVA: 0x00266C67 File Offset: 0x00264E67
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
