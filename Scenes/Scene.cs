using ArmadaEngine.Camera;
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
        protected ContentManager _Content;
        public string _Name = "Untitled";
        public bool _Active = false;
        protected SceneManager _SM;
        protected TestCamera _Camera;
        public Scene(ContentManager c, SceneManager sm, TestCamera ca)
        {
            _Content = c;
            _SM = sm;
            _Camera = ca;
        }

        public virtual void Init()
        {

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
