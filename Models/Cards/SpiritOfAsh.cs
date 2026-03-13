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
	// Token: 0x02000A63 RID: 2659
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpiritOfAsh : CardModel
	{
		// Token: 0x0600709B RID: 28827 RVA: 0x0026776B File Offset: 0x0026596B
		public SpiritOfAsh()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001EBE RID: 7870
		// (get) Token: 0x0600709C RID: 28828 RVA: 0x00267778 File Offset: 0x00265978
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("BlockOnExhaust", 4m));
			}
		}

		// Token: 0x17001EBF RID: 7871
		// (get) Token: 0x0600709D RID: 28829 RVA: 0x0026778F File Offset: 0x0026598F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromKeyword(CardKeyword.Ethereal),
					HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x0600709E RID: 28830 RVA: 0x002677B4 File Offset: 0x002659B4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<SpiritOfAshPower>(base.Owner.Creature, base.DynamicVars["BlockOnExhaust"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600709F RID: 28831 RVA: 0x002677F7 File Offset: 0x002659F7
		protected override void OnUpgrade()
		{
			base.DynamicVars["BlockOnExhaust"].UpgradeValueBy(1m);
		}

		// Token: 0x040025D3 RID: 9683
		private const string _blockOnExhaustKey = "BlockOnExhaust";
	}
}
