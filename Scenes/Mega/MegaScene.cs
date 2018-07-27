using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArmadaEngine.TileMaps;
using ArmadaEngine.Helpers;
using Microsoft.Xna.Framework.Input;
using TiledSharp;

namespace ArmadaEngine.Scenes.Mega
{
    class MegaScene : Scenes.Scene
    {

        TilemapManager _MapManager;
        Player testGuy;
        List<Rectangle> rectList = new List<Rectangle>();
        

        FPSCounter fps = new FPSCounter();
        
        public MegaScene(Microsoft.Xna.Framework.Content.ContentManager c, Scenes.SceneManager sm, Camera.TestCamera ca) : base(c, sm, ca)
        {
            _Name = "Mega";
        }

        public override void LoadContent()
        {
            base.LoadContent();
            rectList = new List<Rectangle>();
            testGuy = new Player();

            testGuy.LoadContent(@"Art/Player", _Content);
            testGuy._Position = new Vector2(200, 0);

            _MapManager = new TilemapManager();
            _MapManager.LoadMap("MegaLevel", _Content);
            LoadCollision(_MapManager.findMapByName("MegaLevel"));
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            _MapManager = null;
            testGuy = null;
            rectList = null;
        }

        private void LoadCollision(TileMap theMap)
        {
            //TmxList<TmxObject> ObjectList = theMap.FindCollisions();
            //if (ObjectList != null)
            //{
            //    foreach (TmxObject thing in ObjectList)
            //    {
            //        Rectangle newR = new Rectangle((int)thing.X, (int)thing.Y, (int)thing.Width, (int)thing.Height);
            //        rectList.Add(newR);
            //    }
            //}
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            testGuy.UpdateActive(gt, rectList);

            _Camera._Position = testGuy._Position;
        }

        public override void Draw(SpriteBatch sb, Rectangle bounds)
        {
            base.Draw(sb, bounds);


            // TODO: Add your drawing code here
            _MapManager.Draw(sb, bounds);
            testGuy.Draw(sb);
        }
    }
}
