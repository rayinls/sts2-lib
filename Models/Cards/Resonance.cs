using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A27 RID: 2599
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Resonance : CardModel
	{
		// Token: 0x06006F4B RID: 28491 RVA: 0x00264FB2 File Offset: 0x002631B2
		public Resonance()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001E2E RID: 7726
		// (get) Token: 0x06006F4C RID: 28492 RVA: 0x00264FBF File Offset: 0x002631BF
		public override int CanonicalStarCost
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001E2F RID: 7727
		// (get) Token: 0x06006F4D RID: 28493 RVA: 0x00264FC2 File Offset: 0x002631C2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(1m));
			}
		}

		// Token: 0x17001E30 RID: 7728
		// (get) Token: 0x06006F4E RID: 28494 RVA: 0x00264FD3 File Offset: 0x002631D3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06006F4F RID: 28495 RVA: 0x00264FE0 File Offset: 0x002631E0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			int intValue = base.DynamicVars["StrengthPower"].IntValue;
			await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, intValue, base.Owner.Creature, this, false);
			foreach (Creature creature in base.CombatState.HittableEnemies)
			{
				await PowerCmd.Apply<StrengthPower>(creature, -1m, base.Owner.Creature, this, false);
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06006F50 RID: 28496 RVA: 0x00265023 File Offset: 0x00263223
		protected override void OnUpgrade()
		{
			base.DynamicVars["StrengthPower"].UpgradeValueBy(1m);
		}
	}
}
