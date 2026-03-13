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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AA3 RID: 2723
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Tremble : CardModel
	{
		// Token: 0x060071EF RID: 29167 RVA: 0x0026A240 File Offset: 0x00268440
		public Tremble()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F41 RID: 8001
		// (get) Token: 0x060071F0 RID: 29168 RVA: 0x0026A24D File Offset: 0x0026844D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<VulnerablePower>(2m));
			}
		}

		// Token: 0x17001F42 RID: 8002
		// (get) Token: 0x060071F1 RID: 29169 RVA: 0x0026A25F File Offset: 0x0026845F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x060071F2 RID: 29170 RVA: 0x0026A26C File Offset: 0x0026846C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060071F3 RID: 29171 RVA: 0x0026A2B7 File Offset: 0x002684B7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Vulnerable.UpgradeValueBy(1m);
		}
	}
}
