using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008B9 RID: 2233
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Bombardment : CardModel
	{
		// Token: 0x060067B4 RID: 26548 RVA: 0x00255EC5 File Offset: 0x002540C5
		public Bombardment()
			: base(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001AFB RID: 6907
		// (get) Token: 0x060067B5 RID: 26549 RVA: 0x00255ED2 File Offset: 0x002540D2
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001AFC RID: 6908
		// (get) Token: 0x060067B6 RID: 26550 RVA: 0x00255EDA File Offset: 0x002540DA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(18m, ValueProp.Move));
			}
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x00255EF0 File Offset: 0x002540F0
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
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x060067B8 RID: 26552 RVA: 0x00255F44 File Offset: 0x00254144
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			CardPile pile = base.Pile;
			if (pile != null && pile.Type == PileType.Exhaust)
			{
				if (player == base.Owner)
				{
					await CardCmd.AutoPlay(choiceContext, this, null, AutoPlayType.Default, false, false);
				}
			}
		}

		// Token: 0x060067B9 RID: 26553 RVA: 0x00255F97 File Offset: 0x00254197
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(6m);
		}
	}
}
