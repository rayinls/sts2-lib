using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000674 RID: 1652
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PlowPower : PowerModel
	{
		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x0600547D RID: 21629 RVA: 0x00224A43 File Offset: 0x00222C43
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x0600547E RID: 21630 RVA: 0x00224A46 File Offset: 0x00222C46
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x0600547F RID: 21631 RVA: 0x00224A49 File Offset: 0x00222C49
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Stun, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromPower<StrengthPower>()
				});
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06005480 RID: 21632 RVA: 0x00224A6C File Offset: 0x00222C6C
		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x00224A70 File Offset: 0x00222C70
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner)
			{
				if (result.UnblockedDamage > 0)
				{
					if (target.CurrentHp <= base.Amount)
					{
						base.Flash();
						CeremonialBeast monster = (CeremonialBeast)base.Owner.Monster;
						await PowerCmd.Remove<StrengthPower>(base.Owner);
						await monster.SetStunned();
						await CreatureCmd.Stun(base.Owner, new Func<IReadOnlyList<Creature>, Task>(monster.StunnedMove), monster.BeastCryState.StateId);
						await PowerCmd.Remove(this);
					}
				}
			}
		}
	}
}
