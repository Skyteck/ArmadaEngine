﻿using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using ArmadaEngine.Scenes.Sagey.GameObjects;
using Newtonsoft.Json;

namespace ArmadaEngine.Scenes.Sagey.Managers
{
    public class ChemistryManager
    {
        public event Delegates.GameEvent ItemCraftedEvent;
        public event EventHandler RecipesChanged;

        InventoryManager _InvenManager;
        WorldObjectManager _WorldObjectManager;
        NPCManager _NPCManager;
        ContentManager _Content;
        ItemManager _ItemManager;
        public Item selectedItem;
        public List<Recipe> RecipeList;
        public List<Recipe> ActiveRecipes;



        public ChemistryManager(InventoryManager invenM, WorldObjectManager WOM, NPCManager NPCM, ContentManager content, ItemManager IM)
        {

            RecipeList = new List<Recipe>();
            ActiveRecipes = new List<Recipe>();
            _InvenManager = invenM;
            _WorldObjectManager = WOM;
            _NPCManager = NPCM;
            _Content = content;
            _ItemManager = IM;

            _InvenManager.InventoryChanged += HandleInventoryChanged;
            _InvenManager.ItemSelected += HandleItemSelected;
            _InvenManager.ItemCombine += HandleItemCombined;

            //Recipe matches = new Recipes.MatchesRecipe();
            //RecipeList.Add(matches);

            //Recipe DoubleLog = new Recipes.DoubleLogRecipe();
            //RecipeList.Add(DoubleLog);

            //Recipe fishStick = new Recipes.FishStickRecipe();

            //RecipeList.Add(fishStick);
        }

        public void LoadRecipes(string path)
        {

            var file = System.IO.File.ReadAllText(path);
            RecipeList = JsonConvert.DeserializeObject<List<Recipe>>(file);
        }

        public void LoadIcons()
        {
            foreach (Recipe recipe in RecipeList)
            {
                recipe.RecipeTexture = _ItemManager.GetTexture(_ItemManager.GetItem(recipe.outputID)._Name.Replace(" ", "") +"Item");
            }
        }

        public void AttachEvents(EventManager em)
        {
            ItemCraftedEvent += em.HandleEvent;
        }

        public void CheckRecipes()
        {
            bool dirty = false;
            foreach(Recipe recipe in RecipeList)
            {
                bool itemsFound = false;
                foreach(Ingredient slot in recipe.ingredients)
                {
                    int amt = _InvenManager.getItemCount(slot._ItemID);
                    if(amt >= slot.Amount)
                    {
                        itemsFound = true;
                    }
                    else
                    {
                        itemsFound = false;
                        break;
                    }
                }
                bool recipeInList = ActiveRecipes.Contains(recipe);
                if (itemsFound) //items needed found
                {
                    if(!recipeInList) // items needed found and the recipe isn't in the active list.
                    {
                        ActiveRecipes.Add(recipe);
                        dirty = true;
                    }
                } 
                else //items not found, if recipe in active list remove it
                {
                    if(recipeInList)
                    {
                        ActiveRecipes.Remove(recipe);
                        dirty = true;
                    }
                }
            }
            if(dirty)
            {
                OnRecipesChanged();
            }
        }

        public void ProcessRecipe(Recipe recipe)
        {
            //make sure the recipe is active...

            Recipe theRecipe = ActiveRecipes.Find(x => x == recipe);
            if(theRecipe != null)
            {
                foreach(Ingredient slot in recipe.ingredients)
                {
                    _InvenManager.RemoveItem(slot._ItemID, slot.Amount);                    
                }
                _InvenManager.AddItem(recipe.outputID, recipe.amount);
                OnItemCrafted(recipe.Name);
                this.CheckRecipes();
            }
            else
            {
                Console.WriteLine("Null recipe:" + recipe.Name);
            }
        }

        public void HandleInventoryChanged(object sender, EventArgs args)
        {
            CheckRecipes();
        }

        public void HandleItemSelected(object sender, EventArgs args)
        {
            selectedItem = _InvenManager.selectedItem;
        }

        public void HandleItemCombined(object sender, EventArgs args)
        {
            Console.WriteLine(selectedItem._Name + " used on: " + _InvenManager.secondItem._Name);
            selectedItem = null;
        }

        private void OnRecipesChanged()
        {
            RecipesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnItemCrafted(string itemName)
        {
            ItemCraftedEvent?.Invoke(Enums.EventTypes.kEventItemCrafted, itemName);
        }
    }
}
