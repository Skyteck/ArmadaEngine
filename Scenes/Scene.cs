using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaEngine.Scenes
{
    class Scene
    {
        protected ContentManager Content;
        public string _Name = "Untitled";
        public bool _Active = false;
        protected SceneManager _SM;
        public Scene(ContentManager c, SceneManager sm)
        {
            Content = c;
            _SM = sm;
        }

        public virtual void LoadContent()
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gt)
        {
            Helpers.InputHelper.Update();
        }

        public virtual void Draw(SpriteBatch sb, Rectangle b)
        {
            if (!_Active) return;
        }
    }
}
