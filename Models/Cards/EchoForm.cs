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
	// Token: 0x0200092F RID: 2351
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EchoForm : CardModel
	{
		// Token: 0x06006A16 RID: 27158 RVA: 0x0025A6BE File Offset: 0x002588BE
		public EchoForm()
			: base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001C04 RID: 7172
		// (get) Token: 0x06006A17 RID: 27159 RVA: 0x0025A6CB File Offset: 0x002588CB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("EchoForm", 1m));
			}
		}

		// Token: 0x17001C05 RID: 7173
		// (get) Token: 0x06006A18 RID: 27160 RVA: 0x0025A6E1 File Offset: 0x002588E1
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Ethereal);
			}
		}

		// Token: 0x06006A19 RID: 27161 RVA: 0x0025A6EC File Offset: 0x002588EC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<EchoFormPower>(base.Owner.Creature, base.DynamicVars["EchoForm"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006A1A RID: 27162 RVA: 0x0025A72F File Offset: 0x0025892F
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Ethereal);
		}

		// Token: 0x04002572 RID: 9586
		private const string _echoFormKey = "EchoForm";
	}
}
