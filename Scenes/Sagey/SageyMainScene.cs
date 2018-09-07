using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmadaEngine.Camera;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ArmadaEngine.Scenes.Sagey.Managers;
using Microsoft.Xna.Framework;
using ArmadaEngine.BaseObjects;
using Newtonsoft.Json;
using ArmadaEngine.TileMaps;
using TiledSharp;
using Microsoft.Xna.Framework.Input;
using ArmadaEngine.Helpers;
namespace ArmadaEngine.Scenes.Sagey
{
    class SageyMainScene : Scene
    {

        public GameObjects.Player player;
        
        SpriteFont font;
        bool typingMode = false;
        //public KbHandler kbHandler;

        //Managers
        InventoryManager _InvenManager;
        TileMaps.TilemapManager _MapManager;
        public NPCManager _NPCManager;
        WorldObjectManager _WorldObjectManager;
        ArmadaEngine.UI.UIManager _UIManager;
        ChemistryManager _ChemistryManager;
        ItemManager _ItemManager;
        PlayerManager _PlayerManager;
        BankManager _BankManager;
        GatherableManager _GatherableManager;
        DialogManager _DialogManager;
        QuestManager _QuestManager;
        EventManager _EventManager;

        //UI        
        Vector2 mouseClickPos;
        Sprite mouseCursor;
        Texture2D _SelectTex;
        Sprite _SelectedSprite = null;
        bool BankMode = false;


        const int TargetWidth = 480;
        const int TargetHeight = 270;
        Matrix Scale;

        int effectIndex = 0;
        List<Effect> effectsList = new List<Effect>();
        Effect inverse;
        Effect virtualBoy;
        Effect brightness;
        float brightnessIntensity = 0.5f;
        Effect ColorFun;
        float ColorFunMode = 0.1f;


