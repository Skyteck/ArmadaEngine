using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using ArmadaEngine.Helpers;

namespace ArmadaEngine.Scenes.Mega
{
    class Player : ArmadaEngine.BaseObjects.Sprite
    {
        // walk 1.5 p/f = 1.5 * 60 = 90
        // dash 3.5 p/f = 210
        // jump = 5 p/f = 5 * 60 = 300
        // ladder climb = 1.5 p/f = 90
        // max fall speed = 5.75 = 345
        // gravity = 0.25 p/f = 15
        float _Gravity = 30f;
        float _Friction = 5f;
        float _Speed = 180f;
        float _MaxJumpSpeed = 300f;
        float _MaxFallSpeed = 345f;
        float _AirTime = 0f;
        const float _MaxAirTime = .25f;
        Vector2 _Momentum = Vector2.Zero;
        Vector2 curPos;
        Texture2D rectTex;
        float _AirSpeed = 0f;

        bool _InAir = true;
        public virtual Rectangle _TopRect
        {
            get
            {
                return new Rectangle((int)(this._BoundingBox.Left), this._BoundingBox.Top, frameWidth, 1);
            }
        }
        public virtual Rectangle _LeftRect
        {
            get
            {
                return new Rectangle((int)(this._BoundingBox.Left), this._BoundingBox.Top, 3, frameHeight - 5 );
            }
        }

        public virtual Rectangle _RightRect
        {
            get
            {
                //return new Rectangle((int)(this._Position.X + (frameWidth / 2)), (int)this._Position.Y - (frameHeight / 2), 3, frameHeight - 5);
                return new Rectangle((int)(this._BoundingBox.Right), (int)this._BoundingBox.Top, 3, frameHeight - 5);
            }
        }


        public virtual Rectangle _BottomRect
        {
            get
            {
                return new Rectangle((int)this._Position.X - (frameWidth/2), (int)this._Position.Y + (frameHeight/2), frameWidth, 3);
            }
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            rectTex = content.Load<Texture2D>(@"Art/edgeTex");
        }

        public void UpdateActive(GameTime gameTime, List<Rectangle> bList)
        {
            base.UpdateActive(gameTime);
            float moveAmt = (float)(_Speed * gameTime.ElapsedGameTime.TotalSeconds);

            if (InputHelper.IsKeyDown(Keys.W))
            {
                this._Position.Y -= moveAmt;
            }
            else if(InputHelper.IsKeyDown(Keys.S))
            {
                this._Position.Y += moveAmt;
            }

            if (InputHelper.IsKeyDown(Keys.A))
            {
                this._Position.X -= moveAmt;
            }
            else if (InputHelper.IsKeyDown(Keys.D))
            {
                this._Position.X += moveAmt;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(rectTex, _TopRect, Color.White);
            spriteBatch.Draw(rectTex, _BottomRect, Color.White);
            spriteBatch.Draw(rectTex, _RightRect, Color.Blue);
            spriteBatch.Draw(rectTex, _LeftRect, Color.Yellow);
        }
    }

    
}
