﻿using Microsoft.Xna.Framework.Content;
using ArmadaEngine.Scenes.Sagey.Managers;


namespace ArmadaEngine.Scenes.Sagey.GameObjects.NPCs
{
    public class Banker : NPC
    {
        public Banker(NPCManager nm) : base(nm)
        {
            AddMessages("OpenBank");
            _Interactable = true;
        }
        

        public override void Interact()
        {
            base.Interact();
            _NPCManager.PlayDialogue("OpenBank");
        }
    }
}