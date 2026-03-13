using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009B5 RID: 2485
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Lethality : CardModel
	{
		// Token: 0x06006CE2 RID: 27874 RVA: 0x0026003C File Offset: 0x0025E23C
		public Lethality()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001D30 RID: 7472
		// (get) Token: 0x06006CE3 RID: 27875 RVA: 0x00260049 File Offset: 0x0025E249
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Ethereal);
			}
		}

		// Token: 0x17001D31 RID: 7473
		// (get) Token: 0x06006CE4 RID: 27876 RVA: 0x00260051 File Offset: 0x0025E251
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<LethalityPower>(50m));
			}
		}

		// Token: 0x06006CE5 RID: 27877 RVA: 0x00260064 File Offset: 0x0025E264
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<LethalityPower>(base.Owner.Creature, base.DynamicVars["LethalityPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006CE6 RID: 27878 RVA: 0x002600A7 File Offset: 0x0025E2A7
		protected override void OnUpgrade()
		{
			base.DynamicVars["LethalityPower"].UpgradeValueBy(25m);
		}
	}
}
