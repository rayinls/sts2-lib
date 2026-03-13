using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Settings;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000955 RID: 2389
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FlashOfSteel : CardModel
	{
		// Token: 0x06006AE4 RID: 27364 RVA: 0x0025C041 File Offset: 0x0025A241
		public FlashOfSteel()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001C5E RID: 7262
		// (get) Token: 0x06006AE5 RID: 27365 RVA: 0x0025C04E File Offset: 0x0025A24E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(5m, ValueProp.Move),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x06006AE6 RID: 27366 RVA: 0x0025C074 File Offset: 0x0025A274
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NThinSliceVfx.Create(cardPlay.Target, VfxColor.Cyan));
			}
			float num = base.Owner.Character.AttackAnimDelay;
			if (SaveManager.Instance.PrefsSave.FastMode == FastModeType.Normal)
			{
				num += 0.2f;
			}
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithAttackerAnim("Attack", num, null)
				.Execute(choiceContext);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06006AE7 RID: 27367 RVA: 0x0025C0C7 File Offset: 0x0025A2C7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}
	}
}