        bool shaderOn = false;
        public SageyMainScene(ContentManager c, SceneManager sm, ArmadaCamera ca) : base(c, sm, ca)
        {
            this._Name = "Sagey";
            _Content.RootDirectory = "Content/Scenes/Sagey";
            player = new GameObjects.Player();
            _UIManager = new ArmadaEngine.UI.UIManager(_Content);
            _QuestManager = new QuestManager();
            _MapManager = new  TileMaps.TilemapManager();
            _DialogManager = new Managers.DialogManager(_QuestManager);
            _EventManager = new EventManager(_QuestManager);
            _ItemManager = new Managers.ItemManager(_Content);
            _InvenManager = new Managers.InventoryManager(_ItemManager);
            _BankManager = new Managers.BankManager(_ItemManager, _InvenManager);
            _WorldObjectManager = new Managers.WorldObjectManager(_MapManager, _InvenManager, _Content, player, _ItemManager);
            _NPCManager = new Managers.NPCManager(_MapManager, _Content, player, _DialogManager, _InvenManager, _WorldObjectManager);
            _GatherableManager = new Managers.GatherableManager(_MapManager, _InvenManager, _Content, player);
            _ChemistryManager = new Managers.ChemistryManager(_InvenManager, _WorldObjectManager, _NPCManager, _Content, _ItemManager);

            _PlayerManager = new Managers.PlayerManager(player, _InvenManager, _WorldObjectManager, _NPCManager, _MapManager, _GatherableManager);
            _WorldObjectManager.SetGatherManager(_GatherableManager);
            //kbHandler = new KbHandler();

            _SelectedSprite = new Sprite();

            //InputHelper.Init();

            //_TestCamera = new TestCamera(GraphicsDevice);

            //EVENTS
            _DialogManager.BankOpened += HandleBankOpened;
            _PlayerManager.BankOpened += HandleBankOpened;
            _PlayerManager.PlayerMoved += HandlePlayerMoved;

            _BankManager.AttachEvents(_EventManager);
            _NPCManager.AttachEvents(_EventManager);
            _ChemistryManager.AttachEvents(_EventManager);
            _WorldObjectManager.AttachEvents(_EventManager);
            _GatherableManager.AttachEvents(_EventManager);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            player.LoadContent("Art/Player", _Content);
            _MapManager.LoadMap("0-0", _Content);

            LoadMapNPCs(_MapManager.findMapByName("0-0"));

            LoadMapObjects(_MapManager.findMapByName("0-0"));

            //_MapManager.LoadMap("0-1", _Content);
            font = _Content.Load<SpriteFont>("Fonts/Fipps");
            _ItemManager.LoadItems("Content/Scenes/Sagey/JSON/ItemList.json");
            _ChemistryManager.LoadRecipes("Content/Scenes/Sagey/JSON/RecipeList.json");
            _ChemistryManager.LoadIcons();
            _GatherableManager.LoadContent(_Content);

            _SelectTex = _Content.Load<Texture2D>("Art/WhiteTexture");
            _InvenManager.AddItem(Enums.ItemID.kItemFish, 1500);
            _InvenManager.AddItem(Enums.ItemID.kItemStrawberry, 1000000);
            _InvenManager.AddItem(Enums.ItemID.kItemSlimeGoo, 999999999);
            _InvenManager.AddItem(Enums.ItemID.kItemBucket, 3);
            _BankManager.AddItem(Enums.ItemID.kItemOre, 5);
            _BankManager.AddItem(Enums.ItemID.kItemBucket, 4);

            _BankManager.AddItem(Enums.ItemID.kItemFish, 1500);
            _BankManager.AddItem(Enums.ItemID.kItemStrawberry, 1000000);
            _BankManager.AddItem(Enums.ItemID.kItemSlimeGoo, 999999999);
            _BankManager.AddItem(Enums.ItemID.kItemBucket, 3);
            LoadGUI();
            //check if save exists

            ////else start a new save
            //bool saveExist = false;
            string path = _Content.RootDirectory + @"\Save\save.txt";
            //if (System.IO.File.Exists(path))
            //{
            //    saveExist = true;
            //}
            //else
            //{
            //    System.IO.Directory.CreateDirectory(_Content.RootDirectory + @"\Save\");
            //}
            //if (saveExist)
            //{
            //    //load
            //    System.IO.StreamReader file = new System.IO.StreamReader(path);
            //    string line = file.ReadLine();
            //    if (line != null)
            //    {
            //        string[] playerPos = line.Split(' ');
            //        float.TryParse(playerPos[0], out var x);
            //        float.TryParse(playerPos[1], out var y);
            //        _PlayerManager.SetPosition(x, y);
            //    }
            //    bool bankMode = false;
            //    bool inventoryMode = false;
            //    while ((line = file.ReadLine()) != null)
            //    {
            //        if (line.Equals("B"))
            //        {
            //            bankMode = true;
            //            continue;
            //        }
            //        else if (line.Equals("BEnd"))
            //        {
            //            bankMode = false;
            //            continue;
            //        }
            //        if (line.Equals("I"))
            //        {
            //            inventoryMode = true;
            //            continue;
            //        }
            //        else if (line.Equals("IEnd"))
            //        {
            //            inventoryMode = false;
            //            continue;
            //        }
            //        if (bankMode || inventoryMode)
            //        {
            //            string[] items = line.Split(' ');
            //            Int32.TryParse(items[0], out int itemType);
            //            Enums.ItemID type = (Enums.ItemID)itemType;
            //            Int32.TryParse(items[1], out int amt);
            //            if (bankMode)
            //            {
            //                _BankManager.AddItem(type, amt);
            //            }
            //            else if (inventoryMode)
            //            {
            //                _InvenManager.AddItem(type, amt);
            //            }
            //        }

            //    }

            //    file.Close();
            //}
            //else
            //{
            //    player._Position = new Vector2(32, 320);

            //    _InvenManager.AddItem(Enums.ItemID.kItemLog, 5);
            //    _InvenManager.AddItem(Enums.ItemID.kItemMatches);
            //    _InvenManager.AddItem(Enums.ItemID.kItemFish, 2);
            //    _InvenManager.AddItem(Enums.ItemID.kItemMilk, 1);
            //    _InvenManager.AddItem(Enums.ItemID.kItemBucket, 3);

            //    _BankManager.AddItem(Enums.ItemID.kItemLog, 10);
            //    _BankManager.AddItem(Enums.ItemID.kItemFish, 3);
            //}

            _QuestManager.GenerateQuest();
            //List<Dialog> dList = new List<Dialog>();

            //Dialog d1 = new Dialog();
            //d1.ID = "NPC1";
            //d1.textList.Add("I'm a talking slime!");
            //d1.textList.Add("y = mx + b");
            //d1.textList.Add("Do you know the muffin pan?");
            //DialogOption option = new DialogOption();
            //option.NextMsgID = "muffinYes";
            //option.optiontext = "I think it's muffin pan though.";
            //d1.options.Add(option);
            //option = new DialogOption();
            //option.NextMsgID = "muffinNo";
            //option.optiontext = "That's not how the story goes...";
            //d1.options.Add(option);
            //dList.Add(d1);

            //Dialog d2 = new Dialog();
            //d2.ID = "MightyDucks";
            //d2.textList.Add("I love Mike!");
            //dList.Add(d2);
            //List <Dialog> list2 = new List<Dialog>();

            //List<Recipe> rList = new List<Recipe>();
            //Recipe matches = new Recipes.MatchesRecipe();
            //Recipe DoubleLog = new Recipes.DoubleLogRecipe();
            //Recipe fishStick = new Recipes.FishStickRecipe();

            //rList.Add(matches);
            //rList.Add(DoubleLog);
            //rList.Add(fishStick);



            string text = JsonConvert.SerializeObject(_QuestManager.GetActiveQuests(), Newtonsoft.Json.Formatting.Indented);

            path = _Content.RootDirectory + @"\JSON\Dialog_EN_US.json";
            _DialogManager.LoadDialog(path);

            _Content.RootDirectory = "Content/Scenes/Stest";
            inverse = _Content.Load<Effect>("Inverse");
            virtualBoy = _Content.Load<Effect>("VirtualBoy");
            brightness = _Content.Load<Effect>("Brightness");
            ColorFun = _Content.Load<Effect>("ColorFun");
            effectsList.Add(inverse);
            effectsList.Add(virtualBoy);
            effectsList.Add(brightness);
            effectsList.Add(ColorFun);
            _Content.RootDirectory = "Content/Scenes/Sagey";

            //_DialogManager.PlayMessage("NPC1");
            //var dialog = System.IO.File.ReadAllText(path);
            //list2 = JsonConvert.DeserializeObject<List<Dialog>>(dialog);
        }

