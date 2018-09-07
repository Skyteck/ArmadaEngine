using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaEngine.UI;
using ArmadaEngine.Scenes.Sagey.Managers;
using Microsoft.Xna.Framework;
using ArmadaEngine.Scenes.Sagey.UI.Inventory;

namespace ArmadaEngine.Scenes.Sagey.UI
{
    class InventoryPanel : UIPanel
    {
        public event EventHandler InventoryChanged;

        InventoryManager _InvenManager;
        Inventory.InventoryItemsContainer ItemsContainer;
        public InventoryPanel(UIManager uim, InventoryManager bm) : base(uim)
        {
            _InvenManager = bm;
            _Name = "Inventory";
            _InvenManager.InventoryChanged += HandleInventoryChanged;
        }

        public override void LoadContent(string name)
        {
            base.LoadContent(name);

        }

        public override void Setup()
        {
            base.Setup();
            ItemsContainer = new InventoryItemsContainer(_UIManager);
            this.AddChild(ItemsContainer);
            ItemsContainer.OffsetPos = Vector2.Zero;
            ItemsContainer._Size = new Vector2(adJustedWidth - 5, adjustedHeight - 5);
            ItemsContainer._Name = "InventoryContainer";
            ItemsContainer.LoadContent("Panel");
            ItemsContainer.bufferX = 62;
            ItemsContainer.bufferY = 47;

            for (int i = 0; i < 28; i++)
            {
                ItemListItem ili = new ItemListItem(_UIManager);
                ili._Size = new Vector2(60, 45);
                ili.LoadContent("Inventory3BG");
                ili.Setup();

                ItemsContainer.InvenItems.Add(ili);
            }

            foreach(ItemListItem ili in ItemsContainer.InvenItems)
            {
                ili.Reset();
            }

            int c = 0;
            foreach(GameObjects.ItemSlot s in _InvenManager.itemSlots)
            {
                ItemsContainer.InvenItems[c].SetItem(s);
                c++;
            }
        }

        public void HandleInventoryChanged(object sender, EventArgs args)
        {
            if (!this._Show) return;
            foreach (ItemListItem ili in ItemsContainer.InvenItems)
            {
                ili.Reset();
            }
            

            int c = 0;
            foreach (GameObjects.ItemSlot s in _InvenManager.itemSlots)
            {
                ItemsContainer.InvenItems[c].SetItem(s);
                c++;
            }
        }
    }
}
