using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaEngine.Scenes.Sagey.GameObjects
{
    public class ItemSlot
    {
        private Item itemInSlot;
        private int amount;

        public Item ItemInSlot { get => itemInSlot; set => itemInSlot = value; }
        public int Amount { get => amount; set => amount = value; }

        public ItemSlot()
        {

        }
    }
}
