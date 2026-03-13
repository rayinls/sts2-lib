using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Characters
{
	// Token: 0x02000883 RID: 2179
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Silent : CharacterModel
	{
		// Token: 0x17001A7B RID: 6779
		// (get) Token: 0x06006691 RID: 26257 RVA: 0x00253AE6 File Offset: 0x00251CE6
		public override Color NameColor
		{
			get
			{
				return StsColors.green;
			}
		}

		// Token: 0x17001A7C RID: 6780
		// (get) Token: 0x06006692 RID: 26258 RVA: 0x00253AED File Offset: 0x00251CED
		public override CharacterGender Gender
		{
			get
			{
				return CharacterGender.Feminine;
			}
		}

		// Token: 0x17001A7D RID: 6781
		// (get) Token: 0x06006693 RID: 26259 RVA: 0x00253AF0 File Offset: 0x00251CF0
		[Nullable(2)]
		protected override CharacterModel UnlocksAfterRunAs
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001A7E RID: 6782
		// (get) Token: 0x06006694 RID: 26260 RVA: 0x00253AF3 File Offset: 0x00251CF3
		public override int StartingHp
		{
			get
			{
				return 70;
			}
		}

		// Token: 0x17001A7F RID: 6783
		// (get) Token: 0x06006695 RID: 26261 RVA: 0x00253AF7 File Offset: 0x00251CF7
		public override int StartingGold
		{
			get
			{
				return 99;
			}
		}

		// Token: 0x17001A80 RID: 6784
		// (get) Token: 0x06006696 RID: 26262 RVA: 0x00253AFB File Offset: 0x00251CFB
		public override CardPoolModel CardPool
		{
			get
			{
				return ModelDb.CardPool<SilentCardPool>();
			}
		}

		// Token: 0x17001A81 RID: 6785
		// (get) Token: 0x06006697 RID: 26263 RVA: 0x00253B02 File Offset: 0x00251D02
		public override RelicPoolModel RelicPool
		{
			get
			{
				return ModelDb.RelicPool<SilentRelicPool>();
			}
		}

		// Token: 0x17001A82 RID: 6786
		// (get) Token: 0x06006698 RID: 26264 RVA: 0x00253B09 File Offset: 0x00251D09
		public override PotionPoolModel PotionPool
		{
			get
			{
				return ModelDb.PotionPool<SilentPotionPool>();
			}
		}

		// Token: 0x17001A83 RID: 6787
		// (get) Token: 0x06006699 RID: 26265 RVA: 0x00253B10 File Offset: 0x00251D10
		public override IEnumerable<CardModel> StartingDeck
		{
			get
			{
				return new <>z__ReadOnlyArray<CardModel>(new CardModel[]
				{
					ModelDb.Card<StrikeSilent>(),
					ModelDb.Card<StrikeSilent>(),
					ModelDb.Card<StrikeSilent>(),
					ModelDb.Card<StrikeSilent>(),
					ModelDb.Card<StrikeSilent>(),
					ModelDb.Card<DefendSilent>(),
					ModelDb.Card<DefendSilent>(),
					ModelDb.Card<DefendSilent>(),
					ModelDb.Card<DefendSilent>(),
					ModelDb.Card<DefendSilent>(),
					ModelDb.Card<Neutralize>(),
					ModelDb.Card<Survivor>()
				});
			}
		}

		// Token: 0x17001A84 RID: 6788
		// (get) Token: 0x0600669A RID: 26266 RVA: 0x00253B8C File Offset: 0x00251D8C
		public override IReadOnlyList<RelicModel> StartingRelics
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<RelicModel>(ModelDb.Relic<RingOfTheSnake>());
			}
		}

		// Token: 0x17001A85 RID: 6789
		// (get) Token: 0x0600669B RID: 26267 RVA: 0x00253B98 File Offset: 0x00251D98
		public override float AttackAnimDelay
		{
			get
			{
				return 0.15f;
			}
		}

		// Token: 0x17001A86 RID: 6790
		// (get) Token: 0x0600669C RID: 26268 RVA: 0x00253B9F File Offset: 0x00251D9F
		public override float CastAnimDelay
		{
			get
			{
				return 0.25f;
			}
		}

		// Token: 0x0600669D RID: 26269 RVA: 0x00253BA8 File Offset: 0x00251DA8
		public unsafe override List<string> GetArchitectAttackVfx()
		{
			int num = 4;
			List<string> list = new List<string>(num);
			CollectionsMarshal.SetCount<string>(list, num);
			Span<string> span = CollectionsMarshal.AsSpan<string>(list);
			int num2 = 0;
			*span[num2] = "vfx/vfx_dagger_spray";
			num2++;
			*span[num2] = "vfx/vfx_flying_slash";
			num2++;
			*span[num2] = "vfx/vfx_dramatic_stab";
			num2++;
			*span[num2] = "vfx/vfx_dagger_throw";
			return list;
		}

		// Token: 0x17001A87 RID: 6791
		// (get) Token: 0x0600669E RID: 26270 RVA: 0x00253C11 File Offset: 0x00251E11
		public override Color EnergyLabelOutlineColor
		{
			get
			{
				return new Color("004f04FF");
			}
		}

		// Token: 0x17001A88 RID: 6792
		// (get) Token: 0x0600669F RID: 26271 RVA: 0x00253C1D File Offset: 0x00251E1D
		public override Color DialogueColor
		{
			get
			{
				return new Color("284719");
			}
		}

		// Token: 0x17001A89 RID: 6793
		// (get) Token: 0x060066A0 RID: 26272 RVA: 0x00253C29 File Offset: 0x00251E29
		public override Color MapDrawingColor
		{
			get
			{
				return new Color("2F6729");
			}
		}

		// Token: 0x17001A8A RID: 6794
		// (get) Token: 0x060066A1 RID: 26273 RVA: 0x00253C35 File Offset: 0x00251E35
		public override Color RemoteTargetingLineColor
		{
			get
			{
				return new Color("2EBD5EFF");
			}
		}

		// Token: 0x17001A8B RID: 6795
		// (get) Token: 0x060066A2 RID: 26274 RVA: 0x00253C41 File Offset: 0x00251E41
		public override Color RemoteTargetingLineOutline
		{
			get
			{
				return new Color("004f04FF");
			}
		}

		// Token: 0x060066A3 RID: 26275 RVA: 0x00253C50 File Offset: 0x00251E50
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			AnimState animState6 = new AnimState("shiv", false);
			AnimState animState7 = new AnimState("relaxed_loop", true);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState6.NextState = animState;
			animState7.AddBranch("Idle", animState, null);
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Shiv", animState6, null);
			creatureAnimator.AddAnyState("Relaxed", animState7, null);
			return creatureAnimator;
		}

		// Token: 0x04002555 RID: 9557
		public const string shivTrigger = "Shiv";

		// Token: 0x04002556 RID: 9558
		public const string energyColorName = "silent";
	}
}
