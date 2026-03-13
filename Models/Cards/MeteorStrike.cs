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
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009C9 RID: 2505
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MeteorStrike : CardModel
	{
		// Token: 0x06006D5F RID: 27999 RVA: 0x002611AF File Offset: 0x0025F3AF
		public MeteorStrike()
			: base(5, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D61 RID: 7521
		// (get) Token: 0x06006D60 RID: 28000 RVA: 0x002611BC File Offset: 0x0025F3BC
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// Token: 0x17001D62 RID: 7522
		// (get) Token: 0x06006D61 RID: 28001 RVA: 0x002611CB File Offset: 0x0025F3CB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(24m, ValueProp.Move));
			}
		}

		// Token: 0x17001D63 RID: 7523
		// (get) Token: 0x06006D62 RID: 28002 RVA: 0x002611DF File Offset: 0x0025F3DF
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<PlasmaOrb>()
				});
			}
		}

		// Token: 0x06006D63 RID: 28003 RVA: 0x00261204 File Offset: 0x0025F404
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(cardPlay.Target) : null);
			if (ncreature != null)
			{
				NLargeMagicMissileVfx nlargeMagicMissileVfx = NLargeMagicMissileVfx.Create(ncreature.GetBottomOfHitbox(), new Color("50b598"));
				NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(nlargeMagicMissileVfx);
				await Cmd.Wait(nlargeMagicMissileVfx.WaitTime, false);
			}
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx(null, null, "blunt_attack.mp3")
				.Execute(choiceContext);
			for (int i = 0; i < 3; i++)
			{
				await OrbCmd.Channel<PlasmaOrb>(choiceContext, base.Owner);
			}
		}

		// Token: 0x06006D64 RID: 28004 RVA: 0x00261257 File Offset: 0x0025F457
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(6m);
		}
	}
}
