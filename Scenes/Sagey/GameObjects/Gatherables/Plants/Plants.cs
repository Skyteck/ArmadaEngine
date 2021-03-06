﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaEngine.Scenes.Sagey.GameObjects;
namespace ArmadaEngine.Scenes.Sagey.GameObjects.Gatherables.Plants
{
    class StrawberryPlant : Plant
    {
        public StrawberryPlant()
        {
            MyPlantType = PlantType.kStrawBerryType;
            _GrowTime = 10;
            this.Name = "StrawberryPlant";
            _MyDrops = new ItemBundle();
            _MyDrops.outputID = Enums.ItemID.kItemStrawberry;
            _MyDrops.amount = 3;
            _MyDrops.odds = 100;
            CurrentDrop = _MyDrops;

            Setup();
        }
    }

    class PotatoPlant : Plant
    {
        public PotatoPlant()
        {
            MyPlantType = PlantType.kPotatoPlant;
            _GrowTime = 10;
            this.Name = "PotatoPlant";
        }
    }

    class CornPlant : Plant
    {
        public CornPlant()
        {
            MyPlantType = PlantType.kCornPlant;
            _GrowTime = 30;
            this.Name = "CornPlant";
        }
    }
}
