using System;
using System.Collections.Generic;
using System.Linq;
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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000995 RID: 2453
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Hyperbeam : CardModel
	{
		// Token: 0x06006C3E RID: 27710 RVA: 0x0025EC12 File Offset: 0x0025CE12
		public Hyperbeam()
			: base(2, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001CEE RID: 7406
		// (get) Token: 0x06006C3F RID: 27711 RVA: 0x0025EC1F File Offset: 0x0025CE1F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(26m, ValueProp.Move),
					new PowerVar<FocusPower>(3m)
				});
			}
		}

		// Token: 0x17001CEF RID: 7407
		// (get) Token: 0x06006C40 RID: 27712 RVA: 0x0025EC4A File Offset: 0x0025CE4A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<FocusPower>());
			}
		}

		// Token: 0x06006C41 RID: 27713 RVA: 0x0025EC58 File Offset: 0x0025CE58
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithAttackerAnim("Cast", 0.5f, null)
				.BeforeDamage(async delegate
				{
					List<Creature> enemies = base.CombatState.Enemies.Where((Creature e) => e.IsAlive).ToList<Creature>();
					NHyperbeamVfx nhyperbeamVfx = NHyperbeamVfx.Create(base.Owner.Creature, enemies.Last<Creature>());
					if (nhyperbeamVfx != null)
					{
						NCombatRoom instance = NCombatRoom.Instance;
						if (instance != null)
						{
							instance.CombatVfxContainer.AddChildSafely(nhyperbeamVfx);
						}
						await Cmd.Wait(0.5f, false);
					}
					foreach (Creature creature in enemies)
					{
						NHyperbeamImpactVfx nhyperbeamImpactVfx = NHyperbeamImpactVfx.Create(base.Owner.Creature, creature);
						if (nhyperbeamImpactVfx != null)
						{
							NCombatRoom instance2 = NCombatRoom.Instance;
							if (instance2 != null)
							{
								instance2.CombatVfxContainer.AddChildSafely(nhyperbeamImpactVfx);
							}
						}
					}
				})
				.Execute(choiceContext);
			await PowerCmd.Apply<FocusPower>(base.Owner.Creature, -base.DynamicVars["FocusPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006C42 RID: 27714 RVA: 0x0025ECA3 File Offset: 0x0025CEA3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(8m);
		}
	}
}
