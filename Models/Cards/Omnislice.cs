using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009E7 RID: 2535
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Omnislice : CardModel
	{
		// Token: 0x06006E03 RID: 28163 RVA: 0x002625D7 File Offset: 0x002607D7
		public Omnislice()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001DAC RID: 7596
		// (get) Token: 0x06006E04 RID: 28164 RVA: 0x002625E4 File Offset: 0x002607E4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(8m, ValueProp.Move));
			}
		}

		// Token: 0x06006E05 RID: 28165 RVA: 0x002625F8 File Offset: 0x002607F8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			AttackContext attackContext = await AttackCommand.CreateContextAsync(base.CombatState, this);
			AttackContext context = attackContext;
			object obj = null;
			int num = 0;
			try
			{
				List<DamageResult> list = (await CreatureCmd.Damage(choiceContext, cardPlay.Target, base.DynamicVars.Damage.BaseValue, ValueProp.Move, this)).ToList<DamageResult>();
				context.AddHit(list);
				DamageResult damageResult = list.FirstOrDefault<DamageResult>();
				if (damageResult != null)
				{
					List<Creature> list2 = (from e in base.CombatState.GetTeammatesOf(damageResult.Receiver).Except(new <>z__ReadOnlySingleElementList<Creature>(cardPlay.Target))
						where e.IsHittable
						select e).ToList<Creature>();
					if (list2.Count != 0)
					{
						AttackContext attackContext2 = context;
						attackContext2.AddHit(await CreatureCmd.Damage(choiceContext, list2, damageResult.TotalDamage + damageResult.OverkillDamage, ValueProp.Unpowered | ValueProp.Move, base.Owner.Creature, this));
						attackContext2 = null;
					}
				}
				num = 1;
			}
			catch (object obj)
			{
			}
			if (context != null)
			{
				await context.DisposeAsync();
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex = obj2 as Exception;
				if (ex == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
			if (num != 1)
			{
				obj = null;
				context = null;
			}
		}

		// Token: 0x06006E06 RID: 28166 RVA: 0x0026264B File Offset: 0x0026084B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
