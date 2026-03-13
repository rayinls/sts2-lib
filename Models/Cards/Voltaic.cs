using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AB8 RID: 2744
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Voltaic : CardModel
	{
		// Token: 0x06007258 RID: 29272 RVA: 0x0026AE57 File Offset: 0x00269057
		public Voltaic()
			: base(2, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001F6B RID: 8043
		// (get) Token: 0x06007259 RID: 29273 RVA: 0x0026AE64 File Offset: 0x00269064
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<LightningOrb>()
				});
			}
		}

		// Token: 0x17001F6C RID: 8044
		// (get) Token: 0x0600725A RID: 29274 RVA: 0x0026AE88 File Offset: 0x00269088
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(0m);
				array[1] = new CalculationExtraVar(1m);
				array[2] = new CalculatedVar("CalculatedChannels").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => CombatManager.Instance.History.Entries.OfType<OrbChanneledEntry>().Count((OrbChanneledEntry e) => e.Actor.Player == card.Owner && e.Orb is LightningOrb));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x17001F6D RID: 8045
		// (get) Token: 0x0600725B RID: 29275 RVA: 0x0026AEEB File Offset: 0x002690EB
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x0600725C RID: 29276 RVA: 0x0026AEF4 File Offset: 0x002690F4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			int lightningChanneledCount = (int)((CalculatedVar)base.DynamicVars["CalculatedChannels"]).Calculate(cardPlay.Target);
			for (int i = 0; i < lightningChanneledCount; i++)
			{
				await OrbCmd.Channel<LightningOrb>(choiceContext, base.Owner);
			}
		}

		// Token: 0x0600725D RID: 29277 RVA: 0x0026AF47 File Offset: 0x00269147
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}

		// Token: 0x040025E4 RID: 9700
		private const string _calculatedChannelsKey = "CalculatedChannels";
	}
}
