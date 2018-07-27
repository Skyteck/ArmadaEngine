using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ArmadaEngine.BaseObjects;
namespace ArmadaEngine.Scenes.mm
{
    class mmScene : Scene
    {
        Texture2D mmPic;
        List<Sprites.PuzzlePiece> splitPics = new List<Sprites.PuzzlePiece>();
        public mmScene(ContentManager c, SceneManager sm, Camera.TestCamera ca) : base(c, sm, ca)
        {
            this._Name = "mm";
        }

        public override void LoadContent()
        {
            base.LoadContent();
            int columns = 5;
            int rows = 5;
            mmPic = this._Content.Load<Texture2D>(@"Art/mm1");
            int pieceWidth = mmPic.Width / columns;
            int pieceHeight = mmPic.Height / rows;
            Vector2 startLoc = new Vector2(50, 50);
            // i == row j == col
            float zIncrease = 0.01f;
            int placed = 1;
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    Sprites.PuzzlePiece piece = new Sprites.PuzzlePiece();
                    piece.frameWidth = pieceWidth;
                    piece.frameHeight = pieceHeight;
                    piece.LoadContent(mmPic);
                    piece._ZOrder = zIncrease * placed;
                    piece.Setup(i, j, pieceHeight, pieceWidth);
                    piece._Position = startLoc + new Vector2(pieceWidth * j, pieceHeight * i);
                    splitPics.Add(piece);
                    placed++;
                }
            }

            List<Sprites.PuzzlePiece> randomList = new List<Sprites.PuzzlePiece>();
            randomList.AddRange(splitPics);

            Random ran = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int num = ran.Next(0, randomList.Count);
                    randomList[num]._Position = startLoc + new Vector2(pieceWidth * j, pieceHeight * i);
                    randomList.RemoveAt(num);
                }
            }


        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            _Camera.Update();

            //splitPics = splitPics.OrderBy(x => x._ZOrder).ToList();
            if (splitPics.Count(x=>x.Clicked) >= 1)
            {
                splitPics.Find(x => x.Clicked).Update(gt, splitPics);
                return;
            }
            else
            {
                foreach (Sprites.PuzzlePiece pp in splitPics)
                {
                    pp.Update(gt, splitPics);
                    if(pp.Clicked)
                    {
                        break;
                    }
                }
            }

            splitPics = splitPics.OrderByDescending(x => x._ZOrder).ToList();
            
        }

        public override void Draw(SpriteBatch sb, Rectangle b)
        {
            base.Draw(sb, b);
            foreach(Sprites.PuzzlePiece p in splitPics)
            {
                p.Draw(sb);
            }
        }
    }
}
