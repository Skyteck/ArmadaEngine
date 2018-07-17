using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArmadaEngine
{
    class TestScene : ArmadaEngine.Scenes.Scene
    {
        Sprite tester = new Sprite();
        public TestScene(ContentManager c, Scenes.SceneManager sm) : base(c, sm)
        {
            this._Name = "Test Scene";
        }

        public override void LoadContent()
        {
            base.LoadContent();
            tester.LoadContent("Art/test", Content);
            tester._Position = new Vector2(100, 100);
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            if(Helpers.InputHelper.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.D1))
            {
                _SM.ActivateScene("Scene Two");
                return;
            }
            else if(Helpers.InputHelper.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.D2))
            {
                _SM.ActivateScene("TmTest");
            }

            tester.Update(gt);
        }

        public override void Draw(SpriteBatch sb, Rectangle b)
        {
            base.Draw(sb, b);
            tester.Draw(sb);
        }
    }
}
