using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009DF RID: 2527
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Nightmare : CardModel
	{
		// Token: 0x06006DD8 RID: 28120 RVA: 0x00262038 File Offset: 0x00260238
		public Nightmare()
			: base(3, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001D99 RID: 7577
		// (get) Token: 0x06006DD9 RID: 28121 RVA: 0x00262045 File Offset: 0x00260245
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001D9A RID: 7578
		// (get) Token: 0x06006DDA RID: 28122 RVA: 0x0026204D File Offset: 0x0026024D
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NNightmareHandsVfx.AssetPaths;
			}
		}

		// Token: 0x06006DDB RID: 28123 RVA: 0x00262054 File Offset: 0x00260254
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (TestMode.IsOff)
			{
				NSmokyVignetteVfx nsmokyVignetteVfx = NSmokyVignetteVfx.Create(new Color(0.8f, 0.3f, 0.8f, 0.66f), new Color(0f, 0f, 4f, 0.33f));
				NGame.Instance.CurrentRunNode.GlobalUi.AddChildSafely(nsmokyVignetteVfx);
				NGame.Instance.CurrentRunNode.GlobalUi.AddChildSafely(NNightmareHandsVfx.Create());
				await Cmd.CustomScaledWait(0.1f, 0.25f, false, default(CancellationToken));
			}
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, null, this);
			CardModel selectedCard = enumerable.FirstOrDefault<CardModel>();
			if (selectedCard != null)
			{
				NightmarePower nightmarePower = await PowerCmd.Apply<NightmarePower>(base.Owner.Creature, 3m, base.Owner.Creature, this, false);
				nightmarePower.SetSelectedCard(selectedCard);
			}
		}

		// Token: 0x06006DDC RID: 28124 RVA: 0x0026209F File Offset: 0x0026029F
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