        private void LoadGUI()
        {
            UI.InventoryPanel inv = new UI.InventoryPanel(_UIManager, _InvenManager);
            inv.LoadContent("Panel");
            inv.SetSize(new Vector2(300, 300));
            inv.Setup();
            inv.HidePanel();
            inv.SetPosition(new Vector2(400, 100));
            _UIManager.AddPanel(inv);

            UI.BankPanel bnkp = new UI.BankPanel(_UIManager, _BankManager);
            bnkp.LoadContent("Panel");
            bnkp.SetSize(new Vector2(300, 300));
            bnkp.Setup();
            bnkp.ShowPanel();
            bnkp.SetPosition(new Vector2(0, 0));
            _UIManager.AddPanel(bnkp);
        }

        public void LoadMapNPCs(TileMap testMap)
        {
            TmxList<TmxObject> ObjectList = testMap.FindNPCs();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    int adjustThingX = (int)(thing.X + (testMap._Postion.X + (thing.Width / 2)));
                    int adjustThingY = (int)(thing.Y + (testMap._Postion.Y + (thing.Height / 2)));
                    _NPCManager.CreateNPC(thing, new Vector2(adjustThingX, adjustThingY));
                }
            }
        }

        public void LoadMapObjects(TileMap testMap)
        {
            TmxList<TmxObject> ObjectList = testMap.FindObjects();
            if (ObjectList != null)
            {
                foreach (TmxObject thing in ObjectList)
                {
                    Vector2 newPos = new Vector2((int)thing.X + testMap._Postion.X, (int)thing.Y + testMap._Postion.Y);
                    //_WorldObjectManager.CreateObject(thing, newPos);
                    if (thing.Type == "Dirt")
                    {
                        _WorldObjectManager.CreateObject(thing, newPos);
                    }
                    else
                    {
                        _GatherableManager.CreateGatherable(thing, newPos);

                    }
                }
            }
        }

        public override void Update(GameTime gt)
        {

            base.Update(gt);
            _EventManager.ProcessEvents();

            //ProcessMouse(gt);
            ProcessKeyboard(gt);


            if (InputHelper.IsKeyPressed(Keys.J))
            {
                _BankManager.AddItem(Enums.ItemID.kItemBucket);
            }

            //if (typingMode && !kbHandler.typingMode) //ugly, but should show that input mode ended...?
            //{
            //    processor.Parsetext(kbHandler.Input);
            //    if (processor.currentError != string.Empty) kbHandler.Input = processor.currentError;
            //    //kbHandler.Input = string.Empty;
            //}
            _PlayerManager.Update(gt);
            _SelectedSprite = null;
            if (_PlayerManager._FrontSprite != null)
            {
                _SelectedSprite = _PlayerManager._FrontSprite;
            }

            _NPCManager.UpdateNPCs(gt);
            _WorldObjectManager.Update(gt);
            _GatherableManager.Update(gt);


            //ProcessCamera(gameTime);
            //_Camera.SetPosition(player._Position);
            _Camera._Position = player._Position;
            _UIManager.Update(gt);
            


        }

        private void ProcessMouse(object gameTime)
        {
            //first check UI

        }

        private void ProcessKeyboard(GameTime gameTime)
        {
            if (InputHelper.IsKeyPressed(Keys.E))
            {
                _UIManager.TogglePanel("Bank");
            }
            if (InputHelper.IsKeyPressed(Keys.I))
            {
                _UIManager.TogglePanel("Inventory");
            }
            if (InputHelper.IsKeyPressed(Keys.Escape))
            {
                _UIManager.HideAll();
            }
            if(InputHelper.IsKeyPressed(Keys.F6))
            {
                shaderOn = !shaderOn;
            }
            if(InputHelper.IsKeyPressed(Keys.F7))
            {
                effectIndex++;
                if(effectIndex == effectsList.Count)
                {
                    effectIndex = 0;
                }
            }

            if (InputHelper.IsKeyPressed(Keys.F9))
            {
                brightnessIntensity += 0.1f;
                if (brightnessIntensity > 1.0f)
                {
                    brightnessIntensity = 1.0f;
                }
            }
            else if (InputHelper.IsKeyPressed(Keys.F8))
            {
                brightnessIntensity -= 0.1f;
                if (brightnessIntensity < -1.0f)
                {
                    brightnessIntensity = -1.0f;
                }
            }

            if (InputHelper.IsKeyPressed(Keys.F10))
            {
                if(ColorFunMode == 0.1f)
                {
                    ColorFunMode = 0.2f;
                }
                else
                {
                    ColorFunMode = 0.1f;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle b)
        {
            base.Draw(spriteBatch, b);


            spriteBatch.Begin(SpriteSortMode.Immediate,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        _Camera.GetTransform());

            if (shaderOn)
            {
                effectsList[effectIndex].Techniques[0].Passes[0].Apply();
                if(effectsList[effectIndex].Name == "Brightness")
                {
                    effectsList[effectIndex].Parameters["Intensity"].SetValue(brightnessIntensity);
                }
                else if (effectsList[effectIndex].Name == "ColorFun")
                {
                    effectsList[effectIndex].Parameters["Mode"].SetValue(ColorFunMode);
                }
            }

            _MapManager.Draw(spriteBatch, _Camera._Viewport);

            _PlayerManager.Draw(spriteBatch);

            _NPCManager.DrawNPCs(spriteBatch);

            _WorldObjectManager.Draw(spriteBatch);
            _GatherableManager.Draw(spriteBatch);

            //Vector2 invenBgpos = _UIManager.getUIElement("Inventory")._TopLeft;
            //_InvenManager.Draw(spriteBatch, invenBgpos);

            //mouseCursor.Draw(spriteBatch);
            DrawSelectRect(spriteBatch);
            //spriteBatch.DrawString(font, kbHandler.Input, camera.ToWorld(new Vector2(100, 100)), Color.Black);
            //spriteBatch.DrawString(font, player._HP.ToString(), camera.ToWorld(new Vector2(200, 200)), Color.White);

            //base.Draw(gt);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate);
            if (shaderOn)
            {
                effectsList[effectIndex].Techniques[0].Passes[0].Apply();
            }
            _UIManager.Draw(spriteBatch);
            spriteBatch.DrawString(font, _PlayerManager.currentInteracttext, new Vector2(100, 100), Color.White);
            spriteBatch.End();
        }

        private void DrawSelectRect(SpriteBatch sb)
        {
            int border = 3;
            Rectangle rect;
            if (_SelectedSprite != null)
            {
                rect = _SelectedSprite._BoundingBox;
                sb.Draw(_SelectTex, new Rectangle(rect.X, rect.Y, border, rect.Height + border), Color.White);
                sb.Draw(_SelectTex, new Rectangle(rect.X, rect.Y, rect.Width + border, border), Color.White);
                sb.Draw(_SelectTex, new Rectangle(rect.X + rect.Width, rect.Y, border, rect.Height + border), Color.White);
                sb.Draw(_SelectTex, new Rectangle(rect.X, rect.Y + rect.Height, rect.Width + border, border), Color.White);
            }

            rect = _PlayerManager.CheckRect;
            sb.Draw(_SelectTex, new Rectangle(rect.X, rect.Y, border, rect.Height + border), Color.White);
            sb.Draw(_SelectTex, new Rectangle(rect.X, rect.Y, rect.Width + border, border), Color.White);
            sb.Draw(_SelectTex, new Rectangle(rect.X + rect.Width, rect.Y, border, rect.Height + border), Color.White);
            sb.Draw(_SelectTex, new Rectangle(rect.X, rect.Y + rect.Height, rect.Width + border, border), Color.White);
        }

        public void HandleBankOpened(object sender, EventArgs args)
        {
            BankMode = true;
            _UIManager.ShowPanel("Bank");
        }
        private void HandlePlayerMoved(object sender, EventArgs args)
        {
            BankMode = false;
            _UIManager.HidePanel("Bank");
        }
    }
}
