using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000770 RID: 1904
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Noisebot : MonsterModel
	{
		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06005CF3 RID: 23795 RVA: 0x00238183 File Offset: 0x00236383
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 24, 23);
			}
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06005CF4 RID: 23796 RVA: 0x0023818F File Offset: 0x0023638F
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 29, 28);
			}
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06005CF5 RID: 23797 RVA: 0x0023819B File Offset: 0x0023639B
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005CF6 RID: 23798 RVA: 0x002381A0 File Offset: 0x002363A0
		public override Task AfterAddedToRoom()
		{
			base.AfterAddedToRoom();
			if (TestMode.IsOff)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				FabricatorNormal.SetBotFallPosition(creatureNode);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005CF7 RID: 23799 RVA: 0x002381D8 File Offset: 0x002363D8
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("NOISE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.NoiseMove), new AbstractIntent[]
			{
				new StatusIntent(2)
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x00238228 File Offset: 0x00236428
		private async Task NoiseMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.6f);
			foreach (Creature creature in targets)
			{
				Player player = creature.Player ?? creature.PetOwner;
				CardPileAddResult[] statusCards = new CardPileAddResult[2];
				CardModel cardModel = base.CombatState.CreateCard<Dazed>(player);
				CardPileAddResult[] array = statusCards;
				array[0] = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Discard, false, CardPilePosition.Bottom);
				array = null;
				CardModel cardModel2 = base.CombatState.CreateCard<Dazed>(player);
				array = statusCards;
				array[1] = await CardPileCmd.AddGeneratedCardToCombat(cardModel2, PileType.Draw, false, CardPilePosition.Random);
				array = null;
				if (LocalContext.IsMe(player))
				{
					CardCmd.PreviewCardPileAdd(statusCards, 1.2f, CardPreviewStyle.HorizontalLayout);
					await Cmd.Wait(1f, false);
				}
				player = null;
				statusCards = null;
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x04002373 RID: 9075
		private const int _noiseStatusCount = 2;
	}
}
