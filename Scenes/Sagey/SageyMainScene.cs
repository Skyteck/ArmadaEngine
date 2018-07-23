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
using ArmadaEngine.GameObjects;
namespace ArmadaEngine.Scenes.Sagey
{
    class SageyMainScene : Scene
    {

        public GameObjects.Player player;

        //public Camera  camera;
        TestCamera _TestCamera;

        SpriteFont font;
        bool typingMode = false;
        //public KbHandler kbHandler;

        //Managers
        InventoryManager _InvenManager;
        TileMaps.TilemapManager _MapManager;
        public NPCManager _NPCManager;
        WorldObjectManager _WorldObjectManager;
        UI.UIManager _UIManager;
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


        public SageyMainScene(ContentManager c, SceneManager sm, TestCamera ca) : base(c, sm, ca)
        {

            player = new GameObjects.Player();
            _UIManager = new UI.UIManager();
            _QuestManager = new QuestManager();
            _MapManager = new  TileMaps.TilemapManager();
            _DialogManager = new Managers.DialogManager(_QuestManager);
            _EventManager = new EventManager(_QuestManager);
            _ItemManager = new Managers.ItemManager(_Content);
            _InvenManager = new Managers.InventoryManager(_ItemManager);
            _BankManager = new Managers.BankManager(_ItemManager);
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


    }
}
