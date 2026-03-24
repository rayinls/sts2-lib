using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Settings;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedWhirlwind : CardModel
	{
		public RedWhirlwind()
			: base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies, true)
		{
		}

		// (get) Token: 0x0600726B RID: 29291 RVA: 0x0026B068 File Offset: 0x00269268
		protected override bool HasEnergyCostX
		{
			get
			{
				return true;
			}
		}

		// (get) Token: 0x0600726C RID: 29292 RVA: 0x0026B06B File Offset: 0x0026926B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new DamageVar(5m, ValueProp.Move) };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			int num = base.ResolveEnergyXValue();
			if (num > 0)
			{
				Color color = new Color("FFFFFF80");
				double num2 = ((SaveManager.Instance.PrefsSave.FastMode == FastModeType.Fast) ? 0.2 : 0.3);
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance != null)
				{
					instance.CombatVfxContainer.AddChildSafely(NHorizontalLinesVfx.Create(color, 0.8 + (double)Mathf.Min(8, num) * num2, true));
				}
				SfxCmd.Play("event:/sfx/characters/ironclad/ironclad_whirlwind", 1f);
				NRun instance2 = NRun.Instance;
				if (instance2 != null)
				{
					instance2.GlobalUi.AddChildSafely(NSmokyVignetteVfx.Create(color, color));
				}
			}
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(num).FromCard(this)
				.TargetingAllOpponents(base.CombatState)
				.WithHitFx("vfx/vfx_giant_horizontal_slash", null, null)
				.Execute(choiceContext);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}

		private const string _whirlwindSfx = "event:/sfx/characters/ironclad/ironclad_whirlwind";
	}
}
