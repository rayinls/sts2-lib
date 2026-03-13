using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200097A RID: 2426
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GuidingStar : CardModel
	{
		// Token: 0x06006BB3 RID: 27571 RVA: 0x0025DA3B File Offset: 0x0025BC3B
		public GuidingStar()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001CB6 RID: 7350
		// (get) Token: 0x06006BB4 RID: 27572 RVA: 0x0025DA48 File Offset: 0x0025BC48
		public override int CanonicalStarCost
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001CB7 RID: 7351
		// (get) Token: 0x06006BB5 RID: 27573 RVA: 0x0025DA4B File Offset: 0x0025BC4B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(12m, ValueProp.Move),
					new CardsVar(2)
				});
			}
		}

		// Token: 0x06006BB6 RID: 27574 RVA: 0x0025DA74 File Offset: 0x0025BC74
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(cardPlay.Target) : null);
			if (ncreature != null)
			{
				SfxCmd.Play("event:/sfx/characters/regent/regent_guiding_star", 1f);
				NSmallMagicMissileVfx nsmallMagicMissileVfx = NSmallMagicMissileVfx.Create(ncreature.GetBottomOfHitbox(), new Color("50b598"));
				NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(nsmallMagicMissileVfx);
				await Cmd.Wait(nsmallMagicMissileVfx.WaitTime, false);
			}
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithNoAttackerAnim().FromCard(this)
				.Targeting(cardPlay.Target)
				.Execute(choiceContext);
			await PowerCmd.Apply<DrawCardsNextTurnPower>(base.Owner.Creature, base.DynamicVars.Cards.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006BB7 RID: 27575 RVA: 0x0025DAC7 File Offset: 0x0025BCC7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}

		// Token: 0x04002588 RID: 9608
		private const string _guidingStarSfx = "event:/sfx/characters/regent/regent_guiding_star";
	}
}
