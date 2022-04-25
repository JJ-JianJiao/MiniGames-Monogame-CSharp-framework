using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace MosquitoAttack
{
    public class MosquitoAttack : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        static Random random = new Random();

        private bool firstStart = true;
        //Frame object
        Texture2D ObjectFrameTexture;
        Color[] ObjectFrameColors;
        bool ObjectFrameActive = false;

        const int ZoomSize = 2;
        const int WindowsWidth = 550 * ZoomSize;
        const int WindowsHeight = 400 * ZoomSize;

        BackGroud sceneOneBG = new BackGroud();
        Texture2D sceneOneTexture;
        Texture2D MenuBgTexture;
        const int sceneOneWidth = 550 * ZoomSize;
        const int sceneOneHeight = 400 * ZoomSize;


        Cannon cannon = new Cannon();
        const int cannonWidth = 40;
        const int cannonHeight = 19;
        const int cannonSpeed = 100 * ZoomSize;
        const float cannonCelTime = 1 / 8f;
        Texture2D cannonTexture;
        Vector2 cannonStartPosition = new Vector2(0, 378 - cannonHeight) * ZoomSize;
        const float reloadCannonCounter = 3.0f;

        const int barrelWidth = 18;
        const int barrelHeight = 36;
        const float barrelCelTime = -1;
        Texture2D barrelTexture;


        const int cannonBallWidth = 4;
        const int cannonBallHeight = 4;
        const float cannonBallCelTime = -1;
        const float cannonBallSpeed = 200 * ZoomSize;
        Vector2 cannonBallPosition = new Vector2(-10 * ZoomSize, -10 * ZoomSize);
        Texture2D cannonBallTexture;
        Texture2D AttackIconTexture;

        Texture2D cannonBall01Texture;
        Texture2D AttackOneIconTexture;

        Texture2D cannonBall02Texture;
        Texture2D AttackTwoIconTexture;

        Texture2D cannonBall03Texture;
        Texture2D AttackThreeIconTexture;

        Texture2D cannonBall04Texture;
        const int NumOfCannonBall = 10;

        const int NumofSinWaveBall = 3;

        const int NumofSplitBall = 6;
        float cooldownSinWaveBallTimer = 0.0f;
        const float cooldownSineWaveBallDruing = 0.22f;


        List<Mosquito> normalMosquitos = new List<Mosquito>();
        const int MosquitokWidth = 46;
        const int MosquitoHeight = 40;
        const float MosquitoCelTime = 1.0f / 11f;
        const int MosquitoSpeed = 250;
        const int MosquitoMinSpeed = 100 * ZoomSize;
        const int MosquitoMaxSpeed = 160 * ZoomSize;
        Vector2 MosquitoPosition = new Vector2(60, 30) * ZoomSize;
        Texture2D mosquitoTexture;


        protected int mosquitoNum = 20;
        protected int mosequitoNumLevelTwo = 100;
        protected int MosquitoFireballFrequency = 500;
        protected int MosquitoFireballFrequencyLevelTwo = 1000;





        protected int MosquitoFireballFrequencyMin = 1;


        const int FireballWidth = 5;
        const int FireballHeight = 17;
        const float FireballCelTime = 1.0f / 8;
        const int FireballSpeed = 200 * ZoomSize;
        Vector2 FireballPosition = new Vector2(-20 * ZoomSize, 0);
        Texture2D firballTexture;

        UI ui = new UI();
        SpriteFont arial;
        //Texture2D menu
        Vector2 menuPosition = new Vector2(388, 233) * ZoomSize;
        Vector2 settingMenuPositon = new Vector2(10, 290) * ZoomSize;

        Texture2D poofTexture;
        const int PoofWidth = 16;
        const float PoofCelTime = 1 / 16.0f;
        const int PoofHeight = 16;

        Shield cannonShield = new Shield();
        Texture2D CannonSheildTexture;
        const int shieldWidth = 556;
        const int shieldHeight = 556;
        const int shieldScale = ZoomSize;
        Vector2 cannonSheildPositon = new Vector2(0, 0);


        MarcoRossi marcoRossi = new MarcoRossi();
        Texture2D marcoRossiTexture;
        const int marcoRossiWidth = 550;
        const int marcoRossiHeight = 460;
        const int marcoScale = ZoomSize;
        Vector2 marcoRossiPosition = Vector2.Zero;
        const float marcoRossiSpeed = 100 * ZoomSize;

        NetBarrier netBarrierOne = new NetBarrier();
        const int netBarrierOneWidth = 1920;
        const int netBarrierOneHeight = 913;
        const int netBarrierOneScale = ZoomSize;
        const int netBarrierOneSpeed = 50 * ZoomSize;
        Vector2 netBarrierOnePositon = new Vector2(20, 150) * ZoomSize;
        Texture2D netBarrierTexture;
        //Vector2


        //NetBarrier NetBarrierTwo = new NetBarrier();

        KeyboardState currentKbState;
        KeyboardState preKbState;

        public enum GameState
        {
            Start,
            Playing,
            Paused,
            Over
        }

        GameState currentGameState = GameState.Start;
        private bool setting;

        //For test
        //GameState currentGameState = GameState.Playing;

        public MosquitoAttack()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = WindowsWidth;
            graphics.PreferredBackBufferHeight = WindowsHeight;
            graphics.ApplyChanges();
            graphics.SynchronizeWithVerticalRetrace = true;
        }

        protected override void Initialize()
        {
            sceneOneBG.Initial(sceneOneWidth, sceneOneHeight, ZoomSize);
            cannon.Initial(cannonWidth, cannonHeight, cannonSpeed, ZoomSize, cannonStartPosition, sceneOneBG.gameBorder);
            ui.Initial(ZoomSize, cannon.Life, cannon.Life, normalMosquitos.Count, NumOfCannonBall);
            ui.SetTotalBulletNumAttackModel(NumOfCannonBall,NumofSinWaveBall,NumofSplitBall);
            ui.SetMenuPosition(menuPosition);
            ui.SetSettingMenuPosition(settingMenuPositon);
            cannonShield.Initial(shieldWidth, shieldHeight, 0, ZoomSize, cannonSheildPositon, sceneOneBG.gameBorder);
            marcoRossi.Initial(marcoRossiWidth, marcoRossiHeight, marcoRossiSpeed, ZoomSize, marcoRossiPosition, sceneOneBG.gameBorder);
            netBarrierOne.Initial(netBarrierOneWidth, netBarrierOneHeight, netBarrierOneSpeed, ZoomSize, netBarrierOnePositon, sceneOneBG.gameBorder);
            base.Initialize();

        }

        protected override void LoadContent()
        {



            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Create Object Frame Texture
            ObjectFrameTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            ObjectFrameColors = new Color[1 * 1];
            for (int i = 0; i < ObjectFrameColors.Length; ++i)
                ObjectFrameColors[i] = Color.White;
            ObjectFrameTexture.SetData(ObjectFrameColors);

            //poof texture
            poofTexture = Content.Load<Texture2D>("Poof");

            sceneOneTexture = Content.Load<Texture2D>("Background");
            MenuBgTexture = Content.Load<Texture2D>("MenuBG");
            sceneOneBG.LoadContent(MenuBgTexture);

            cannonTexture = Content.Load<Texture2D>("Cannon");
            cannon.LoadContent(cannonTexture, cannonCelTime);
            cannon.ObjectFrameTexture = ObjectFrameTexture;
            cannon.ObjectFrameActive = ObjectFrameActive;

            arial = Content.Load<SpriteFont>("SystemArialFont");
            ui.LoadContent(arial);

            barrelTexture = Content.Load<Texture2D>("Barrel");
            cannon.InitialBarrel(barrelWidth, barrelHeight, barrelTexture, barrelCelTime);

            cannonBallTexture = Content.Load<Texture2D>("CannonBall");
            cannonBall01Texture = Content.Load<Texture2D>("CannonBall_01");
            cannonBall02Texture = Content.Load<Texture2D>("CannonBall_02");
            cannonBall03Texture = Content.Load<Texture2D>("CannonBall_03");
            cannonBall04Texture = Content.Load<Texture2D>("CannonBall_04");


            //cannonBallTexture = Content.Load<Texture2D>("CannonBall_04");

            mosquitoTexture = Content.Load<Texture2D>("Mosquito");

            firballTexture = Content.Load<Texture2D>("Fireball");

            ui.ObjectSpanTexture = ObjectFrameTexture;

            CannonSheildTexture = Content.Load<Texture2D>("Shiled");
            cannonShield.LoadContent(CannonSheildTexture, -1);
            cannonShield.ObjectFrameTexture = ObjectFrameTexture;
            cannonShield.ObjectFrameActive = ObjectFrameActive;


            marcoRossiTexture = Content.Load<Texture2D>("MarcoRossi");
            marcoRossi.LoadContent(marcoRossiTexture,-1);
            marcoRossi.ObjectFrameTexture = ObjectFrameTexture;
            marcoRossi.ObjectFrameActive = ObjectFrameActive;
            marcoRossi.CannonBorder = cannon.ObjectBorder;

            netBarrierTexture = Content.Load<Texture2D>("NetOne");
            netBarrierOne.LoadContent(netBarrierTexture, -1);
            netBarrierOne.ObjectFrameTexture = ObjectFrameTexture;
            netBarrierOne.ObjectFrameActive = ObjectFrameActive;


            AttackIconTexture = Content.Load<Texture2D>("NormalIcon");
            AttackOneIconTexture = Content.Load<Texture2D>("SineIcon");
            AttackTwoIconTexture = Content.Load<Texture2D>("SplitIcon");
            //AttackThreeIconTexture = Content.Load<Texture2D>("SineIcon");
            //AttackIconTexture = Content.Load<Texture2D>("CannonBall_04");
            ui.SetAttackModelIconTexture(AttackIconTexture, AttackOneIconTexture, AttackTwoIconTexture, CannonSheildTexture);

            marcoRossi.GetBallProperty(cannonBallWidth, cannonBallHeight, cannonSpeed, cannonBall04Texture);
            marcoRossi.GetFontTextrue(arial);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            currentKbState = Keyboard.GetState();

            switch (currentGameState)
            {
                case GameState.Start:
                    //sceneOneBG.Texture = MenuBgTexture;
                    if (currentKbState.IsKeyUp(Keys.Up) && preKbState.IsKeyDown(Keys.Up) && !ui.settingActive)
                    {
                        // 0:Up, 1:down
                        ui.ChangeOption(0);
                    }
                    else if (currentKbState.IsKeyUp(Keys.Down) && preKbState.IsKeyDown(Keys.Down) &&  !ui.settingActive)
                    {
                        ui.ChangeOption(1);
                    }

                    if (currentKbState.IsKeyUp(Keys.Enter) && preKbState.IsKeyDown(Keys.Enter) && !ui.settingActive)
                    {
                        switch (ui.ChosenOption)
                        {
                            case 0:
                                sceneOneBG.Texture = sceneOneTexture;
                                ui.JumpSceneActive = true;
                                ui.ResetChangeScene(1);
                                
                                if(ui.DifficultChosen == 0) {
                                    mosquitoNum = 20;
                                    mosequitoNumLevelTwo = 100;
                                    MosquitoFireballFrequency = 500;
                                    MosquitoFireballFrequencyLevelTwo = 1000;
                                }
                                else if (ui.DifficultChosen == 1)
                                {
                                    mosquitoNum = 40;
                                    mosequitoNumLevelTwo = 200;
                                    MosquitoFireballFrequency = 1000;
                                    MosquitoFireballFrequencyLevelTwo = 1500;
                                }
                                else if (ui.DifficultChosen == 2)
                                {
                                    mosquitoNum = 100;
                                    mosequitoNumLevelTwo = 500;
                                    MosquitoFireballFrequency = 1000;
                                    MosquitoFireballFrequencyLevelTwo = 3000;
                                }

                                if (ui.SizeModelChosen == 0)
                                {
                                    if (ZoomSize != 1) {

                                    }
                                }
                                else if (ui.SizeModelChosen == 1)
                                {
                                    if (ZoomSize !=2)
                                    {

                                    }
                                }
                                else if (ui.SizeModelChosen == 2)
                                {
                                    if (ZoomSize != 3)
                                    {

                                    }
                                }

                                if (ui.DeveloperModelChosen == 0) {
                                    ObjectFrameActive = true;
                                    cannon.ObjectFrameActive = true;
                                    cannon.barrel.ObjectFrameActive = true;
                                    marcoRossi.ObjectFrameActive = true;
                                    cannonShield.ObjectFrameActive = true;
                                    netBarrierOne.ObjectFrameActive = true;
                                }
                                else if (ui.DeveloperModelChosen == 1)
                                {
                                    ObjectFrameActive = false;
                                    cannon.ObjectFrameActive = false;
                                    cannon.barrel.ObjectFrameActive = false;
                                    marcoRossi.ObjectFrameActive = false;
                                    cannonShield.ObjectFrameActive = false;
                                    netBarrierOne.ObjectFrameActive = false;
                                }

                                normalMosquitos = CreateMosquito();
                                ui.LevelOneActive = true;
                                ui.LevelTwoActive = false;
                                ui.LevelTwoActive = false;
                                ui.EndCurrentLevel = false;
                                currentGameState = GameState.Playing;
                                break;
                            case 1:
                                ui.settingActive = true;
                                break;
                            case 2:
                                Exit();
                                break;
                            default:
                                break;
                        }
                    }
                    if (ui.settingActive) { 
                        if (currentKbState.IsKeyUp(Keys.Up) && preKbState.IsKeyDown(Keys.Up))
                        {
                            // 0:Up, 1:down
                            ui.ChangeSetOption(0);
                        }
                        else if (currentKbState.IsKeyUp(Keys.Down) && preKbState.IsKeyDown(Keys.Down))
                        {
                            ui.ChangeSetOption(1);
                        }
                        else if (currentKbState.IsKeyUp(Keys.Down) && preKbState.IsKeyDown(Keys.Down))
                        {
                            ui.ChangeSetOption(2);
                        }

                        if (currentKbState.IsKeyUp(Keys.Escape) && preKbState.IsKeyDown(Keys.Escape))
                        {
                            ui.settingActive = false;
                        }

                        if (ui.ChosenSettingOption == 0) {
                            if (currentKbState.IsKeyUp(Keys.Left) && preKbState.IsKeyDown(Keys.Left))
                            {
                                if (ui.DifficultChosen == 1)
                                {
                                    ui.DifficultChosen = 0;
                                }
                                else if (ui.DifficultChosen == 0)
                                {
                                    ui.DifficultChosen = 2;
                                }
                                else if (ui.DifficultChosen == 2)
                                {
                                    ui.DifficultChosen = 1;
                                }
                            }
                            if (currentKbState.IsKeyUp(Keys.Right) && preKbState.IsKeyDown(Keys.Right))
                            {
                                if (ui.DifficultChosen == 1)
                                {
                                    ui.DifficultChosen = 2;
                                }
                                else if (ui.DifficultChosen == 0)
                                {
                                    ui.DifficultChosen = 1;
                                }
                                else if (ui.DifficultChosen == 2)
                                {
                                    ui.DifficultChosen = 0;
                                }
                            }
                        }
                        if (ui.ChosenSettingOption == 1)
                        {
                            if (currentKbState.IsKeyUp(Keys.Left) && preKbState.IsKeyDown(Keys.Left))
                            {
                                if (ui.SizeModelChosen == 1)
                                {
                                    ui.SizeModelChosen = 0;
                                }
                                else if (ui.SizeModelChosen == 0)
                                {
                                    ui.SizeModelChosen = 2;
                                }
                                else if (ui.SizeModelChosen == 2)
                                {
                                    ui.SizeModelChosen = 1;
                                }
                            }
                            if (currentKbState.IsKeyUp(Keys.Right) && preKbState.IsKeyDown(Keys.Right))
                            {
                                if (ui.SizeModelChosen == 1)
                                {
                                    ui.SizeModelChosen = 2;
                                }
                                else if (ui.SizeModelChosen == 0)
                                {
                                    ui.SizeModelChosen = 1;
                                }
                                else if (ui.SizeModelChosen == 2)
                                {
                                    ui.SizeModelChosen = 0;
                                }
                            }
                        }
                        if (ui.ChosenSettingOption == 2)
                        {
                            if (currentKbState.IsKeyUp(Keys.Left) && preKbState.IsKeyDown(Keys.Left))
                            {
                                if (ui.DeveloperModelChosen == 1)
                                {
                                    ui.DeveloperModelChosen = 0;
                                }
                                else if (ui.DeveloperModelChosen == 0)
                                {
                                    ui.DeveloperModelChosen = 1;
                                }
                            }
                            if (currentKbState.IsKeyUp(Keys.Right) && preKbState.IsKeyDown(Keys.Right))
                            {
                                if (ui.DeveloperModelChosen == 1)
                                {
                                    ui.DeveloperModelChosen = 0;
                                }
                                else if (ui.DeveloperModelChosen == 0)
                                {
                                    ui.DeveloperModelChosen = 1;
                                }
                            }
                        }
                    }

                    break;
                case GameState.Playing:
                    cooldownSinWaveBallTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;


                    if (ui.LevelTwoActive && !ui.LevelTwoInitial)
                    {
                        mosquitoNum = mosequitoNumLevelTwo;
                        MosquitoFireballFrequency = MosquitoFireballFrequencyLevelTwo;
                        normalMosquitos = CreateMosquito();
                        ui.JumpSceneActive = true;
                        ui.ResetChangeScene(2);
                        ui.LevelTwoInitial = true;
                        ui.EndCurrentLevel = false;
                        ui.NumDestroyEnemy = 0;
                        cannon.cannonNomalBalls.Clear();
                    }


                    //UI Update
                    int numOfEnmy = 0;
                    numOfEnmy += normalMosquitos.Count;
                    if (cannon.AttackModelOne)
                    {
                        ui.bulletNumState = NumOfCannonBall;
                        ui.currentAttackModel = 1;
                        ui.Update(gameTime, cannon.Life, cannon.cannonNomalBalls.Count, cannon.Position, cannon.barrel.Position, numOfEnmy, cannonShield.getShieldLife());
                    }
                    else if (cannon.AttackModelTwo)
                    {
                        ui.bulletNumState = NumofSinWaveBall;
                        ui.currentAttackModel = 2;
                        ui.Update(gameTime, cannon.Life, cannon.sineWaveCannonBalls.Count, cannon.Position, cannon.barrel.Position, numOfEnmy, cannonShield.getShieldLife());
                    }
                    else if (cannon.AttackModelThree)
                    {
                        ui.bulletNumState = NumofSplitBall;
                        ui.currentAttackModel = 3;
                        ui.Update(gameTime, cannon.Life, cannon.splitCannonBalls.Count, cannon.Position, cannon.barrel.Position, numOfEnmy, cannonShield.getShieldLife());
                    }


                    //Mosquitos Loop for mosquito -> Update() && mosquito -> Collision() && Generate Mosquito Fireball
                    foreach (Mosquito aMosquito in normalMosquitos)
                    {

                        aMosquito.Update(gameTime);
                        if (aMosquito.SweepDownMosquito) {
                            aMosquito.GetCannonPositon(cannon.Position);
                            aMosquito.Collision(cannon);
                        }
                        if (aMosquito.Collision(cannon.cannonNomalBalls) || aMosquito.Collision(marcoRossi.normalBalls))
                        {
                            //aMosquito.LoadContent(poofTexture)
                            aMosquito.ChangeTexture(poofTexture, PoofWidth, PoofHeight, PoofCelTime, ZoomSize * 2);
                            ui.NumDestroyEnemy++;

                        }
                        if (aMosquito.Collision(cannon.sineWaveCannonBalls) || aMosquito.Collision(marcoRossi.sinWaveBalls))
                        {
                            //aMosquito.LoadContent(poofTexture)
                            aMosquito.ChangeTexture(poofTexture, PoofWidth, PoofHeight, PoofCelTime, ZoomSize * 2);
                            ui.NumDestroyEnemy++;

                        }
                        if (aMosquito.Collision(cannon.splitCannonBalls) || aMosquito.Collision(marcoRossi.splitBalls))
                        {
                            //aMosquito.LoadContent(poofTexture)
                            aMosquito.ChangeTexture(poofTexture, PoofWidth, PoofHeight, PoofCelTime, ZoomSize * 2);
                            ui.NumDestroyEnemy++;

                        }
                        if (random.Next(MosquitoFireballFrequencyMin, MosquitoFireballFrequency + 1) == 5 && aMosquito.currentState == Mosquito.MosquitoState.Alive && !ui.JumpSceneActive)
                        {
                            Fireball fireball = new Fireball(FireballWidth, FireballHeight, FireballSpeed, ZoomSize, FireballPosition, sceneOneBG.gameBorder);
                            fireball.LoadContent(firballTexture, FireballCelTime);
                            aMosquito.GenerateFireball(fireball);
                        }
                        foreach (Fireball aFireball in aMosquito.fireballs)
                        {
                            aFireball.Update(gameTime);
                            int currentCannonLife = -1;
                            if (aFireball.currentFireBallState == Fireball.FireBallState.Fyling && cannon.Collision(aFireball, out currentCannonLife))
                            {
                                //aFireball.currentFireBallState = Fireball.FireBallState.Dispose;
                                //aFireball.Position = new Vector2(500, 500) * ZoomSize;
                                aFireball.ChangeTexture(poofTexture, PoofWidth, PoofHeight, PoofCelTime, ZoomSize * 2);
                            }

                            if (aFireball.currentFireBallState == Fireball.FireBallState.Fyling && cannonShield.Collision(aFireball))
                            {
                                aFireball.ChangeTexture(poofTexture, PoofWidth, PoofHeight, PoofCelTime, ZoomSize * 2);
                            }

                            if (aFireball.currentFireBallState == Fireball.FireBallState.Fyling && netBarrierOne.Collision(aFireball))
                            {
                                aFireball.ChangeTexture(poofTexture, PoofWidth, PoofHeight, PoofCelTime, ZoomSize * 2);
                            }
                            if (currentCannonLife == 0)
                            {
                                cannon.ChangeTexture(poofTexture, PoofWidth, PoofHeight, PoofCelTime, ZoomSize * 2);
                            }
                        }
                    }


                    //Cannon Movement: inpuet :Left and Right to control Cannon 
                    if (currentKbState.IsKeyDown(Keys.Left))
                    {
                        cannon.moveActive = cannon.Move(new Vector2(-1, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        //cannon.Update(gameTime);
                    }
                    else if (currentKbState.IsKeyDown(Keys.Right))
                    {
                        cannon.moveActive = cannon.Move(new Vector2(1, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        //cannon.Update(gameTime);
                    }
                    else
                    {
                        cannon.moveActive = false;
                    }

                    //Barrel Control. Input D or A to change the Barrel's Rotation
                    if (currentKbState.IsKeyDown(Keys.D))
                    {
                        if (MathHelper.ToDegrees(cannon.barrel.Rotation) < 60)
                        {
                            //cannon.barrel.Rotation += 1* (float)gameTime.ElapsedGameTime.TotalSeconds;
                            cannon.barrel.Rotation += MathHelper.ToRadians(1f);
                        }
                    }
                    else if (currentKbState.IsKeyDown(Keys.A))
                    {
                        if (MathHelper.ToDegrees(cannon.barrel.Rotation) > -60)
                        {
                            //cannon.barrel.Rotation -= 1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                            cannon.barrel.Rotation -= MathHelper.ToRadians(1f);
                        }
                    }

                    cannon.Update(gameTime);
                    if (cannon.cannonNomalBalls.Count == 0 && ui.ReloadActive)
                    {
                        ui.BulletReloadAlert(1);
                        ui.ReloadActive = false;
                    }
                    else if (cannon.sineWaveCannonBalls.Count == 0 && ui.ReloadActiveTwo)
                    {
                        ui.BulletReloadAlert(1);
                        ui.ReloadActiveTwo = false;
                    }
                    else if (cannon.splitCannonBalls.Count == 0 && ui.ReloadActiveThree)
                    {
                        ui.BulletReloadAlert(1);
                        ui.ReloadActiveThree = false;
                    }


                    //Key Space to generate the Cannon's Ball
                    if (currentKbState.IsKeyUp(Keys.Space) && preKbState.IsKeyDown(Keys.Space) && !ui.JumpSceneActive)
                    {

                        if (cannon.AttackModelOne && !ui.ReloadActive)
                        {
                            CreateNormalCannonBall();
                        }
                        else if (cannon.AttackModelTwo && !ui.ReloadActiveTwo)
                        {
                            if (cooldownSinWaveBallTimer > cooldownSineWaveBallDruing)
                            {
                                CreateSineWaveCannonBall();
                                cooldownSinWaveBallTimer = 0;
                            }
                        }
                        else if (cannon.AttackModelThree && !ui.ReloadActiveThree)
                        {
                            CreateSplitCannonBall();
                            cooldownSinWaveBallTimer = 0;
                        }
                    }

                    //Cannon Balls update
                    foreach (CannonBall ball in cannon.cannonNomalBalls)
                    {
                        ball.Update(gameTime);
                        if (ball.Collision(netBarrierOne.ObjectBorder)) {
                            netBarrierOne.HitByPlayer();
                        }
                    }

                    foreach (SineWaveCannonBall waveCannonBall in cannon.sineWaveCannonBalls)
                    {
                        waveCannonBall.Update(gameTime);
                        waveCannonBall.Collision(netBarrierOne);

                    }
                    foreach (SplitCannonBall splitCannonBall in cannon.splitCannonBalls)
                    {
                        splitCannonBall.Update(gameTime);
                        splitCannonBall.Collision(netBarrierOne);
                    }


                    foreach (CannonBall ball in marcoRossi.normalBalls)
                    {
                        ball.Update(gameTime);
                    }
                    foreach (SineWaveCannonBall waveCannonBall in marcoRossi.sinWaveBalls)
                    {
                        waveCannonBall.Update(gameTime);
                    }
                    foreach (SplitCannonBall splitCannonBall in marcoRossi.splitBalls)
                    {
                        splitCannonBall.Update(gameTime);
                    }

                    if (currentKbState.IsKeyUp(Keys.D1) && preKbState.IsKeyDown(Keys.D1))
                    {
                        cannon.AttackModelOne = true;
                        cannon.AttackModelTwo = false;
                        cannon.AttackModelThree = false;
                        ui.RecreaseReloadBarTimer = 11;
                    }
                    else if (currentKbState.IsKeyUp(Keys.D2) && preKbState.IsKeyDown(Keys.D2))
                    {
                        cannon.AttackModelOne = false;
                        cannon.AttackModelTwo = true;
                        cannon.AttackModelThree = false;
                        ui.RecreaseReloadBarTimer = 11;
                    }
                    else if (currentKbState.IsKeyUp(Keys.D3) && preKbState.IsKeyDown(Keys.D3))
                    {
                        cannon.AttackModelOne = false;
                        cannon.AttackModelTwo = false;
                        cannon.AttackModelThree = true;
                        ui.RecreaseReloadBarTimer = 11;
                    }

                    if (currentKbState.IsKeyUp(Keys.S) && preKbState.IsKeyDown(Keys.S))
                    {
                        if (cannonShield.ShieldReadyUse()) {
                            cannonShield.Active();
                            cannon.ShieldIsOn = true;
                        }
                    }

                    if (currentKbState.IsKeyUp(Keys.Q) && preKbState.IsKeyDown(Keys.Q))
                    {
                        marcoRossi.CallBuddy();
                    }

                    if (currentKbState.IsKeyUp(Keys.Escape) && preKbState.IsKeyDown(Keys.Escape) && !ui.JumpSceneActive)
                    {
                        if (!ui.IsPaused) { 
                            currentGameState = GameState.Paused;
                            ui.IsPaused = true;
                        }
                    }

                    if (currentKbState.IsKeyUp(Keys.Enter) && preKbState.IsKeyDown(Keys.Enter) && (ui.IsPlayerDie || ui.GameOver))
                    {
                        if(ui.JumpSceneTimer >= 1.5f) { 
                            currentGameState = GameState.Over;
                        }
                    }



                    //Key R to reload the Cannon's Ball
                    if (currentKbState.IsKeyUp(Keys.R) && preKbState.IsKeyDown(Keys.R))
                    {

                        if (cannon.cannonNomalBalls.Count == NumOfCannonBall && !ui.ReloadActive && cannon.AttackModelOne && !cannon.AttackModelTwo && !cannon.AttackModelThree)
                        {
                            ui.ReloadAlertTimer = 10;
                            cannon.Reload(1);
                            ui.ReloadActive = true;
                            ui.ActiveReloadBar(1);
                        }
                        else if (cannon.sineWaveCannonBalls.Count == NumofSinWaveBall && !ui.ReloadActiveTwo && !cannon.AttackModelOne && cannon.AttackModelTwo && !cannon.AttackModelThree)
                        {
                            ui.ReloadAlertTimer = 10;
                            cannon.Reload(2);
                            ui.ReloadActiveTwo = true;
                            ui.ActiveReloadBar(2);
                        }
                        else if (cannon.splitCannonBalls.Count == NumofSplitBall && !ui.ReloadActiveThree && !cannon.AttackModelOne && !cannon.AttackModelTwo && cannon.AttackModelThree)
                        {
                            ui.ReloadAlertTimer = 10;
                            cannon.Reload(3);
                            ui.ReloadActiveThree = true;
                            ui.ActiveReloadBar(3);
                        }
                    }

                    cannonShield.Update(gameTime, cannon.Position);
                    if (cannonShield.getShieldLife() <= 0) {
                        cannon.ShieldIsOn = false;
                    }
                    netBarrierOne.Update(gameTime);

                    marcoRossi.Update(gameTime, cannon.Position, ui.currentAttackModel);

                    if (cannon.Life <= 0) {
                        //currentGameState = GameState.Over;

                    }

                    break;
                case GameState.Paused:
                    if (currentKbState.IsKeyUp(Keys.Escape) && preKbState.IsKeyDown(Keys.Escape))
                    {
                        if (ui.IsPaused)
                        {
                            currentGameState = GameState.Playing;
                            ui.IsPaused = false;
                        }
                    }
                    break;
                case GameState.Over:
                    sceneOneBG.Texture = MenuBgTexture;
                    this.Initialize();
                    //ui.Initial(ZoomSize, cannon.Life, cannon.Life, normalMosquitos.Count, NumOfCannonBall);
                    //ui.SetTotalBulletNumAttackModel(NumOfCannonBall, NumofSinWaveBall, NumofSplitBall);
                    currentGameState = GameState.Start;
                    break;
                default:
                    break;
            }

            preKbState = currentKbState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();


            //Display Barrel's Rotation (For testing)
            spriteBatch.DrawString(arial, cannon.barrel.Rotation.ToString(), Vector2.One, Color.Red);

            switch (currentGameState)
            {
                case GameState.Start:
                    sceneOneBG.Draw(spriteBatch);
                    ui.DrawChangeScene(spriteBatch);
                    ui.DrawMenu(spriteBatch);
                    if (ui.settingActive) {
                        ui.DrawSettingMenu(spriteBatch);
                    }
                    ui.DisplayHowToPlay(spriteBatch);
                    break;
                case GameState.Playing:
                case GameState.Paused:
                    //UI display draw
                    sceneOneBG.Draw(spriteBatch);
                    ui.DisplaySpyState(spriteBatch);
                    ui.DisplayReloadAlert(spriteBatch);
                    ui.DrawEnemyNum(spriteBatch);
                    ui.DrawReloadBar(spriteBatch);
                    ui.DrawAttckIcon(spriteBatch);



                    foreach (Mosquito aMostuito in normalMosquitos)
                    {
                        aMostuito.Draw(spriteBatch);
                        foreach (Fireball aFireball in aMostuito.fireballs)
                        {
                            aFireball.Draw(spriteBatch);
                        }
                    }
                    cannon.Draw(spriteBatch);

                    foreach (SineWaveCannonBall ball in cannon.sineWaveCannonBalls)
                    {
                        ball.Draw(spriteBatch);
                    }
                    foreach (SplitCannonBall ball in cannon.splitCannonBalls)
                    {
                        ball.Draw(spriteBatch);
                    }

                    cannonShield.Draw(spriteBatch);
                    marcoRossi.Draw(spriteBatch);

                    netBarrierOne.Draw(spriteBatch);
                    //if (ui.JumpSceneActive)
                    //{
                    ui.DrawChangeScene(spriteBatch);
                    //}
                    //Display Barrel's Rotation (For testing)
                    if (cannon.barrel.ObjectFrameActive) { 
                        //spriteBatch.DrawString(arial, cannon.barrel.Rotation.ToString(), Vector2.One, Color.Red);
                        spriteBatch.DrawString(arial, "Rotation:" +  cannon.barrel.Rotation.ToString() + "\nDegree:" + MathHelper.ToDegrees(cannon.barrel.Rotation).ToString(), cannon.barrel.Position + new Vector2(0, -20 * ZoomSize), Color.Red);
                    }

                    if (ui.IsPaused) {
                        ui.DrawPausedMsg(spriteBatch);
                    }


                    break;
                case GameState.Over:
                    break;
                default:
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Create a list of Normal Mosquitos. They have different speed, UpAndDown Wave, facing different ways
        internal List<Mosquito> CreateMosquito()
        {
            List<Mosquito> normalMosquitosModel = new List<Mosquito>();
            if (normalMosquitosModel.Count <= mosquitoNum)
            {
                for (int i = normalMosquitosModel.Count; i < mosquitoNum; i++)
                {
                    Mosquito mosquito = new Mosquito(MosquitokWidth, MosquitoHeight, MosquitoSpeed, ZoomSize, MosquitoPosition, sceneOneBG.gameBorder)
                    {
                        ObjectFrameTexture = ObjectFrameTexture,
                        ObjectFrameActive = ObjectFrameActive
                    };
                    int a = i % 10;
                    switch (a)
                    {
                        case 1:
                            mosquito.Direction = new Vector2(1, 1);
                            mosquito.Position += new Vector2(random.Next(50, 100) * ZoomSize, random.Next(10, 20) * ZoomSize);
                            break;
                        case 2:
                            mosquito.Direction = new Vector2(-1, 1);
                            mosquito.Position += new Vector2(random.Next(100, 150) * ZoomSize, random.Next(10, 20) * ZoomSize);
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            mosquito.Direction = new Vector2(1, -1);
                            mosquito.Position += new Vector2(random.Next(10, 50) * ZoomSize, random.Next(10, 20) * ZoomSize);
                            //mosquito.SweepDownMosquito = true;
                            break;
                        default:
                            mosquito.Direction = new Vector2(-1, -1);
                            mosquito.Position += new Vector2(random.Next(0, 10) * ZoomSize, random.Next(10, 20) * ZoomSize);
                            mosquito.SweepDownMosquito = true;
                            break;
                    }
                    if(ui.DifficultChosen == 0) { 
                        mosquito.SwoopDownColdDownDuring = random.Next(10, 20);
                    }
                    else if (ui.DifficultChosen == 1)
                    {
                        mosquito.SwoopDownColdDownDuring = random.Next(5, 20);
                    }
                    else if (ui.DifficultChosen == 2)
                    {
                        mosquito.SwoopDownColdDownDuring = random.Next(5, 15);
                    }
                    mosquito.SpriteEffect = SpriteEffects.FlipVertically;
                    mosquito.Speed = random.Next(MosquitoMinSpeed, MosquitoMaxSpeed);
                    mosquito.LoadContent(mosquitoTexture, MosquitoCelTime);
                    mosquito.UpDuring = random.Next(30, 40) * 0.01f;
                    mosquito.DownDuring = mosquito.UpDuring * 2;
                    normalMosquitosModel.Add(mosquito);
                }
            }
            return normalMosquitosModel;
        }

        internal void CreateNormalCannonBall()
        {
            if (cannon.cannonNomalBalls.Count < NumOfCannonBall)
            {
                ui.ReloadAlertTimer = 10;
                CannonBall normalCannonBall = new CannonBall();
                normalCannonBall.Initial(cannonBallWidth, cannonBallHeight, cannonBallSpeed, ZoomSize, cannonBallPosition, sceneOneBG.gameBorder);
                normalCannonBall.LoadContent(cannonBallTexture, -1f);
                normalCannonBall.Direction = new Vector2((float)Math.Sin(cannon.barrel.Rotation), -(float)Math.Cos(cannon.barrel.Rotation));
                //normalCannonBall.SetSinProperty(true, 180);
                cannon.Shoot(normalCannonBall);
            }
            else
            {
                ui.BulletReloadAlert(0);
            }
        }

        internal void CreateSineWaveCannonBall()
        {
            if (cannon.sineWaveCannonBalls.Count < NumofSinWaveBall)
            {
                ui.ReloadAlertTimer = 10;
                SineWaveCannonBall sineWaveCannonBall = new SineWaveCannonBall(cannonBallWidth, cannonBallHeight, cannonBallSpeed, ZoomSize, cannonBallPosition, sceneOneBG.gameBorder, cannonBall02Texture);
                cannon.Shoot(sineWaveCannonBall);
            }
            else
            {
                ui.BulletReloadAlert(0);
            }

        }

        internal void CreateSplitCannonBall()
        {
            if (cannon.splitCannonBalls.Count < NumofSplitBall)
            {
                ui.ReloadAlertTimer = 10;
                SplitCannonBall splitCannonBall = new SplitCannonBall(cannonBallWidth, cannonBallHeight, cannonBallSpeed, ZoomSize, cannonBallPosition, sceneOneBG.gameBorder, cannonBall03Texture);
                cannon.Shoot(splitCannonBall);
            }
            else
            {
                ui.BulletReloadAlert(0);
            }
        }


        public void CreatAndWriteSize(int size) {

            if (!File.Exists(@".\"))
            {
                FileStream fs1 = new FileStream(@".\TestTxt.txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(size.ToString());//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(@".\TestTxt.txt", FileMode.Open, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(size.ToString());//开始写入值
                sr.Close();
                fs.Close();
            }
        }
    }
}
