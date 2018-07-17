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

namespace ArmadaEngine
{
    class tmTestScene : Scenes.Scene
    {

        TilemapManager _MapManager;
        List<Tile> path;
        Tile TileOne;
        Tile TileTwo;
        Texture2D rectTex;
        Rectangle tileRect;
        bool diagonalPaths = false;

        public tmTestScene(Microsoft.Xna.Framework.Content.ContentManager c, Scenes.SceneManager sm) : base(c, sm)
        {
            _Name = "TmTest";
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _MapManager = new TilemapManager();
            _MapManager.LoadMap("ProtoLevel", Content);
            rectTex = Content.Load<Texture2D>(@"Art/edgeTex");
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
            

            if (Helpers.InputHelper.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                _SM.ActivateScene("Mega");
                return;
            }
            Tile hoveredTile = _MapManager.findTile(InputHelper.MouseScreenPos);

            if (InputHelper.IsKeyPressed(Keys.D1))
            {
                TileOne = hoveredTile;
                if (TileOne != null && TileTwo != null)
                {
                    _MapManager.ResetTileColors();
                    path = _MapManager.AStarTwo(TileOne, TileTwo, diagonalPaths);
                }
            }

            if (InputHelper.IsKeyPressed(Keys.D2))
            {
                TileTwo = hoveredTile;
                if (TileOne != null && TileTwo != null)
                {
                    _MapManager.ResetTileColors();
                    path = _MapManager.AStarTwo(TileOne, TileTwo, diagonalPaths);
                }
            }

            if (InputHelper.IsKeyPressed(Keys.Space))
            {
                diagonalPaths = !diagonalPaths;

                if (TileOne != null && TileTwo != null)
                {
                    _MapManager.ResetTileColors();
                    path = _MapManager.AStarTwo(TileOne, TileTwo, diagonalPaths);
                }
            }

            if (InputHelper.IsKeyPressed(Keys.O))
            {
                TileOne = null;
                TileTwo = null;
                path = null;
            }
        }

        public override void Draw(SpriteBatch sb, Rectangle bounds)
        {
            base.Draw(sb, bounds);


            // TODO: Add your drawing code here
            _MapManager.Draw(sb, bounds);

            //if(hoveredTile != null)
            //{
            //    spriteBatch.Draw(rectTex, hoveredTile.destRect, Color.White);
            //}

            if (TileOne != null)
            {
                sb.Draw(rectTex, TileOne.destRect, Color.White);
            }


            if (TileTwo != null)
            {
                sb.Draw(rectTex, TileTwo.destRect, Color.Blue);
            }

            if (TileOne != null && TileTwo != null)
            {
                Tile prevTile = TileOne;
                if (path != null)
                {

                    foreach (Tile t in path)
                    {
                        DrawLine(sb, prevTile.tileCenter, t.tileCenter);
                        prevTile = t;
                    }
                }
            }
        }

        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(rectTex,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.Red, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
    }
}
