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
	// Token: 0x020009E5 RID: 2533
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Oblivion : CardModel
	{
		// Token: 0x06006DF8 RID: 28152 RVA: 0x00262494 File Offset: 0x00260694
		public Oblivion()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DA7 RID: 7591
		// (get) Token: 0x06006DF9 RID: 28153 RVA: 0x002624A1 File Offset: 0x002606A1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<DoomPower>(3m));
			}
		}

		// Token: 0x17001DA8 RID: 7592
		// (get) Token: 0x06006DFA RID: 28154 RVA: 0x002624B3 File Offset: 0x002606B3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x06006DFB RID: 28155 RVA: 0x002624C0 File Offset: 0x002606C0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<OblivionPower>(cardPlay.Target, base.DynamicVars.Doom.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006DFC RID: 28156 RVA: 0x0026250B File Offset: 0x0026070B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Doom.UpgradeValueBy(1m);
		}
	}
}
