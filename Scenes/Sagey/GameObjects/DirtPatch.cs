using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArmadaEngine.Scenes.Sagey.GameObjects
{
    class DirtPatch : WorldObject
    {
        public Gatherables.Plant MyPlant { get; set; }

        public DirtPatch()
        {
            MyWorldObjectTag = WorldObjectTag.kDirtTag;
            _Detector = true;
        }
        
    }
}
