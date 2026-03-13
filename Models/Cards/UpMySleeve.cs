using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AAF RID: 2735
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class UpMySleeve : CardModel
	{
		// Token: 0x0600722C RID: 29228 RVA: 0x0026A94F File Offset: 0x00268B4F
		public UpMySleeve()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001F5A RID: 8026
		// (get) Token: 0x0600722D RID: 29229 RVA: 0x0026A95C File Offset: 0x00268B5C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x17001F5B RID: 8027
		// (get) Token: 0x0600722E RID: 29230 RVA: 0x0026A969 File Offset: 0x00268B69
		// (set) Token: 0x0600722F RID: 29231 RVA: 0x0026A971 File Offset: 0x00268B71
		private int TimesPlayedThisCombat
		{
			get
			{
				return this._timesPlayedThisCombat;
			}
			set
			{
				base.AssertMutable();
				this._timesPlayedThisCombat = value;
			}
		}

		// Token: 0x17001F5C RID: 8028
		// (get) Token: 0x06007230 RID: 29232 RVA: 0x0026A980 File Offset: 0x00268B80
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(false));
			}
		}

		// Token: 0x06007231 RID: 29233 RVA: 0x0026A990 File Offset: 0x00268B90
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++)
			{
				await Shiv.CreateInHand(base.Owner, base.CombatState);
				await Cmd.Wait(0.1f, false);
			}
			this.TimesPlayedThisCombat++;
			base.EnergyCost.AddThisCombat(-1, false);
		}

		// Token: 0x06007232 RID: 29234 RVA: 0x0026A9D3 File Offset: 0x00268BD3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}

		// Token: 0x040025E2 RID: 9698
		private int _timesPlayedThisCombat;
	}
}
