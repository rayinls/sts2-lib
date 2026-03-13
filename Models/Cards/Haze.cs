using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000984 RID: 2436
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Haze : CardModel
	{
		// Token: 0x06006BE7 RID: 27623 RVA: 0x0025E03E File Offset: 0x0025C23E
		public Haze()
			: base(3, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001CCC RID: 7372
		// (get) Token: 0x06006BE8 RID: 27624 RVA: 0x0025E04B File Offset: 0x0025C24B
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Sly);
			}
		}

		// Token: 0x17001CCD RID: 7373
		// (get) Token: 0x06006BE9 RID: 27625 RVA: 0x0025E053 File Offset: 0x0025C253
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PoisonPower>(4m));
			}
		}

		// Token: 0x17001CCE RID: 7374
		// (get) Token: 0x06006BEA RID: 27626 RVA: 0x0025E065 File Offset: 0x0025C265
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x06006BEB RID: 27627 RVA: 0x0025E074 File Offset: 0x0025C274
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			this.SpawnVfx();
			await Cmd.CustomScaledWait(0.2f, 0.4f, false, default(CancellationToken));
			foreach (Creature creature in base.CombatState.HittableEnemies)
			{
				await PowerCmd.Apply<PoisonPower>(creature, base.DynamicVars.Poison.BaseValue, base.Owner.Creature, this, false);
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06006BEC RID: 27628 RVA: 0x0025E0B8 File Offset: 0x0025C2B8
		private void SpawnVfx()
		{
			NCombatRoom instance = NCombatRoom.Instance;
			Node node = ((instance != null) ? instance.CombatVfxContainer : null);
			if (node != null)
			{
				NSmokyVignetteVfx nsmokyVignetteVfx = NSmokyVignetteVfx.Create(new Color(0.8f, 0.8f, 0.3f, 0.66f), new Color(0f, 4f, 0f, 0.33f));
				node.AddChildSafely(nsmokyVignetteVfx);
				foreach (Creature creature in base.CombatState.HittableEnemies)
				{
					node.AddChildSafely(NSmokePuffVfx.Create(creature, NSmokePuffVfx.SmokePuffColor.Green));
				}
			}
		}

		// Token: 0x06006BED RID: 27629 RVA: 0x0025E164 File Offset: 0x0025C364
		protected override void OnUpgrade()
		{
			base.DynamicVars.Poison.UpgradeValueBy(2m);
		}
	}
}
