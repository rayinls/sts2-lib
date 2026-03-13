using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A17 RID: 2583
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Rally : CardModel
	{
		// Token: 0x06006EF1 RID: 28401 RVA: 0x0026449C File Offset: 0x0026269C
		public Rally()
			: base(2, CardType.Skill, CardRarity.Rare, TargetType.AllAllies, true)
		{
		}

		// Token: 0x17001E08 RID: 7688
		// (get) Token: 0x06006EF2 RID: 28402 RVA: 0x002644A9 File Offset: 0x002626A9
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001E09 RID: 7689
		// (get) Token: 0x06006EF3 RID: 28403 RVA: 0x002644AC File Offset: 0x002626AC
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E0A RID: 7690
		// (get) Token: 0x06006EF4 RID: 28404 RVA: 0x002644AF File Offset: 0x002626AF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(12m, ValueProp.Move));
			}
		}

		// Token: 0x06006EF5 RID: 28405 RVA: 0x002644C4 File Offset: 0x002626C4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<Creature> enumerable = from c in base.CombatState.GetTeammatesOf(base.Owner.Creature)
				where c != null && c.IsAlive && c.IsPlayer
				select c;
			foreach (Creature creature in enumerable)
			{
				await CreatureCmd.GainBlock(creature, base.DynamicVars.Block, cardPlay, false);
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06006EF6 RID: 28406 RVA: 0x0026450F File Offset: 0x0026270F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(5m);
		}
	}
}
