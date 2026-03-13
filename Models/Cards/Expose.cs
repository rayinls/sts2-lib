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
	// Token: 0x02000941 RID: 2369
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Expose : CardModel
	{
		// Token: 0x06006A76 RID: 27254 RVA: 0x0025B14E File Offset: 0x0025934E
		public Expose()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C2F RID: 7215
		// (get) Token: 0x06006A77 RID: 27255 RVA: 0x0025B15B File Offset: 0x0025935B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Power", 2m));
			}
		}

		// Token: 0x17001C30 RID: 7216
		// (get) Token: 0x06006A78 RID: 27256 RVA: 0x0025B172 File Offset: 0x00259372
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001C31 RID: 7217
		// (get) Token: 0x06006A79 RID: 27257 RVA: 0x0025B17A File Offset: 0x0025937A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<VulnerablePower>(),
					HoverTipFactory.FromPower<ArtifactPower>(),
					HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x06006A7A RID: 27258 RVA: 0x0025B1A8 File Offset: 0x002593A8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			VfxCmd.PlayOnCreatureCenter(base.Owner.Creature, "vfx/vfx_flying_slash");
			int amount = base.DynamicVars["Power"].IntValue;
			await CreatureCmd.LoseBlock(cardPlay.Target, cardPlay.Target.Block);
			if (cardPlay.Target.HasPower<ArtifactPower>())
			{
				await PowerCmd.Remove<ArtifactPower>(cardPlay.Target);
			}
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, amount, base.Owner.Creature, this, false);
		}

		// Token: 0x06006A7B RID: 27259 RVA: 0x0025B1F3 File Offset: 0x002593F3
		protected override void OnUpgrade()
		{
			base.DynamicVars["Power"].UpgradeValueBy(1m);
		}

		// Token: 0x04002578 RID: 9592
		private const string _powerKey = "Power";
	}
}
