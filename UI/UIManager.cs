using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ArmadaEngine.UI
{
    public class UIManager
    {        
        List<UIPanel> PanelList;
        ContentManager _ContentManager;

        Rectangle mmSlideInRect;
        Rectangle mmSlideOutRect;

        public UIManager(ContentManager c)
        {
            PanelList = new List<UIPanel>();
            mmSlideInRect = new Rectangle(0, 140, 60, 220);
            mmSlideOutRect = new Rectangle(0, 120, 100, 260);
            _ContentManager = c;
        }

        //public void LoadContent(ContentManager content)
        //{
        //    _ContentManager = content;
        //}

        public void CreatePanel(String name, Vector2 pos, Vector2 size, string texName)
        {
            UIPanel p = new UIPanel(this);
            p._Name = name;
            p.LoadContent(texName);
            p.SetPosition(pos);
            p.SetSize(size);
            PanelList.Add(p);
        }

        public void AddPanel(UIPanel p)
        {
            PanelList.Add(p);
        }

        public Texture2D GetTexture(string texName)
        {
            return _ContentManager.Load<Texture2D>(@"Art/" + texName);
        }

        public UIPanel GetUIPanel(string name)
        {
            return PanelList.Find(x => x._Name == name);
        }

        public void ShowPanel(string name)
        {
            PanelList.Find(x => x._Name == name).ShowPanel();
        }

        public void HidePanel(string name)
        {
            PanelList.Find(x => x._Name == name).HidePanel();
        }

        internal void HideAll()
        {
            foreach(UIPanel p in PanelList)
            {
                p.HidePanel();
            }
        }

        public void TogglePanel(string name)
        {
            PanelList.Find(x => x._Name == name).ToggleShow();
        }

        public void AttachButton(UIButton b)
        {
            b.ButtonPressed += HandleButtonClicked;
        }

        private void HandleButtonClicked(UIButton b)
        {
            if(b._Name == "Store")
            {
                TogglePanel("Store");
            }
        }

        public void Update(GameTime gt)
        {
            foreach(UIPanel p in PanelList)
            {
                p.Update(gt);
            }
            //only handling clicks for now
            if (Helpers.InputHelper.LeftButtonClicked)
            {
                foreach (UIPanel p in PanelList)
                {
                    p.ProcessClick(Helpers.InputHelper.MouseScreenPos);
                }
            }



        }

        internal SpriteFont GetFont(string v)
        {
            return _ContentManager.Load<SpriteFont>("Fonts/"+v);
        }

        public void Draw(SpriteBatch sb)
        {
            foreach(UIPanel p in PanelList)
            {
                p.Draw(sb);
            }
        }


    }
}
