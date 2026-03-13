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
	// Token: 0x02000AB6 RID: 2742
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VoidForm : CardModel
	{
		// Token: 0x0600724F RID: 29263 RVA: 0x0026AD4F File Offset: 0x00268F4F
		public VoidForm()
			: base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001F68 RID: 8040
		// (get) Token: 0x06007250 RID: 29264 RVA: 0x0026AD5C File Offset: 0x00268F5C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<VoidFormPower>(2m));
			}
		}

		// Token: 0x06007251 RID: 29265 RVA: 0x0026AD70 File Offset: 0x00268F70
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<VoidFormPower>(base.Owner.Creature, base.DynamicVars["VoidFormPower"].BaseValue, base.Owner.Creature, this, false);
			PlayerCmd.EndTurn(base.Owner, false, null);
		}

		// Token: 0x06007252 RID: 29266 RVA: 0x0026ADB3 File Offset: 0x00268FB3
		protected override void OnUpgrade()
		{
			base.DynamicVars["VoidFormPower"].UpgradeValueBy(1m);
		}
	}
}
