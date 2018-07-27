using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaEngine.Scenes.Sagey.GameObjects.WorldObjects
{
    class Fire : WorldObject
    {

        public Fire()
        {
            this._Tag = SpriteType.kFireType;
            this._CurrentState = SpriteState.kStateActive;
            this.MyWorldObjectTag = WorldObjectTag.kFireTag;
        }
    }
}
