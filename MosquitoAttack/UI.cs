using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosquitoAttack
{
    class UI
    {
        private bool isPlayerDie;
        private int _scale;
        private string[] menuTexts = {
            "Start",
            "Option",
            "Quit",
        };

        private string[] SettingTexts = {
            "Difficulty",
            "Size",
            "Developer Model"
        };
        
        private string[] DifficultModel = {
            "-- Easy    --",
            "-- Normal  --",
            "-- Hardest --"
        };


        private string[] SizeModelTexts = {
            //"-- Small    --",
            "-- Normal --",
            "-- Normal --",
            "-- Normal --"
            //"-- Bigest --"
        };

        private string[] DeveloperModelTexts = {
            "-- ON  --",
            "-- OFF --"
        };

        private int chosenOption = 0;
        private int difficultChosen = 1;
        private int sizeModelChosen = 1;
        private int developerModelChosen = 0;
        private int chosenSettingOption = 0;
        Color MenuOneColor = Color.Red;
        Color MenuTwoColor = Color.White;
        Color MenuThreeColor = Color.White;

        Color settingOneColor = Color.Red;
        Color settingTwoColor = Color.White;
        Color settingThreeColor = Color.White;

        private float menuOneScale;
        private float menuTwoScale;
        private float menuThreeScale;

        private float settingOneScale;
        private float settingTwoScale;
        private float settingThreeScale;
        Vector2 stringSpace = new Vector2(0, 30);
        private Vector2[] menuListPosition = null;
        private Vector2[] settingListPosition = null;
        public int PlayerLife { get; set; }
        public int CurrentPlayerLife { get; set; }
        public int EnemyNum { get; set; }
        public int bulletNumState { get; set; }
        public int currentBulletNum { get; set; }

        private string currentBulletMsg;
        public Texture2D ObjectSpanTexture { get; set; }

        private Rectangle lifeSpanRect = new Rectangle();
        private Point lifeSpanStartPosition = new Point(26, 387);
        private int lifeSpanWidth = 62;
        private int lifeSpanHeight = 8;
        private Rectangle currentLifeSpanRect = new Rectangle();
        private Rectangle totalLifeSpanRect = new Rectangle();
        private Vector2 lifeDipslayPosition = new Vector2();

        private Vector2 BulletNumStatePosition = new Vector2();

        private Vector2 CannonPosition = new Vector2();
        private float reloadAlertTimer = 2.0f;
        private float reloadAlertDuring = 1.0f;
        private float decreaseReloadBarTimer;
        private string reloadAlertMsg = "";
        public bool ReloadActive = false;
        public bool ReloadActiveTwo = false;
        public bool ReloadActiveThree = false;
        private int decreaseReloadBar;
        private Color decreaseBarColor = Color.Green*1;

        private Rectangle changeSceneRectZero;
        private int autoIncreasePixcelZero = 0;
        private Rectangle changeSceneRectOneUp;
        private Rectangle changeSceneRectOneDown;
        private Rectangle changeSceneRectTwoLeft;
        private Rectangle changeSceneRectTwoRight;
        private int preJumpModel;
        private int jumpModel;
        private float jumpSceneTimer;
        private float jumpSceneDuring;
        private string currentLevelStr;
        private Vector2 currentLevelStrPosition;

        public int currentAttackModel { get; set; }

        public int CurrentLevel { get; set; }


        private bool levelOneActive;
        private bool levelTwoActive;
        public bool LevelTwoInitial { get; set; }
        private bool levelThreeActive;
        public bool levelThreeInitial { get; set; }
        private bool endCurrentLevel;


        public bool JumpSceneActive { get; set; }
        private bool CloseSceneActive;

        private Vector2 numOfEnemyPosition = new Vector2();
        public int NumDestroyEnemy { get; set; }


        private Vector2 cannonPosition;
        private Vector2 barrelPosition;


        private Vector2 AttackOneIconPosition;
        private Texture2D AttackOneIconTexture;
        private Vector2 AttackTwoIconPosition;
        private Texture2D AttackTwoIconTexture;
        private Vector2 AttackThreeIconPosition;
        private Texture2D AttackThreeIconTexture;
        private Vector2 AttackFourIconPosition;
        private Texture2D AttackFourIconTexture;
        private int AttackModelIconLength;
        private int AttackModelIconGap;
        private int currentScale_1 = 0;
        private int currentScale_2 = 0;
        private int currentScale_3 = 0;
        private int shadowHeightModelOne;
        private int shadowHeightModelTwo;
        private int shadowHeightModelThree;
        private int shadowHeightModelFour;
        private int bulletLeftModelOne;
        private int totalBulletModelOne;
        private int bulletLeftModelTwo;
        private int totalBulletModelTwo;
        private int bulletLeftModelThree;
        private int totalBulletModelThree;
        private Color attackModelOneColor;
        private Color attackModelTwoColor;
        private Color attackModelThreeColor;
        private Color shiledIconColor = Color.Green;

        public int Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public float RecreaseReloadBarTimer { get => decreaseReloadBarTimer; set => decreaseReloadBarTimer = value; }
        public int ChosenOption { get => chosenOption; set => chosenOption = value; }
        public bool LevelOneActive { get => levelOneActive; set => levelOneActive = value; }
        public bool LevelTwoActive { get => levelTwoActive; set => levelTwoActive = value; }
        public bool LevelThreeActive { get => levelThreeActive; set => levelThreeActive = value; }
        public bool EndCurrentLevel { get => endCurrentLevel; set => endCurrentLevel = value; }
        public float ReloadAlertTimer { get => reloadAlertTimer; set => reloadAlertTimer = value; }
        public bool IsPlayerDie { get => isPlayerDie; set => isPlayerDie = value; }
        public float JumpSceneTimer { get => jumpSceneTimer; set => jumpSceneTimer = value; }
        public bool GameOver { get => gameOver; set => gameOver = value; }
        public bool IsPaused { get; internal set; }
        public int ChosenSettingOption { get => chosenSettingOption; set => chosenSettingOption = value; }
        public int DifficultChosen { get => difficultChosen; set => difficultChosen = value; }
        public int SizeModelChosen { get => sizeModelChosen; set => sizeModelChosen = value; }
        public int DeveloperModelChosen { get => developerModelChosen; set => developerModelChosen = value; }

        private SpriteFont arial;
        private int finalLevel;
        private bool gameOver;
        private string pausedStr;
        internal bool settingActive;

        public void LoadContent(SpriteFont font) {
            this.arial = font;
        }

        public void Initial(int scale, int playerLife, int currentLife ,int enemyNum, int bulletNumState) {
            this._scale = scale;
            this.PlayerLife = playerLife;
            this.CurrentPlayerLife = currentLife;
            this.EnemyNum = enemyNum;
            this.bulletNumState = bulletNumState;
            this.currentBulletNum = bulletNumState;
            lifeSpanRect = new Rectangle(lifeSpanStartPosition.X * scale, lifeSpanStartPosition.Y * scale, lifeSpanWidth * scale, lifeSpanHeight * scale);
            currentLifeSpanRect = new Rectangle((lifeSpanStartPosition.X + 1) * scale, (lifeSpanStartPosition.Y + 1) * scale, (lifeSpanWidth - 2) / PlayerLife * CurrentPlayerLife * scale, (lifeSpanHeight - 2) * scale);
            totalLifeSpanRect = new Rectangle((lifeSpanStartPosition.X+1) * scale, (lifeSpanStartPosition.Y+1) * scale, (lifeSpanWidth-2) * scale, (lifeSpanHeight-2) * scale);
            BulletNumStatePosition = new Vector2(currentLifeSpanRect.Right + 20*Scale, currentLifeSpanRect.Y);
            numOfEnemyPosition = new Vector2(10 * scale, 10 * scale);
            lifeDipslayPosition = new Vector2(lifeSpanRect.X + 20 * Scale, lifeSpanRect.Y + 1 * Scale);
            decreaseReloadBarTimer = 10.0f;
            menuOneScale = scale*1.2f;
            menuTwoScale = scale;
            menuThreeScale = scale;
            settingOneScale = scale/2.0f*1.2f;
            settingTwoScale = scale/2.0f;
            settingThreeScale = scale/2.0f;

            autoIncreasePixcelZero = 0;
            changeSceneRectZero = new Rectangle((275 - autoIncreasePixcelZero) * _scale, (200 - autoIncreasePixcelZero) * _scale, (1 + autoIncreasePixcelZero * 2) * _scale, (1 + autoIncreasePixcelZero * 2) * _scale);
            changeSceneRectOneUp = new Rectangle(0, 0, 550*_scale, 200 * _scale);
            changeSceneRectOneDown = new Rectangle(0, 200 * _scale, 550 * _scale, 200 * _scale);
            changeSceneRectTwoLeft = new Rectangle(0, 0, 275 * _scale, 400 * _scale);
            changeSceneRectTwoRight = new Rectangle(275 * _scale, 0, 275 * _scale, 400 * _scale);
            jumpModel = -5;
            JumpSceneActive = false;
            IsPlayerDie = false;

            chosenOption = 0;
            chosenSettingOption = 0;
            MenuOneColor = Color.Red;
            MenuTwoColor = Color.White;
            MenuThreeColor = Color.White;
            reloadAlertMsg = "";
            reloadAlertTimer = 2.0f;
            reloadAlertDuring = 1.0f;
            ReloadActive = false;
            ReloadActiveTwo = false;
            ReloadActiveThree = false;

            decreaseBarColor = Color.Green * 1;
            autoIncreasePixcelZero = 0;
            currentScale_1 = 0;
            currentScale_2 = 0;
            currentScale_3 = 0;

            this.LevelOneActive = false;
            this.LevelTwoActive = false;
            this.LevelThreeActive = false;
            this.EndCurrentLevel = false;
            this.LevelTwoInitial = false;
            this.levelThreeInitial = false;
            this.JumpSceneTimer = 10.0f;
            this.jumpSceneDuring = 4.85f*2/3.0f;
            this.currentLevelStr = "";
            currentLevelStrPosition = new Vector2(200, 150) * scale;
            CurrentLevel = 1;

            NumDestroyEnemy = 0;
            AttackModelIconLength = 10 * scale;
            AttackModelIconGap = 2 * scale;
            AttackOneIconPosition = new Vector2(0, 220) ;
            AttackTwoIconPosition = AttackOneIconPosition + new Vector2(0, AttackModelIconLength + AttackModelIconGap);
            AttackThreeIconPosition = AttackTwoIconPosition + new Vector2(0, AttackModelIconLength + AttackModelIconGap);

            AttackFourIconPosition = AttackThreeIconPosition + new Vector2(0, AttackModelIconLength + AttackModelIconGap);
            bulletLeftModelOne =0;
            bulletLeftModelTwo = 0;
            bulletLeftModelThree = 0;

            attackModelOneColor = Color.DarkSlateGray;
            attackModelTwoColor = Color.DarkSlateGray;
            attackModelThreeColor = Color.DarkSlateGray;

            currentBulletMsg = "Attack Moedel One:";

            this.finalLevel = 2;
            this.GameOver = false;
            IsPaused = false;

            pausedStr = "";
            settingActive = false;

            settingOneColor = Color.Red;
            settingTwoColor = Color.White;
            settingThreeColor = Color.White;

            DifficultChosen = 1;
            SizeModelChosen = 1;
            DeveloperModelChosen = 1;
        }

        public void SetMenuPosition(Vector2 menuPostion) {
            this.menuListPosition = new Vector2[] {
                menuPostion,
                menuPostion + stringSpace * Scale,
                menuPostion + stringSpace*2*Scale
            };
        }

        public void SetSettingMenuPosition(Vector2 menuPostion)
        {
            this.settingListPosition = new Vector2[] {
                menuPostion,
                menuPostion + stringSpace * Scale/2,
                menuPostion + stringSpace*2*Scale/2
            };
        }

        public void SetAttackModelIconTexture(Texture2D modelOne, Texture2D modelTwo, Texture2D modelThree, Texture2D modelFour) {
            this.AttackOneIconTexture = modelOne;
            this.AttackTwoIconTexture = modelTwo;
            this.AttackThreeIconTexture = modelThree;
            this.AttackFourIconTexture = modelFour;
        }

        //public bool Update(GameTime gameTime) {
        //    return JunpScene();
        //}

        private int JunpScene() {
            switch (jumpModel)
            {
                case 0:
                    if (autoIncreasePixcelZero <= 275)
                    {
                        changeSceneRectZero = new Rectangle((275 - autoIncreasePixcelZero) * _scale, (200 - autoIncreasePixcelZero) * _scale, (1 + autoIncreasePixcelZero * 2) * _scale, (int)(1 + autoIncreasePixcelZero * 2) * _scale);
                    }
                    else
                    {
                        return 1;
                    }
                    break;
                case 1:
                    if (autoIncreasePixcelZero <= 200)
                    {
                        changeSceneRectOneUp = new Rectangle(0 * _scale, 0 * _scale, 550 * _scale, (200 - autoIncreasePixcelZero) * _scale);
                        changeSceneRectOneDown = new Rectangle(0 * _scale, (200 + autoIncreasePixcelZero) * _scale, 550 * _scale, (200 - autoIncreasePixcelZero) * _scale);
                    }
                    else
                    {
                        return 1;
                    }
                    break;
                case 2:
                    if (autoIncreasePixcelZero <= 275)
                    {
                        changeSceneRectTwoLeft = new Rectangle(0 * _scale, 0 * _scale, (275 - autoIncreasePixcelZero) * _scale, 400 * _scale);
                        changeSceneRectTwoRight = new Rectangle((275 + autoIncreasePixcelZero) * _scale, 0 * _scale, (275 - autoIncreasePixcelZero) * _scale, 400 * _scale);
                    }
                    else
                    {
                        return 1;
                    }
                    break;
                case 3:
                    if (autoIncreasePixcelZero >=0)
                    {
                        changeSceneRectZero = new Rectangle((275 - autoIncreasePixcelZero) * _scale, (200 - autoIncreasePixcelZero) * _scale, (1 + autoIncreasePixcelZero * 2) * _scale, (int)(1 + autoIncreasePixcelZero * 2) * _scale);
                    }
                    else
                    {
                        return 2;
                    }
                    break;
                case 4:
                    if (autoIncreasePixcelZero>=0)
                    {
                        changeSceneRectOneUp = new Rectangle(0 * _scale, 0 * _scale, 550 * _scale, (200 - autoIncreasePixcelZero) * _scale);
                        changeSceneRectOneDown = new Rectangle(0 * _scale, (200 + autoIncreasePixcelZero) * _scale, 550 * _scale, (200 - autoIncreasePixcelZero) * _scale);
                    }
                    else
                    {
                        return 2;
                    }
                    break;
                case 5:
                    if (autoIncreasePixcelZero >=0)
                    {
                        changeSceneRectTwoLeft = new Rectangle(0 * _scale, 0 * _scale, (275 - autoIncreasePixcelZero) * _scale, 400 * _scale);
                        changeSceneRectTwoRight = new Rectangle((275 + autoIncreasePixcelZero) * _scale, 0 * _scale, (275 - autoIncreasePixcelZero) * _scale, 400 * _scale);
                    }
                    else
                    {
                        return 2;
                    }
                    break;
            }
            if (jumpModel == 0 || jumpModel == 1 || jumpModel == 2)
            {
                autoIncreasePixcelZero++;
                //autoIncreasePixcelZero+2;
            }
            else if (jumpModel == 3 || jumpModel == 4 || jumpModel == 5) {
                //autoIncreasePixcelZero+=2;
                autoIncreasePixcelZero--;
            }
            return 0;
        }

        public void Update(GameTime gameTime, int currentLife, int currentBullet, Vector2 cannonPosition, Vector2 barrelPosition, int numOfEnemy, int shiledLife)
        {
            if (settingActive) { 
            
                
            }


            if (shiledLife > 0)
            {
                shiledIconColor = Color.Green;
            }
            else {
                shiledIconColor = Color.Red;
            }

            //Icon change
            switch (currentAttackModel)
            {
                case 1:
                    currentScale_1 = 2 * Scale;
                    currentScale_2 = -5 * Scale;
                    currentScale_3 = -5 * Scale;
                    attackModelOneColor = Color.DarkGray;
                    attackModelTwoColor = Color.DarkSlateGray;
                    attackModelThreeColor = Color.DarkSlateGray;
                    currentBulletMsg = "Attack Moedel One:";
                    break;
                case 2:
                    currentScale_1 = -5 * Scale;
                    currentScale_2 = 2 * Scale;
                    currentScale_3 = -5 * Scale;
                    attackModelOneColor = Color.DarkSlateGray;
                    attackModelTwoColor = Color.DarkGray;
                    attackModelThreeColor = Color.DarkSlateGray;
                    currentBulletMsg = "Attack Moedel Two:";
                    break;
                case 3:
                    currentScale_1 = -5 * Scale;
                    currentScale_2 = -5 * Scale;
                    currentScale_3 = 2 * Scale;
                    attackModelOneColor = Color.DarkSlateGray;
                    attackModelTwoColor = Color.DarkSlateGray;
                    attackModelThreeColor = Color.DarkGray;
                    currentBulletMsg = "Attack Moedel Three:";
                    break;
            }

            if(currentAttackModel == 1) { 
                shadowHeightModelOne = (AttackModelIconLength * Scale - 2 * Scale + currentScale_1) - (AttackModelIconLength * Scale - 2 * Scale + currentScale_1) * bulletLeftModelOne / totalBulletModelOne ;
                shadowHeightModelTwo = (AttackModelIconLength * Scale - 2 * Scale + currentScale_2);
                shadowHeightModelThree = (AttackModelIconLength * Scale - 2 * Scale + currentScale_3);
            }
            else if(currentAttackModel == 2) { 
                shadowHeightModelTwo = (AttackModelIconLength * Scale - 2 * Scale + currentScale_2) - (AttackModelIconLength * Scale - 2 * Scale + currentScale_2) * bulletLeftModelTwo / totalBulletModelTwo;
                shadowHeightModelOne = (AttackModelIconLength * Scale - 2 * Scale + currentScale_1);
                shadowHeightModelThree = (AttackModelIconLength * Scale - 2 * Scale + currentScale_3);
            }
            else if(currentAttackModel == 3) { 
                shadowHeightModelThree = (AttackModelIconLength * Scale - 2 * Scale + currentScale_3) - (AttackModelIconLength * Scale - 2 * Scale + currentScale_3) * bulletLeftModelThree / totalBulletModelThree ;
                shadowHeightModelOne = (AttackModelIconLength * Scale - 2 * Scale + currentScale_1);
                shadowHeightModelTwo = (AttackModelIconLength * Scale - 2 * Scale + currentScale_2);
            }
            //if (bulletLeftModelOne == 0)
            //{
            //    shadowHeightModelOne = AttackModelIconLength * Scale - 2 * Scale + currentScale_1;
            //}
            //else {
            //    shadowHeightModelOne = 0;
            //}
            //if (bulletLeftModelTwo == 0)
            //{
            //    shadowHeightModelTwo = AttackModelIconLength * Scale - 2 * Scale + currentScale_2;
            //}
            //else {
            //    shadowHeightModelTwo = 0;
            //}

            //if (bulletLeftModelThree == 0)
            //{
            //    shadowHeightModelThree = AttackModelIconLength * Scale - 2 * Scale + currentScale_3;
            //}
            //else {
            //    shadowHeightModelThree = 0;
            //}

            JumpSceneTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (CurrentLevel)
            {
                case 1:
                    LevelOneActive = true;
                    currentLevelStr = "Level One";
                    break;
                case 2:
                    LevelTwoActive = true;
                    LevelOneActive = false;
                    currentLevelStr = "Level Two";
                    break;
                case 3:
                    LevelThreeActive = true;
                    LevelTwoActive = false;
                    currentLevelStr = "Level Three";
                    break;
                default:
                    break;
            }

            if (IsPlayerDie) {
                currentLevelStr = "You are \nDead!!!";
            }

            if (GameOver) {
                currentLevelStr = "You win!!!";
            }

            if (IsPaused) {
                this.pausedStr = "Paused!!";
            }

            if (JumpSceneActive)
            {
                if (JumpSceneTimer > 0.5f)
                {
                    int types = JunpScene();
                    if (types == 1)
                    {
                        JumpSceneActive = false;

                    }
                    else if (types == 2)
                    {
                        //endCurrentLevel = 
                        if (!IsPlayerDie) { 
                            this.CurrentLevel++;
                        }
                        JumpSceneActive = false;
                    }
                }
            }
                       
            ReloadAlertTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            decreaseReloadBarTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            IconAndBarDisplay(decreaseReloadBarTimer);

            CurrentPlayerLife = currentLife;
            this.currentBulletNum = bulletNumState - currentBullet;
            this.CannonPosition = cannonPosition;
            this.barrelPosition = barrelPosition;
            this.EnemyNum = numOfEnemy - NumDestroyEnemy;
            currentLifeSpanRect = new Rectangle((lifeSpanStartPosition.X + 1) * _scale, (lifeSpanStartPosition.Y + 1) * _scale, (lifeSpanWidth - 2) / PlayerLife * CurrentPlayerLife * _scale, (lifeSpanHeight - 2) * _scale);

            if (this.EnemyNum == 0 && !EndCurrentLevel)
            {
                CloseScene();
                EndCurrentLevel = true;
                if (this.CurrentLevel == finalLevel) {
                    this.GameOver = true;
                }
            }

            if (this.CurrentPlayerLife <= 0 && !EndCurrentLevel && !IsPlayerDie) {
                CloseScene();
                IsPlayerDie = true;
            }

            if (decreaseReloadBarTimer > 4.0) { 
                if (currentAttackModel == 1) {
                    bulletLeftModelOne = this.currentBulletNum;
                }
                else if(currentAttackModel == 2)
                {
                    bulletLeftModelTwo = this.currentBulletNum;
                }
                else if (currentAttackModel == 3)
                {
                    bulletLeftModelThree = this.currentBulletNum;
                }
            }
        }

        internal int ChangeOption(int UpOrDown)
        {
            if (UpOrDown == 0)
            {
                if (chosenOption == 0)
                {
                    chosenOption = 2;
                    menuThreeScale = Scale * 1.5f;
                    menuOneScale = Scale;
                    menuTwoScale = Scale;
                    MenuThreeColor = Color.Red;
                    MenuOneColor = Color.White;
                    MenuTwoColor = Color.White;                    
                }
                else if (chosenOption == 1)
                {
                    chosenOption = 0;
                    menuOneScale = Scale * 1.5f;
                    menuThreeScale = Scale;
                    menuTwoScale = Scale;
                    MenuOneColor = Color.Red;
                    MenuThreeColor = Color.White;
                    MenuTwoColor = Color.White;
                }
                else if (chosenOption == 2) {
                    chosenOption = 1;
                    menuTwoScale = Scale * 1.5f;
                    menuOneScale = Scale;
                    menuThreeScale = Scale;
                    MenuTwoColor = Color.Red;
                    MenuOneColor = Color.White;
                    MenuThreeColor = Color.White;
                }
            }
            else if (UpOrDown == 1) 
            {
                if (chosenOption == 0)
                {
                    chosenOption = 1;
                    menuTwoScale = Scale * 1.5f;
                    menuOneScale = Scale;
                    menuThreeScale = Scale;
                    MenuTwoColor = Color.Red;
                    MenuOneColor = Color.White;
                    MenuThreeColor = Color.White;
                }
                else if (chosenOption == 1)
                {
                    chosenOption = 2;
                    menuThreeScale = Scale * 1.5f;
                    menuOneScale = Scale;
                    menuTwoScale = Scale;
                    MenuThreeColor = Color.Red;
                    MenuOneColor = Color.White;
                    MenuTwoColor = Color.White;
                }
                else if (chosenOption == 2)
                {
                    chosenOption = 0;
                    menuOneScale = Scale * 1.5f;
                    menuTwoScale = Scale;
                    menuThreeScale = Scale;
                    MenuOneColor = Color.Red;
                    MenuThreeColor = Color.White;
                    MenuTwoColor = Color.White;
                }
            }
            return chosenOption;
        }

        internal int ChangeSetOption(int UpOrDown)
        {
            if (UpOrDown == 0)
            {
                if (chosenSettingOption  == 0)
                {
                    chosenSettingOption = 2;
                    settingOneScale = Scale / 2.0f;
                    settingTwoScale = Scale / 2.0f;
                    settingThreeScale = Scale / 2.0f * 1.2f;

                    settingThreeColor  = Color.Red;
                    settingTwoColor = Color.White;
                    settingOneColor = Color.White;
                }
                //else if (chosenSettingOption == 1)
                //{
                //    chosenSettingOption = 0;
                //    settingOneScale = Scale / 2.0f * 1.2f;
                //    settingTwoScale = Scale / 2.0f;
                //    settingThreeScale = Scale / 2.0f ;
                //    settingOneColor = Color.Red;
                //    settingTwoColor = Color.White;
                //    settingThreeColor  = Color.White;
                //}
                else if (chosenSettingOption == 2)
                {
                    chosenSettingOption = 0;
                    settingOneScale = Scale / 2.0f ;
                    settingTwoScale = Scale / 2.0f * 1.2f;
                    settingThreeScale = Scale / 2.0f;
                    settingTwoColor  = Color.White;
                    settingOneColor = Color.Red;
                    settingThreeColor = Color.White;
                }
            }
            else if (UpOrDown == 1)
            {
                if (chosenSettingOption == 0)
                {
                    chosenSettingOption = 2;
                    settingOneScale = Scale / 2.0f;
                    settingTwoScale = Scale / 2.0f * 1.2f;
                    settingThreeScale = Scale / 2.0f;
                    settingTwoColor = Color.White;
                    settingOneColor = Color.White;
                    settingThreeColor = Color.Red;
                }
                //else if (chosenSettingOption == 1)
                //{
                //    chosenSettingOption = 2;
                //    settingOneScale = Scale / 2.0f;
                //    settingTwoScale = Scale / 2.0f ;
                //    settingThreeScale = Scale / 2.0f * 1.2f;
                //    settingThreeColor = Color.Red;
                //    settingOneColor = Color.White;
                //    settingTwoColor  = Color.White;
                //}
                else if (chosenSettingOption == 2)
                {
                    chosenSettingOption = 0;
                    settingOneScale = Scale / 2.0f * 1.2f;
                    settingTwoScale = Scale / 2.0f;
                    settingThreeScale = Scale / 2.0f ;
                    settingOneColor  = Color.Red;
                    settingThreeColor = Color.White;
                    settingTwoColor = Color.White;
                }
            }
            return chosenSettingOption;
        }


        public void DrawMenu(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(arial, menuTexts[0], menuListPosition[0], MenuOneColor, 0f, Vector2.Zero, menuOneScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(arial, menuTexts[1], menuListPosition[1], MenuTwoColor, 0f, Vector2.Zero, menuTwoScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(arial, menuTexts[2], menuListPosition[2], MenuThreeColor, 0f, Vector2.Zero, menuThreeScale, SpriteEffects.None, 0f);
        }

        public void DrawSettingMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(arial, SettingTexts[0], settingListPosition[0], settingOneColor, 0f, Vector2.Zero, settingOneScale, SpriteEffects.None, 0f);
            if(chosenSettingOption == 0) { 
                spriteBatch.DrawString(arial, DifficultModel[DifficultChosen], settingListPosition[0] + new Vector2(100 *Scale, 0), settingOneColor, 0f, Vector2.Zero, settingOneScale, SpriteEffects.None, 0f);
            }
            //spriteBatch.DrawString(arial, SettingTexts[1], settingListPosition[1], settingTwoColor, 0f, Vector2.Zero, settingTwoScale, SpriteEffects.None, 0f);
            //if (chosenSettingOption == 1)
            //{
            //    spriteBatch.DrawString(arial, SizeModelTexts[SizeModelChosen], settingListPosition[1] + new Vector2(100 * Scale, 0), settingTwoColor, 0f, Vector2.Zero, settingOneScale, SpriteEffects.None, 0f);
            //}

            spriteBatch.DrawString(arial, SettingTexts[2], settingListPosition[1], settingThreeColor, 0f, Vector2.Zero, settingThreeScale, SpriteEffects.None, 0f);
            if (chosenSettingOption == 2)
            {
                spriteBatch.DrawString(arial, DeveloperModelTexts[DeveloperModelChosen], settingListPosition[1] + new Vector2(100 * Scale, 0), settingThreeColor, 0f, Vector2.Zero, settingOneScale, SpriteEffects.None, 0f);
            }
        }

        public void DrawLifeSpan(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(arial, "Life:", new Vector2(lifeSpanRect.X - 20 * Scale, lifeSpanRect.Y-2*Scale), Color.Black,0.0f,Vector2.Zero,Scale/2.0f,SpriteEffects.None,0.0f);
            spriteBatch.Draw(this.ObjectSpanTexture, lifeSpanRect, Color.Black * 0.5f);
            spriteBatch.Draw(this.ObjectSpanTexture, totalLifeSpanRect, Color.White);
            spriteBatch.Draw(this.ObjectSpanTexture, currentLifeSpanRect, Color.Red);
            spriteBatch.DrawString(arial, CurrentPlayerLife.ToString() + "/" + PlayerLife.ToString(), lifeDipslayPosition, Color.Black, 0.0f, Vector2.Zero, Scale / 3.0f, SpriteEffects.None, 0.0f);

        }
        public void DrawEnemyNum(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(arial, "Enemy: " + EnemyNum.ToString() , numOfEnemyPosition, Color.OrangeRed, 0.0f, Vector2.Zero, Scale / 2.0f, SpriteEffects.None, 0.0f);
        }

        public void DrawBulletNum(SpriteBatch spriteBatch) {
            Color model = Color.Black;
            if (currentBulletNum == 0)
            {
                model = Color.DarkRed;
            }
            else
                model = Color.Black;
            spriteBatch.DrawString(arial, currentBulletMsg + currentBulletNum.ToString() + " / " + bulletNumState.ToString(), BulletNumStatePosition, model, 0.0f, Vector2.Zero, Scale / 2.0f, SpriteEffects.None, 0.0f);
        }

        public void DisplaySpyState(SpriteBatch spriteBatch) {
            DrawLifeSpan(spriteBatch);
            DrawBulletNum(spriteBatch);
        }

        public void BulletReloadAlert(int bulletState)
        {
            if (bulletState == 0)
            {
                reloadAlertMsg = "Press R \nto Reload";
                ReloadAlertTimer = 0;
            }
            else if (bulletState == 1) {
                reloadAlertMsg = "Ready to Shoot!";
                ReloadAlertTimer = 0;
            }
        }

        public void DisplayReloadAlert(SpriteBatch spriteBatch) {
            if(ReloadAlertTimer <= reloadAlertDuring) { 
                spriteBatch.DrawString(arial, reloadAlertMsg, barrelPosition + new Vector2(30*Scale,0), Color.DarkRed, 0.0f, Vector2.Zero, Scale / 2.0f, SpriteEffects.None, 0.0f);
            }
        }

        public void DisplayHowToPlay(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(arial, "How To Play:\n<- / ->: Left / Right Move \nA/D: Rotate Barrel\n1/2/3: Change Attack Model\nR: Reload Bullets \nQ: Call Marco Rossi\nS: Active Shiled", Vector2.Zero, Color.Red * 0.8f, 0.0f, Vector2.Zero, Scale / 2, SpriteEffects.None, 0.0f);
        }

        internal void DrawReloadBar(SpriteBatch spriteBatch)
        {
            if(decreaseReloadBarTimer <=3 + reloadAlertDuring) { 
                spriteBatch.Draw(this.ObjectSpanTexture, new Rectangle((int)barrelPosition.X + 32 * Scale, (int)barrelPosition.Y +20*Scale, 10*Scale, 34*Scale), Color.Black * 0.5f);
                spriteBatch.Draw(this.ObjectSpanTexture, new Rectangle((int)barrelPosition.X + 32 * Scale + 2*Scale, (int)barrelPosition.Y + 20 * Scale + 2*Scale, 6*Scale, 30*Scale), decreaseBarColor);
                spriteBatch.Draw(this.ObjectSpanTexture, new Rectangle((int)barrelPosition.X + 32 * Scale + 2 * Scale, (int)barrelPosition.Y + 20 * Scale + 2 * Scale, 6 * Scale, this.decreaseReloadBar* Scale), Color.White);
            }
        }

        internal void DrawAttckIcon(SpriteBatch spriteBatch)
        {                                 
            spriteBatch.Draw(this.ObjectSpanTexture, new Rectangle((int)AttackOneIconPosition.X * Scale, (int)AttackOneIconPosition.Y * Scale, AttackModelIconLength * Scale + currentScale_1, AttackModelIconLength * Scale + currentScale_1), Color.Black);
            spriteBatch.Draw(this.AttackOneIconTexture, new Rectangle((int)AttackOneIconPosition.X * Scale + 1*Scale, (int)AttackOneIconPosition.Y * Scale + 1 * Scale, AttackModelIconLength * Scale - 2 *Scale + currentScale_1, AttackModelIconLength * Scale - 2 * Scale + currentScale_1), Color.White);
            spriteBatch.Draw(this.ObjectSpanTexture, new Rectangle((int)AttackOneIconPosition.X * Scale + 1 * Scale, (int)AttackOneIconPosition.Y * Scale + 1 * Scale, AttackModelIconLength * Scale - 2 * Scale + currentScale_1, shadowHeightModelOne), attackModelOneColor * 0.8f);

            spriteBatch.Draw(this.ObjectSpanTexture, new Rectangle((int)AttackTwoIconPosition.X * Scale, (int)AttackTwoIconPosition.Y * Scale, AttackModelIconLength * Scale + currentScale_2, AttackModelIconLength * Scale + currentScale_2), Color.Black);
            spriteBatch.Draw(this.AttackTwoIconTexture, new Rectangle((int)AttackTwoIconPosition.X * Scale + 1 * Scale, (int)AttackTwoIconPosition.Y * Scale + 1 * Scale, AttackModelIconLength * Scale - 2 * Scale + currentScale_2, AttackModelIconLength * Scale - 2 * Scale + currentScale_2), Color.White);
            spriteBatch.Draw(this.ObjectSpanTexture, new Rectangle((int)AttackTwoIconPosition.X * Scale + 1 * Scale, (int)AttackTwoIconPosition.Y * Scale + 1 * Scale, AttackModelIconLength * Scale - 2 * Scale + currentScale_2, shadowHeightModelTwo), attackModelTwoColor * 0.8f);

            spriteBatch.Draw(this.ObjectSpanTexture, new Rectangle((int)AttackThreeIconPosition.X * Scale, (int)AttackThreeIconPosition.Y * Scale, AttackModelIconLength * Scale + currentScale_3, AttackModelIconLength * Scale + currentScale_3), Color.Black);
            spriteBatch.Draw(this.AttackThreeIconTexture, new Rectangle((int)AttackThreeIconPosition.X * Scale + 1 * Scale, (int)AttackThreeIconPosition.Y * Scale + 1 * Scale, AttackModelIconLength * Scale - 2 * Scale + currentScale_3, AttackModelIconLength * Scale - 2 * Scale + currentScale_3), Color.White);
            spriteBatch.Draw(this.ObjectSpanTexture, new Rectangle((int)AttackThreeIconPosition.X * Scale + 1 * Scale, (int)AttackThreeIconPosition.Y * Scale + 1 * Scale, AttackModelIconLength * Scale - 2 * Scale + currentScale_3, shadowHeightModelThree), attackModelThreeColor * 0.8f);


            spriteBatch.Draw(this.ObjectSpanTexture, new Rectangle((int)AttackFourIconPosition.X * Scale, (int)AttackFourIconPosition.Y * Scale, AttackModelIconLength * Scale, AttackModelIconLength * Scale), Color.Black);
            spriteBatch.Draw(this.ObjectSpanTexture, new Rectangle((int)AttackFourIconPosition.X * Scale + 1 * Scale, (int)AttackFourIconPosition.Y * Scale + 1 * Scale, AttackModelIconLength * Scale - 2 * Scale, AttackModelIconLength * Scale - 2 * Scale), shiledIconColor);
            spriteBatch.Draw(this.AttackFourIconTexture, new Rectangle((int)AttackFourIconPosition.X * Scale + 1 * Scale, (int)AttackFourIconPosition.Y * Scale + 1 * Scale, AttackModelIconLength * Scale - 2 * Scale, AttackModelIconLength * Scale - 2 * Scale), Color.White);

        }

        internal void ActiveReloadBar(int index) {
            this.decreaseReloadBarTimer = 0;
            this.decreaseReloadBar = 30;
            decreaseBarColor = Color.Red;
            if (index == 1)
            {

            }
            else if (index == 2)
            {

            }
            else if (index == 3) { 
            
            }
        }

        internal void DrawChangeScene(SpriteBatch spriteBatch) {
            switch (jumpModel)
            {
                case 0:
                    spriteBatch.Draw(this.ObjectSpanTexture, changeSceneRectZero, Color.Black);
                    break;
                case 1:
                    spriteBatch.Draw(this.ObjectSpanTexture, changeSceneRectOneUp, Color.Black);
                    spriteBatch.Draw(this.ObjectSpanTexture, changeSceneRectOneDown, Color.Black);
                    break;
                case 2:
                    spriteBatch.Draw(this.ObjectSpanTexture, changeSceneRectTwoLeft, Color.Black);
                    spriteBatch.Draw(this.ObjectSpanTexture, changeSceneRectTwoRight, Color.Black);
                    break;
                case 3:
                    spriteBatch.Draw(this.ObjectSpanTexture, changeSceneRectZero, Color.Black);
                    break;
                case 4:
                    spriteBatch.Draw(this.ObjectSpanTexture, changeSceneRectOneUp, Color.Black);
                    spriteBatch.Draw(this.ObjectSpanTexture, changeSceneRectOneDown, Color.Black);
                    break;
                case 5:
                    spriteBatch.Draw(this.ObjectSpanTexture, changeSceneRectTwoLeft, Color.Black);
                    spriteBatch.Draw(this.ObjectSpanTexture, changeSceneRectTwoRight, Color.Black);
                    break;
                default:
                    break;
            }
            if (JumpSceneTimer <= jumpSceneDuring &&!IsPlayerDie)
            {
                spriteBatch.DrawString(arial, currentLevelStr, currentLevelStrPosition, Color.Red * 0.8f, 0.0f, Vector2.Zero, Scale * 2, SpriteEffects.None, 0.0f);
            }
            else if (IsPlayerDie || GameOver) {
                spriteBatch.DrawString(arial, currentLevelStr, currentLevelStrPosition - new Vector2(0,30*Scale), Color.Red * 0.8f, 0.0f, Vector2.Zero, Scale * 2, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(arial, "Developer: Jian \nInstructer:Conrad Nobert", Vector2.Zero, Color.Red * 0.8f, 0.0f, Vector2.Zero, Scale/1.2f, SpriteEffects.None, 0.0f);

                if(JumpSceneTimer >= 1.5f) { 
                    spriteBatch.DrawString(arial, "Prese 'Enter' \nto return Menu.", currentLevelStrPosition + new Vector2(0,100*Scale), Color.Red * 0.8f, 0.0f, Vector2.Zero, Scale * 1, SpriteEffects.None, 0.0f);
                }

            }
        }

        internal void DrawPausedMsg(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(arial, "Paused!!", currentLevelStrPosition, Color.Red * 0.8f, 0.0f, Vector2.Zero, Scale * 2, SpriteEffects.None, 0.0f);

        }

        internal void ResetChangeScene(int jumpModel) {
            //this.thisLevel = true;
            this.JumpSceneTimer = 0;
            this.jumpModel = jumpModel;
            this.preJumpModel = jumpModel;
            if (jumpModel == 0 || jumpModel == 1 || jumpModel == 2)
            {
                autoIncreasePixcelZero = 0;
            }
            else if (jumpModel == 3 | jumpModel == 5 )
            {
                autoIncreasePixcelZero = 275;
            }
            else if (jumpModel == 4)
            {
                autoIncreasePixcelZero = 200;
            }       
        }

        internal void CloseScene() {
            this.JumpSceneActive = true;
            this.jumpSceneTimer = 0;
            if (this.preJumpModel == 0)
            {
                jumpModel = 3;
                autoIncreasePixcelZero = 275;
            }
            else if (this.preJumpModel == 1) {
                jumpModel = 4;
                autoIncreasePixcelZero = 200;
            }
            else if (this.preJumpModel == 2)
            {
                jumpModel = 5;
                autoIncreasePixcelZero = 275;
            }
        }

        internal void SetTotalBulletNumAttackModel(int totalBulletModelOne, int totalBulletModelTwo, int totalBulletModelThree) {
            this.totalBulletModelOne = totalBulletModelOne;
            this.totalBulletModelTwo = totalBulletModelTwo;
            this.totalBulletModelThree = totalBulletModelThree;
            this.bulletLeftModelOne = totalBulletModelOne;
            this.bulletLeftModelTwo = totalBulletModelTwo;
            this.bulletLeftModelThree = totalBulletModelThree;
        }

        private int GetShiledLife(int shiledLife) {
            return shiledLife;
        }

        private void IconAndBarDisplay(float decreaseReloadBarTimer) {
            if (decreaseReloadBarTimer > 0.1f && decreaseReloadBarTimer <= 0.3f)
            {
                decreaseReloadBar = 27;
                if (this.currentAttackModel == 1)
                {
                    bulletLeftModelOne = totalBulletModelOne * 1 / 10;
                }
                else if (this.currentAttackModel == 2)
                {
                    bulletLeftModelTwo = totalBulletModelTwo * 1 / 10;
                }
                else if (this.currentAttackModel == 3)
                {
                    bulletLeftModelThree = totalBulletModelThree * 1 / 10;
                }
            }
            else if (decreaseReloadBarTimer <= 0.6f)
            {
                decreaseReloadBar = 24;
                if (this.currentAttackModel == 1)
                {
                    bulletLeftModelOne = totalBulletModelOne * 2 / 10;
                }
                else if (this.currentAttackModel == 2)
                {
                    bulletLeftModelTwo = totalBulletModelTwo * 2 / 10;
                }
                else if (this.currentAttackModel == 3)
                {
                    bulletLeftModelThree = totalBulletModelThree * 2 / 10;
                }
            }
            else if (decreaseReloadBarTimer <= 0.9f)
            {
                decreaseReloadBar = 21;
                if (this.currentAttackModel == 1)
                {
                    bulletLeftModelOne = totalBulletModelOne * 3 / 10;
                }
                else if (this.currentAttackModel == 2)
                {
                    bulletLeftModelTwo = totalBulletModelTwo * 3 / 10;
                }
                else if (this.currentAttackModel == 3)
                {
                    bulletLeftModelThree = totalBulletModelThree * 3 / 10;
                }
            }
            else if (decreaseReloadBarTimer <= 1.2f)
            {
                decreaseBarColor = Color.YellowGreen;
                decreaseReloadBar = 18;
                if (this.currentAttackModel == 1)
                {
                    bulletLeftModelOne = totalBulletModelOne * 4 / 10;
                }
                else if (this.currentAttackModel == 2)
                {
                    bulletLeftModelTwo = totalBulletModelTwo * 4 / 10;
                }
                else if (this.currentAttackModel == 3)
                {
                    bulletLeftModelThree = totalBulletModelThree * 4 / 10;
                }
            }
            else if (decreaseReloadBarTimer <= 1.5f)
            {
                decreaseReloadBar = 15;
                if (this.currentAttackModel == 1)
                {
                    bulletLeftModelOne = totalBulletModelOne * 5 / 10;
                }
                else if (this.currentAttackModel == 2)
                {
                    bulletLeftModelTwo = totalBulletModelTwo * 5 / 10;
                }
                else if (this.currentAttackModel == 3)
                {
                    bulletLeftModelThree = totalBulletModelThree * 5 / 10;
                }
            }
            else if (decreaseReloadBarTimer <= 1.8f)
            {
                decreaseReloadBar = 12;
                if (this.currentAttackModel == 1)
                {
                    bulletLeftModelOne = totalBulletModelOne * 6 / 10;
                }
                else if (this.currentAttackModel == 2)
                {
                    bulletLeftModelTwo = totalBulletModelTwo * 6 / 10;
                }
                else if (this.currentAttackModel == 3)
                {
                    bulletLeftModelThree = totalBulletModelThree * 6 / 10;
                }
            }
            else if (decreaseReloadBarTimer <= 2.1f)
            {
                decreaseBarColor = Color.Green;
                decreaseReloadBar = 9;
                if (this.currentAttackModel == 1)
                {
                    bulletLeftModelOne = totalBulletModelOne * 7 / 10;
                }
                else if (this.currentAttackModel == 2)
                {
                    bulletLeftModelTwo = totalBulletModelTwo * 7 / 10;
                }
                else if (this.currentAttackModel == 3)
                {
                    bulletLeftModelThree = totalBulletModelThree * 7 / 10;
                }
            }
            else if (decreaseReloadBarTimer <= 2.4f)
            {
                decreaseReloadBar = 6;
                if (this.currentAttackModel == 1)
                {
                    bulletLeftModelOne = totalBulletModelOne * 8 / 10;
                }
                else if (this.currentAttackModel == 2)
                {
                    bulletLeftModelTwo = totalBulletModelTwo * 8 / 10;
                }
                else if (this.currentAttackModel == 3)
                {
                    bulletLeftModelThree = totalBulletModelThree * 8 / 10;
                }
            }
            else if (decreaseReloadBarTimer <= 2.7)
            {
                decreaseReloadBar = 3;
                if (this.currentAttackModel == 1)
                {
                    bulletLeftModelOne = totalBulletModelOne * 9 / 10;
                }
                else if (this.currentAttackModel == 2)
                {
                    bulletLeftModelTwo = totalBulletModelTwo * 9 / 10;
                }
                else if (this.currentAttackModel == 3)
                {
                    bulletLeftModelThree = totalBulletModelThree * 9 / 10;
                }
            }
            else if (decreaseReloadBarTimer <= 3.0f)
            {
                decreaseReloadBar = 0;
                if (this.currentAttackModel == 1)
                {
                    bulletLeftModelOne = totalBulletModelOne * 10 / 10;
                }
                else if (this.currentAttackModel == 2)
                {
                    bulletLeftModelTwo = totalBulletModelTwo * 10 / 10;
                }
                else if (this.currentAttackModel == 3)
                {
                    bulletLeftModelThree = totalBulletModelThree * 10 / 10;
                }
            }
        }

    }
}
