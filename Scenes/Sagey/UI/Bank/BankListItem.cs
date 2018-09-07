using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaEngine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ArmadaEngine.Scenes.Sagey.UI.Bank
{
    class BankListItem : UIListItem
    {
        public UILabel itemName;
        public UIIcon ItemIcon;
        Texture2D nullTex;

        public BankListItem(UIManager uim) : base(uim)
        {
            _Show = false;
        }

        public override void Setup()
        {
            base.Setup();
            itemName = new UILabel(new Vector2(5, 28), _UIManager);
            itemName.LoadContent("Fipps");
            AddChild(itemName);

            ItemIcon = new UIIcon(_UIManager);
            ItemIcon.LoadContent("Nulltexture");
            nullTex = ItemIcon._Texture;
            ItemIcon.OffsetPos = new Vector2(22, 10);
            AddChild(ItemIcon);
        }

        public void Reset()
        {
            itemName._Label = "";
            ItemIcon._Texture = nullTex;
            _Show = false;
        }

        public void SetItem(GameObjects.ItemSlot i)
        {
            if(i.Amount > 1)
            {
                if(i.Amount > 9999)
                {
                    int it = i.Amount / 1000;
                    if(it > 9999)
                    {
                        it = it / 1000;
                        itemName._Label = string.Format("{0:n0}", it) + "M";
                    }
                    else
                    {
                        itemName._Label = string.Format("{0:n0}", it) + "K";

                    }
                }
                else
                {
                    itemName._Label = string.Format("{0:n0}", i.Amount);
                }
                Vector2 labelSize = itemName.GetSize();
                itemName.OffsetPos.X = (int)((this._Size.X / 2) - (labelSize.X / 2));
            }
            ItemIcon._Texture = i.ItemInSlot.itemtexture;
            _Show = true;
        }
    }
}
