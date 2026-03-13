using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008FE RID: 2302
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DaggerSpray : CardModel
	{
		// Token: 0x0600690D RID: 26893 RVA: 0x00258939 File Offset: 0x00256B39
		public DaggerSpray()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001B89 RID: 7049
		// (get) Token: 0x0600690E RID: 26894 RVA: 0x00258946 File Offset: 0x00256B46
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(4m, ValueProp.Move));
			}
		}

		// Token: 0x0600690F RID: 26895 RVA: 0x0025895C File Offset: 0x00256B5C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			SfxCmd.Play("event:/sfx/characters/silent/silent_dagger_spray", 1f);
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(2).FromCard(this)
				.TargetingAllOpponents(base.CombatState)
				.WithAttackerFx(() => NDaggerSprayFlurryVfx.Create(base.Owner.Creature, new Color("#b1ccca"), true))
				.BeforeDamage(delegate
				{
					IReadOnlyList<Creature> hittableEnemies = base.CombatState.HittableEnemies;
					foreach (Creature creature in hittableEnemies)
					{
						NDaggerSprayImpactVfx ndaggerSprayImpactVfx = NDaggerSprayImpactVfx.Create(creature, new Color("#b1ccca"), true);
						NCombatRoom instance = NCombatRoom.Instance;
						if (instance != null)
						{
							instance.CombatVfxContainer.AddChildSafely(ndaggerSprayImpactVfx);
						}
					}
					return Task.CompletedTask;
				})
				.Execute(choiceContext);
		}

		// Token: 0x06006910 RID: 26896 RVA: 0x002589A7 File Offset: 0x00256BA7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}

		// Token: 0x0400256C RID: 9580
		private const string _daggerSpraySfx = "event:/sfx/characters/silent/silent_dagger_spray";
	}
}
