using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaEngine.BaseObjects;
using Microsoft.Xna.Framework.Graphics;
using ArmadaEngine.Helpers;

namespace ArmadaEngine.Scenes.mm.Sprites
{
    class PuzzlePiece : Sprite
    {
        public Rectangle myRect;
        public bool Clicked = false;
        public Vector2 ClickOffset = Vector2.Zero;
        public bool HasGravity = false;
        public int FallSpeed = 125;
        public int Row;
        public int Col;
        public float origZ;
        public List<PuzzlePiece> linkedPieces = new List<PuzzlePiece>();
        public List<Rectangle> RectsToDraw = new List<Rectangle>();
        public bool delete = false;
        public Rectangle topRect
        {
            get
            {
                return new Rectangle(this._BoundingBox.Left, this._BoundingBox.Top - 3, this.frameWidth, 6);
            }
        }

        public Rectangle bottomRect
        {
            get
            {
                return new Rectangle(this._BoundingBox.Left, this._BoundingBox.Bottom - 3, this.frameWidth, 6);
            }
        }

        public Rectangle leftRect
        {
            get
            {
                return new Rectangle(this._BoundingBox.Left - 3, this._BoundingBox.Top, 6, this.frameHeight);
            }
        }

        public Rectangle rightRect
        {
            get
            {
                return new Rectangle(this._BoundingBox.Right - 3, this._BoundingBox.Top, 6, this.frameHeight);
            }
        }

        public void Setup(int row, int col, int height, int width)
        {
            myRect = new Rectangle(col * width, row * height, width, height);
            this.frameHeight = height;
            this.frameWidth = width;
            Row = row;
            Col = col;
            origZ = _ZOrder;
            RectsToDraw.Add(myRect);
        }

        public void Update(GameTime gt, List<PuzzlePiece> pList)
        {
            base.Update(gt);


            //Placeable logic
            if (Clicked)
            {
                if (InputHelper.LeftButtonReleased)
                {
                    Clicked = false;
                    CheckNeighbors(pList);
                    _ZOrder = origZ;

                }
                else
                {
                    this._Position = (InputHelper.MouseScreenPos + ClickOffset);
                }
            }
            else
            {
                if (this._Position.Y < 450)
                {
                    if (HasGravity)
                    {
                        this._Position.Y += (float)(FallSpeed * gt.ElapsedGameTime.TotalSeconds);
                    }
                }

                if (InputHelper.LeftButtonClicked && this._BoundingBox.Contains(InputHelper.MouseScreenPos))
                {
                    Clicked = true;
                    ClickOffset.X = this._Position.X - InputHelper.MouseScreenPos.X;
                    ClickOffset.Y = this._Position.Y - InputHelper.MouseScreenPos.Y;
                    _ZOrder = 0f;
                }
            }
        }

        private void CheckNeighbors(List<PuzzlePiece> pList)
        {
            foreach (PuzzlePiece p in pList)
            {
                //check for piece to right
                if (p == this) continue;
                bool linkFound = false;
                if (p.Col == (this.Col + 1) && p.Row == this.Row)
                {
                    if (this.rightRect.Intersects(p.leftRect))
                    {
                        this._Position.Y = p._Position.Y;
                        this._Position.X = p._Position.X - this.frameWidth;
                        linkFound = true;
                    }

                }
                // check for piece to left
                if (p.Col == (this.Col - 1) && p.Row == this.Row)
                {
                    if (this.leftRect.Intersects(p.rightRect))
                    {
                        this._Position.Y = p._Position.Y;
                        this._Position.X = p._Position.X + this.frameWidth;
                        linkFound = true;
                    }

                }

                //below
                if (p.Row == (this.Row + 1) && p.Col == this.Col)
                {
                    if (this.bottomRect.Intersects(p.topRect))
                    {
                        this._Position.Y = p._Position.Y - frameHeight;
                        this._Position.X = p._Position.X;
                        linkFound = true;
                    }

                }

                //above
                if (p.Row == (this.Row - 1) && p.Col == this.Col)
                {
                    if (this.topRect.Intersects(p.bottomRect))
                    {
                        this._Position.Y = p._Position.Y + frameHeight;
                        this._Position.X = p._Position.X;
                        linkFound = true;
                    }


                }
                //if(linkFound)
                //{
                //    RectsToDraw.Add(p.myRect);
                //}

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(Rectangle r in RectsToDraw)
            {
                base.Draw(spriteBatch, r);

            }
        }
    }
}
