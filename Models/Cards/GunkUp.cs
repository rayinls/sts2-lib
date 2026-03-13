using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200097C RID: 2428
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GunkUp : CardModel
	{
		// Token: 0x06006BBF RID: 27583 RVA: 0x0025DB9F File Offset: 0x0025BD9F
		public GunkUp()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x06006BC0 RID: 27584 RVA: 0x0025DBAC File Offset: 0x0025BDAC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Slimed>(false));
			}
		}

		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x06006BC1 RID: 27585 RVA: 0x0025DBB9 File Offset: 0x0025BDB9
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(4m, ValueProp.Move),
					new RepeatVar(3)
				});
			}
		}

		// Token: 0x06006BC2 RID: 27586 RVA: 0x0025DBE0 File Offset: 0x0025BDE0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			AttackCommand attackCommand = DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.DynamicVars.Repeat.IntValue).FromCard(this)
				.Targeting(cardPlay.Target)
				.WithHitFx(null, null, "blunt_attack.mp3");
			Func<Creature, Node2D> func;
			if ((func = GunkUp.<>O.<0>__Create) == null)
			{
				func = (GunkUp.<>O.<0>__Create = new Func<Creature, Node2D>(NGoopyImpactVfx.Create));
			}
			await attackCommand.WithHitVfxNode(func).Execute(choiceContext);
			CardModel cardModel = base.CombatState.CreateCard<Slimed>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Discard, true, CardPilePosition.Bottom);
			CardCmd.PreviewCardPileAdd(cardPileAddResult, 1.2f, CardPreviewStyle.HorizontalLayout);
			await Cmd.Wait(0.5f, false);
		}

		// Token: 0x06006BC3 RID: 27587 RVA: 0x0025DC33 File Offset: 0x0025BE33
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
		}

		// Token: 0x02001EF4 RID: 7924
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007E02 RID: 32258
			[Nullable(new byte[] { 0, 1, 2 })]
			public static Func<Creature, Node2D> <0>__Create;
		}
	}
}
