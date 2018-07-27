using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaEngine.Scenes.Sagey
{
    public class Delegates
    {
        public delegate void GameEvent(Enums.EventTypes eventType, string eventID);
        public delegate void NPCDyingDelegate(GameObjects.NPC theNPC);
        public delegate void NPCInteractDelegate(Enums.InteractType interactType, string interactID);
    }
}
