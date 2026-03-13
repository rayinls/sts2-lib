using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000929 RID: 2345
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DramaticEntrance : CardModel
	{
		// Token: 0x060069F5 RID: 27125 RVA: 0x0025A324 File Offset: 0x00258524
		public DramaticEntrance()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001BF5 RID: 7157
		// (get) Token: 0x060069F6 RID: 27126 RVA: 0x0025A331 File Offset: 0x00258531
		protected override IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<string>(SceneHelper.GetScenePath("vfx/vfx_dramatic_entrance_fullscreen"));
			}
		}

		// Token: 0x17001BF6 RID: 7158
		// (get) Token: 0x060069F7 RID: 27127 RVA: 0x0025A342 File Offset: 0x00258542
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(11m, ValueProp.Move));
			}
		}

		// Token: 0x17001BF7 RID: 7159
		// (get) Token: 0x060069F8 RID: 27128 RVA: 0x0025A356 File Offset: 0x00258556
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Exhaust,
					CardKeyword.Innate
				});
			}
		}

		// Token: 0x060069F9 RID: 27129 RVA: 0x0025A36B File Offset: 0x0025856B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(4m);
		}

		// Token: 0x060069FA RID: 27130 RVA: 0x0025A384 File Offset: 0x00258584
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Attack", base.Owner.Character.AttackAnimDelay);
			VfxCmd.PlayFullScreenInCombat("vfx/vfx_dramatic_entrance_fullscreen");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
		}

		// Token: 0x04002570 RID: 9584
		public const string vfxPath = "vfx/vfx_dramatic_entrance_fullscreen";
	}
}
