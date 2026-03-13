using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008E9 RID: 2281
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Comet : CardModel
	{
		// Token: 0x060068A4 RID: 26788 RVA: 0x00257BFB File Offset: 0x00255DFB
		public Comet()
			: base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001B5D RID: 7005
		// (get) Token: 0x060068A5 RID: 26789 RVA: 0x00257C08 File Offset: 0x00255E08
		public override int CanonicalStarCost
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17001B5E RID: 7006
		// (get) Token: 0x060068A6 RID: 26790 RVA: 0x00257C0B File Offset: 0x00255E0B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(33m, ValueProp.Move),
					new PowerVar<VulnerablePower>(3m),
					new PowerVar<WeakPower>(3m)
				});
			}
		}

		// Token: 0x17001B5F RID: 7007
		// (get) Token: 0x060068A7 RID: 26791 RVA: 0x00257C44 File Offset: 0x00255E44
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<WeakPower>(),
					HoverTipFactory.FromPower<VulnerablePower>()
				});
			}
		}

		// Token: 0x060068A8 RID: 26792 RVA: 0x00257C64 File Offset: 0x00255E64
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(cardPlay.Target) : null);
			if (ncreature != null)
			{
				NSmallMagicMissileVfx nsmallMagicMissileVfx = NSmallMagicMissileVfx.Create(ncreature.GetBottomOfHitbox(), new Color("50b598"));
				NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(nsmallMagicMissileVfx);
				await Cmd.Wait(nsmallMagicMissileVfx.WaitTime, false);
			}
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithNoAttackerAnim()
				.Execute(choiceContext);
			await PowerCmd.Apply<WeakPower>(cardPlay.Target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060068A9 RID: 26793 RVA: 0x00257CB7 File Offset: 0x00255EB7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(11m);
		}
	}
}
