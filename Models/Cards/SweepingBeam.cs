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
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A83 RID: 2691
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SweepingBeam : CardModel
	{
		// Token: 0x06007143 RID: 28995 RVA: 0x00268DEF File Offset: 0x00266FEF
		public SweepingBeam()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001EFD RID: 7933
		// (get) Token: 0x06007144 RID: 28996 RVA: 0x00268DFC File Offset: 0x00266FFC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(6m, ValueProp.Move),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x06007145 RID: 28997 RVA: 0x00268E24 File Offset: 0x00267024
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Attack", base.Owner.Character.AttackAnimDelay);
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithAttackerAnim("Cast", 0.5f, null)
				.BeforeDamage(async delegate
				{
					List<Creature> list = base.CombatState.HittableEnemies.ToList<Creature>();
					NSweepingBeamVfx nsweepingBeamVfx = NSweepingBeamVfx.Create(base.Owner.Creature, list);
					if (nsweepingBeamVfx != null)
					{
						NCombatRoom instance = NCombatRoom.Instance;
						if (instance != null)
						{
							instance.CombatVfxContainer.AddChildSafely(nsweepingBeamVfx);
						}
						await Cmd.Wait(0.5f, false);
					}
				})
				.Execute(choiceContext);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06007146 RID: 28998 RVA: 0x00268E6F File Offset: 0x0026706F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
