using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArmadaEngine.Scenes.TestScenes
{
    class ParticleTestScene : Scene
    {
        public ParticleTestScene(ContentManager c, SceneManager sm, Camera.TestCamera ca) : base(c, sm, ca)
        { 
            _Name = "Particle Test";
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb, Rectangle b)
        {
            base.Draw(sb, b);
        }
    }
}
