using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaEngine.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ArmadaEngine.Scenes.Sagey.UI.Inventory
{
    class InventoryItemsContainer : ArmadaEngine.UI.UIListContainer
    {
        public List<InventoryListItem> InvenItems = new List<InventoryListItem>();

        public InventoryItemsContainer(UIManager uim) : base(uim)
        {

        }

        public void SetItems(List<GameObjects.ItemBundle> items)
        {
            foreach(InventoryListItem i in InvenItems)
            {
                i.Reset();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            /* thinking through scroll bar
             * scroll down one click to increment items drawn by columns?
             * 
             * 
             * */
            base.Draw(spriteBatch);

            int columns = (int)(_Size.X / bufferX);
            int rows = (int)(_Size.Y / bufferY);
            int toDraw = columns * rows;
            int itemsDrawn = 0;
            int currentRow = 0;
            int currentColumn = 0;
            Vector2 StartPos = this._Position;
            StartPos.X += 8;
            StartPos.Y += 8;
            int spaceX = (int)_Size.X - (columns * bufferX);
            spaceX = spaceX / columns;

            int spaceY = (int)_Size.Y - (rows * bufferY);
            spaceY = spaceY / rows;

            while (itemsDrawn < InvenItems.Count)
            {
                //where to draw?
                if(InvenItems[itemsDrawn]._Show)
                {
                    //ADD SPACE BETWEEN ITEMS IF ROW IS FILLED
                    Vector2 pos = new Vector2(StartPos.X + (currentColumn * (bufferX + spaceX)), StartPos.Y + (currentRow * (bufferY + spaceY)));
                    InvenItems[itemsDrawn].SetPosition(pos);
                    InvenItems[itemsDrawn].Draw(spriteBatch);

                    currentColumn++;
                    if (currentColumn >= columns)
                    {
                        currentColumn = 0;
                        currentRow++;
                        if (currentRow >= rows)
                        {
                            break;
                        }
                    }
                }
                itemsDrawn++;

            }
        }
    }
}
