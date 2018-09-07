using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaEngine.UI
{
    class UIListItem : UIElement
    {
        Texture2D _BG;

        Texture2D _DebugTex;
        public bool debugMode = false;
        public bool _Selected = false;
        public Rectangle ListRect
        {
            get
            {
                return new Rectangle((int)this._Position.X, (int)this._Position.Y, (int)this._Size.X, (int)this._Size.Y);
            }
        }

        public UIListItem(UIManager uim) : base(uim)
        {

        }

        public override void LoadContent(string texName)
        {
            _BG = _UIManager.GetTexture(texName);
            _DebugTex = _UIManager.GetTexture("edgeTex");
            base.LoadContent(texName);
        }

        public override void Draw(SpriteBatch sb)
        {
            if(_BG != null)
            {
                sb.Draw(_BG, this._Position, Color.White);
            }

            if(debugMode || _Selected)
            {
                Helpers.HelperFunctions.DrawRectangleOutline(sb, this.ListRect, _DebugTex, Color.White, 2);
            }
            base.Draw(sb);
        }
    }
}
