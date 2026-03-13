using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A8C RID: 2700
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Taunt : CardModel
	{
		// Token: 0x06007171 RID: 29041 RVA: 0x00269379 File Offset: 0x00267579
		public Taunt()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F0F RID: 7951
		// (get) Token: 0x06007172 RID: 29042 RVA: 0x00269386 File Offset: 0x00267586
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F10 RID: 7952
		// (get) Token: 0x06007173 RID: 29043 RVA: 0x00269389 File Offset: 0x00267589
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(7m, ValueProp.Move),
					new PowerVar<VulnerablePower>(1m)
				});
			}
		}

		// Token: 0x17001F11 RID: 7953
		// (get) Token: 0x06007174 RID: 29044 RVA: 0x002693B2 File Offset: 0x002675B2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x06007175 RID: 29045 RVA: 0x002693C0 File Offset: 0x002675C0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06007176 RID: 29046 RVA: 0x0026940B File Offset: 0x0026760B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(1m);
			base.DynamicVars.Vulnerable.UpgradeValueBy(1m);
		}
	}
}
