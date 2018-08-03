using ArmadaEngine.BaseObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaEngine.Scenes.TestScenes
{
    class SceneTwo : Scenes.Scene
    {
        Sprite tester;
        public SceneTwo(ContentManager c, Scenes.SceneManager sm, Camera.ArmadaCamera ca) : base(c, sm, ca)
        {
            this._Name = "Scene Two";

        }
        public override void LoadContent()
        {
            base.LoadContent();
            tester = new Sprite();
            tester.LoadContent("Art/testAgain", _Content);
            tester._Position = new Vector2(100, 100);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            tester = null;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            if(Helpers.InputHelper.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D2))
            {
                _SM.ActivateScene("Test Scene");
                return;
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
