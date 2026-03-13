using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008FC RID: 2300
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CrushUnder : CardModel
	{
		// Token: 0x06006904 RID: 26884 RVA: 0x0025884D File Offset: 0x00256A4D
		public CrushUnder()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001B84 RID: 7044
		// (get) Token: 0x06006905 RID: 26885 RVA: 0x0025885A File Offset: 0x00256A5A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17001B85 RID: 7045
		// (get) Token: 0x06006906 RID: 26886 RVA: 0x00258866 File Offset: 0x00256A66
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(7m, ValueProp.Move),
					new DynamicVar("StrengthLoss", 1m)
				});
			}
		}

		// Token: 0x06006907 RID: 26887 RVA: 0x00258894 File Offset: 0x00256A94
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);
			IReadOnlyList<Creature> enemies = base.CombatState.HittableEnemies;
			foreach (Creature creature in enemies)
			{
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance != null)
				{
					instance.CombatVfxContainer.AddChildSafely(NSpikeSplashVfx.Create(creature, VfxColor.Red));
				}
			}
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_heavy_blunt", null, "blunt_attack.mp3")
				.WithHitVfxSpawnedAtBase()
				.Execute(choiceContext);
			await PowerCmd.Apply<CrushUnderPower>(enemies, base.DynamicVars["StrengthLoss"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006908 RID: 26888 RVA: 0x002588DF File Offset: 0x00256ADF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars["StrengthLoss"].UpgradeValueBy(1m);
		}

		// Token: 0x0400256B RID: 9579
		private const string _strengthLossKey = "StrengthLoss";
	}
}
