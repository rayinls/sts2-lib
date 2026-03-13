using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008C3 RID: 2243
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BubbleBubble : CardModel
	{
		// Token: 0x060067EA RID: 26602 RVA: 0x002565E3 File Offset: 0x002547E3
		public BubbleBubble()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B12 RID: 6930
		// (get) Token: 0x060067EB RID: 26603 RVA: 0x002565F0 File Offset: 0x002547F0
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				CombatState combatState = base.CombatState;
				if (combatState == null)
				{
					return false;
				}
				return combatState.HittableEnemies.Any((Creature e) => e.HasPower<PoisonPower>());
			}
		}

		// Token: 0x17001B13 RID: 6931
		// (get) Token: 0x060067EC RID: 26604 RVA: 0x00256627 File Offset: 0x00254827
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x17001B14 RID: 6932
		// (get) Token: 0x060067ED RID: 26605 RVA: 0x00256633 File Offset: 0x00254833
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NSmokePuffVfx.AssetPaths;
			}
		}

		// Token: 0x17001B15 RID: 6933
		// (get) Token: 0x060067EE RID: 26606 RVA: 0x0025663A File Offset: 0x0025483A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PoisonPower>(9m));
			}
		}

		// Token: 0x060067EF RID: 26607 RVA: 0x00256650 File Offset: 0x00254850
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(cardPlay.Target) : null);
			if (ncreature != null)
			{
				NGaseousImpactVfx ngaseousImpactVfx = NGaseousImpactVfx.Create(ncreature.VfxSpawnPosition, new Color("83eb85"));
				NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(ngaseousImpactVfx);
			}
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			if (cardPlay.Target.HasPower<PoisonPower>())
			{
				await PowerCmd.Apply<PoisonPower>(cardPlay.Target, base.DynamicVars.Poison.BaseValue, base.Owner.Creature, this, false);
			}
		}

		// Token: 0x060067F0 RID: 26608 RVA: 0x0025669B File Offset: 0x0025489B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Poison.UpgradeValueBy(3m);
		}
	}
}
