using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008F7 RID: 2295
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CrashLanding : CardModel
	{
		// Token: 0x060068EC RID: 26860 RVA: 0x00258544 File Offset: 0x00256744
		public CrashLanding()
			: base(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001B7B RID: 7035
		// (get) Token: 0x060068ED RID: 26861 RVA: 0x00258551 File Offset: 0x00256751
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Debris>(false));
			}
		}

		// Token: 0x17001B7C RID: 7036
		// (get) Token: 0x060068EE RID: 26862 RVA: 0x0025855E File Offset: 0x0025675E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(21m, ValueProp.Move));
			}
		}

		// Token: 0x060068EF RID: 26863 RVA: 0x00258574 File Offset: 0x00256774
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_heavy_blunt", null, "heavy_attack.mp3")
				.WithHitVfxSpawnedAtBase()
				.Execute(choiceContext);
			int num = 10 - CardPile.GetCards(base.Owner, new PileType[] { PileType.Hand }).Count<CardModel>();
			List<CardModel> list = new List<CardModel>();
			for (int i = 0; i < num; i++)
			{
				list.Add(base.CombatState.CreateCard<Debris>(base.Owner));
			}
			await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Hand, true, CardPilePosition.Bottom);
		}

		// Token: 0x060068F0 RID: 26864 RVA: 0x002585BF File Offset: 0x002567BF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(5m);
		}
	}
}
