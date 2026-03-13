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
	// Token: 0x02000930 RID: 2352
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EchoingSlash : CardModel
	{
		// Token: 0x06006A1B RID: 27163 RVA: 0x0025A738 File Offset: 0x00258938
		public EchoingSlash()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001C06 RID: 7174
		// (get) Token: 0x06006A1C RID: 27164 RVA: 0x0025A745 File Offset: 0x00258945
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(10m, ValueProp.Move));
			}
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x0025A75C File Offset: 0x0025895C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			AttackContext attackContext2 = await AttackCommand.CreateContextAsync(base.CombatState, this);
			AttackContext attackContext = attackContext2;
			object obj = null;
			int num = 0;
			try
			{
				IEnumerable<DamageResult> enumerable;
				for (int attackCount = 1; attackCount > 0; attackCount += enumerable.Count((DamageResult r) => r.WasTargetKilled))
				{
					attackCount--;
					enumerable = await CreatureCmd.Damage(choiceContext, base.CombatState.HittableEnemies, base.DynamicVars.Damage, base.Owner.Creature, this);
					attackContext.AddHit(enumerable);
				}
				num = 1;
			}
			catch (object obj)
			{
			}
			if (attackContext != null)
			{
				await attackContext.DisposeAsync();
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
				attackContext = null;
			}
		}

		// Token: 0x06006A1E RID: 27166 RVA: 0x0025A7A7 File Offset: 0x002589A7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
