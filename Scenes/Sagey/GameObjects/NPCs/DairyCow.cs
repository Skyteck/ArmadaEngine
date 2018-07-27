using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaEngine.Scenes.Sagey.Managers;
namespace ArmadaEngine.Scenes.Sagey.GameObjects.NPCs
{
    public class DairyCow : NPC
    {
        public DairyCow(NPCManager nm) : base(nm)
        {
            _Interactable = true;
        }

        public override void Interact()
        {
            base.Interact();
            _NPCManager.NPCInteract(Enums.EventTypes.kEventNPCInteract, "CowMilk");
            _NPCManager.AddItem(Enums.ItemID.kItemMilk, 1);
        }
    }
}
