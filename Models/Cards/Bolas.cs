using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008B8 RID: 2232
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Bolas : CardModel
	{
		// Token: 0x060067AD RID: 26541 RVA: 0x00255DC1 File Offset: 0x00253FC1
		public Bolas()
			: base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001AF9 RID: 6905
		// (get) Token: 0x060067AE RID: 26542 RVA: 0x00255DCE File Offset: 0x00253FCE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(3m, ValueProp.Move));
			}
		}

		// Token: 0x17001AFA RID: 6906
		// (get) Token: 0x060067AF RID: 26543 RVA: 0x00255DE1 File Offset: 0x00253FE1
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return NBolasVfx.AssetPaths;
			}
		}

		// Token: 0x060067B0 RID: 26544 RVA: 0x00255DE8 File Offset: 0x00253FE8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NBolasVfx.Create(base.Owner.Creature, cardPlay.Target));
			}
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
		}

		// Token: 0x060067B1 RID: 26545 RVA: 0x00255E3C File Offset: 0x0025403C
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner)
			{
				bool flag = CombatManager.Instance.History.CardPlaysFinished.Any((CardPlayFinishedEntry e) => e.RoundNumber == base.CombatState.RoundNumber - 1 && e.CardPlay.Card == this);
				if (flag)
				{
					CardPile pile = base.Pile;
					if (pile == null || pile.Type != PileType.Hand)
					{
						await CardPileCmd.Add(this, PileType.Hand, CardPilePosition.Bottom, null, false);
					}
				}
			}
		}

		// Token: 0x060067B2 RID: 26546 RVA: 0x00255E87 File Offset: 0x00254087
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
		}
	}
}
