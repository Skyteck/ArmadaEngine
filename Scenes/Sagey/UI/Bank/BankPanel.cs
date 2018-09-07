using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaEngine.UI;
using ArmadaEngine.Scenes.Sagey.Managers;
using Microsoft.Xna.Framework;
using ArmadaEngine.Scenes.Sagey.UI.Bank;

namespace ArmadaEngine.Scenes.Sagey.UI
{
    class BankPanel : UIPanel
    {
        public event EventHandler BankChanged;

        BankManager _BankManager;
        BankItemsContainer ItemsContainer;
        public BankPanel(UIManager uim, BankManager bm) : base(uim)
        {
            _BankManager = bm;
            _Name = "Bank";
            _BankManager.BankChangedEvent += HandleBankChanged;
        }

        public override void LoadContent(string name)
        {
            base.LoadContent(name);

        }

        public override void Setup()
        {
            base.Setup();
            ItemsContainer = new BankItemsContainer(_UIManager);
            this.AddChild(ItemsContainer);
            ItemsContainer.OffsetPos = Vector2.Zero;
            ItemsContainer._Size = new Vector2(adJustedWidth - 5, adjustedHeight - 5);
            ItemsContainer._Name = "BankContainer";
            ItemsContainer.LoadContent("Panel");
            ItemsContainer.bufferX = 62;
            ItemsContainer.bufferY = 47;

            for (int i = 0; i < 28; i++)
            {
                Inventory.ItemListItem ili = new Inventory.ItemListItem(_UIManager);
                ili.debugMode = true;
                ili._Size = new Vector2(60, 45);
                ili.LoadContent("Inventory3BG");
                ili.Setup();

                ItemsContainer.InvenItems.Add(ili);
            }

            foreach(Inventory.ItemListItem ili in ItemsContainer.InvenItems)
            {
                ili.Reset();
            }

            int c = 0;
            foreach(GameObjects.ItemSlot s in _BankManager.itemSlots)
            {
                ItemsContainer.InvenItems[c].SetItem(s);
                c++;
            }
        }

        public override void ProcessClick(Vector2 pos)
        {
            base.ProcessClick(pos);

            foreach(Inventory.ItemListItem bli in ItemsContainer.InvenItems)
            {
                if(bli._Show && bli.ListRect.Contains(Helpers.InputHelper.MouseScreenPos))
                {
                    bli._Selected = true;
                    _BankManager.ToInventory(bli._ItemID, 1);
                }
                
            }
        }

        public void HandleBankChanged(object sender, EventArgs args)
        {
            if (!this._Show) return;
            foreach (Inventory.ItemListItem ili in ItemsContainer.InvenItems)
            {
                ili.Reset();
            }

            int c = 0;
            foreach (GameObjects.ItemSlot s in _BankManager.itemSlots)
            {
                ItemsContainer.InvenItems[c].SetItem(s);
                c++;
            }
        }
    }
}
