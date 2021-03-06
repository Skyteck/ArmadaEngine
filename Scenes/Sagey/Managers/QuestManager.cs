﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaEngine.Scenes.Sagey.Quests;

namespace ArmadaEngine.Scenes.Sagey.Managers
{
    public class QuestManager
    {
        public event Delegates.GameEvent QuestAcceptedEvent;

        List<Quest> Quests;

        public QuestManager()
        {
            Quests = new List<Quest>();
        }

        public void AttachEvents(EventManager em)
        {
            QuestAcceptedEvent += em.HandleEvent;
        }

        public void GenerateQuest()
        {
            Quest nq = new Quest();
            nq.QuestName = "My Test Quest";
            nq.QuestID = "TestQuest";

            QuestObjective QO = new QuestObjective();
            QO.Name = "Milk Cow";
            QO.ObjectiveInfo = new EventInfo { EventType = Enums.EventTypes.kEventNPCInteract, EventTitle = "CowMilk" };
            QO.lfEventType = Enums.EventTypes.kEventNPCInteract;
            QO.EventID = "CowMilk";

            QuestObjective QO2 = new QuestObjective();
            QO2.Name = "Kill 5 Slimes";
            QO2.ObjectiveInfo = new EventInfo { EventType = Enums.EventTypes.kEventNPCDying, EventTitle = "SLIME" };
            QO2.lfEventType = Enums.EventTypes.kEventNPCDying;
            QO2.EventID = "SLIMEKilled";
            QO2.Amount = 5;

            nq.Objectives.Add(QO2);
            nq.Objectives.Add(QO);
            nq.Active = false;
            Quests.Add(nq);
            
        }

        public void ActivateQuest(String QuestID)
        {
            Quest questTofind = Quests.Find(x => x.QuestID == QuestID);

            if(questTofind.Completed == false)
            {
                //if reqs met
                questTofind.Active = true;
            }
        }

        internal void CheckEvent(EventInfo eI)
        {
            List<Quest> ActiveQuests = Quests.FindAll(x => x.Active = true);
            foreach(Quest q in ActiveQuests)
            {
                List<QuestObjective> currentObjectives = q.Objectives.FindAll(x => x.Active == true && x.Completed == false);
                foreach(QuestObjective qo in currentObjectives)
                {
                    if(eI.EventType == qo.ObjectiveInfo.EventType)
                    {
                        if(eI.EventTitle == qo.ObjectiveInfo.EventTitle)
                        {

                            qo.currentProgress++;
                            Console.WriteLine("Objective: " + qo.Name + " progressed.");
                            Console.WriteLine("Objective: " + qo.Name + " " + (qo.Amount - qo.currentProgress).ToString() + " to go.");
                            if (qo.currentProgress >= qo.Amount)
                            {
                                qo.Completed = true;
                                Console.WriteLine("Objective: " + qo.Name + " completed.");
                                if(currentObjectives.Count == currentObjectives.FindAll(x=>x.Completed == true).Count)
                                {
                                    q.Completed = true;
                                    q.Active = false;
                                    Console.WriteLine("Quest: " + q.QuestName + " complete!");
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<Quest> GetActiveQuests()
        {
            return Quests.FindAll(x => x.Active);
        }

        public bool CheckQuestCompleted(string questID)
        {
            return Quests.Find(x => x.QuestID == questID).Completed;
        }
    }
}
