using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200076D RID: 1901
	public class MysteriousKnight : FlailKnight
	{
		// Token: 0x06005CCE RID: 23758 RVA: 0x00237AC4 File Offset: 0x00235CC4
		[NullableContext(1)]
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<StrengthPower>(base.Creature, 6m, base.Creature, null, false);
			await PowerCmd.Apply<PlatingPower>(base.Creature, 6m, base.Creature, null, false);
		}
	}
}
