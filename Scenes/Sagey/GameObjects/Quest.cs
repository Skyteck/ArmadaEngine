using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaEngine.Scenes.Sagey.Quests
{
    public class Quest
    {
        public List<QuestObjective> Objectives;
        public string QuestID;
        public string QuestName;
        public bool Completed = false;
        public bool Active = false;

        public Quest()
        {
            Objectives = new List<QuestObjective>();
        }
    }

}
