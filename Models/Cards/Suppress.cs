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
	// Token: 0x02000A81 RID: 2689
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Suppress : CardModel
	{
		// Token: 0x06007138 RID: 28984 RVA: 0x00268C92 File Offset: 0x00266E92
		public Suppress()
			: base(0, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001EF8 RID: 7928
		// (get) Token: 0x06007139 RID: 28985 RVA: 0x00268C9F File Offset: 0x00266E9F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x17001EF9 RID: 7929
		// (get) Token: 0x0600713A RID: 28986 RVA: 0x00268CAB File Offset: 0x00266EAB
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Innate);
			}
		}

		// Token: 0x17001EFA RID: 7930
		// (get) Token: 0x0600713B RID: 28987 RVA: 0x00268CB3 File Offset: 0x00266EB3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(11m, ValueProp.Move),
					new PowerVar<WeakPower>(3m)
				});
			}
		}

		// Token: 0x0600713C RID: 28988 RVA: 0x00268CE0 File Offset: 0x00266EE0
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

		// Token: 0x0600713D RID: 28989 RVA: 0x00268D33 File Offset: 0x00266F33
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(6m);
			base.DynamicVars.Weak.UpgradeValueBy(2m);
		}
	}
}
