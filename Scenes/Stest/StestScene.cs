using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaEngine.Camera;
using Microsoft.Xna.Framework.Content;
using ArmadaEngine.BaseObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArmadaEngine.Scenes.Stest
{
    internal class StestScene : Scene
    {
        BaseObjects.Sprite tester;
        Effect inverse;
        Texture2D bg;

        bool shaderOn = false;

        public StestScene(ContentManager c, SceneManager sm, ArmadaCamera ca = null) : base(c, sm, ca)
        {
            this._Name = "Shader test";
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _Content.RootDirectory = "Content/";
            tester = new Sprite();
            tester.LoadContent("Art/test", _Content);
            tester._Position = new Vector2(100, 100);
            bg = _Content.Load<Texture2D>("Art/mm1");
            _Content.RootDirectory = "Content/Scenes/Stest";
            inverse = _Content.Load<Effect>("Inverse");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            tester = null;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            if(Helpers.InputHelper.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                shaderOn = !shaderOn;
            }
        }

        public override void Draw(SpriteBatch sb, Rectangle b)
        {
            sb.Begin(SpriteSortMode.Immediate);
            base.Draw(sb, b);
            if(shaderOn)
            {
                inverse.Techniques[0].Passes[0].Apply();
            }
            sb.Draw(bg, new Vector2(0, 0), Color.White);
            tester.Draw(sb);
            sb.End();
        }
    }
}
