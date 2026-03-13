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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000917 RID: 2327
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Defy : CardModel
	{
		// Token: 0x06006992 RID: 27026 RVA: 0x0025986F File Offset: 0x00257A6F
		public Defy()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001BC5 RID: 7109
		// (get) Token: 0x06006993 RID: 27027 RVA: 0x0025987C File Offset: 0x00257A7C
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BC6 RID: 7110
		// (get) Token: 0x06006994 RID: 27028 RVA: 0x0025987F File Offset: 0x00257A7F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(6m, ValueProp.Move),
					new PowerVar<WeakPower>(1m)
				});
			}
		}

		// Token: 0x17001BC7 RID: 7111
		// (get) Token: 0x06006995 RID: 27029 RVA: 0x002598A8 File Offset: 0x00257AA8
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Ethereal);
			}
		}

		// Token: 0x17001BC8 RID: 7112
		// (get) Token: 0x06006996 RID: 27030 RVA: 0x002598B0 File Offset: 0x00257AB0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x06006997 RID: 27031 RVA: 0x002598BC File Offset: 0x00257ABC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<WeakPower>(cardPlay.Target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006998 RID: 27032 RVA: 0x00259907 File Offset: 0x00257B07
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(1m);
			base.DynamicVars.Weak.UpgradeValueBy(1m);
		}
	}
}
