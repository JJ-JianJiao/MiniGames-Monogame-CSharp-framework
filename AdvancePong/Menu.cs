using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_Porn
{
    class Menu
    {
        SpriteFont arialFont;
        private int _menuWidth;
        private int _menuHeight;
        private Point startPosition;
        private Vector2[] menuListPosition;
        private Texture2D menuBackground;
        private float scale;
        Vector2 stringSpace = new Vector2(0,50);
        private string[] menuTexts = {
            "Start",
            " Quit",
            "-- Start -- ",
            "-- Quit -- ",
        };
        private int menuOne = 2;
        private int menuTwo = 1;

        public int MenueWidth {
            set {
                _menuWidth = value;
            }
            get {
                return _menuWidth;
            }
        }

        public int MenueHeight
        {
            set
            {
                _menuHeight = value;
            }
            get
            {
                return _menuHeight;
            }
        }

        
        public Menu(int width, int height, Point startPosition, int scale) {
            _menuWidth = width;
            _menuHeight = height;
            this.startPosition = startPosition;
            this.scale = (float)scale / 2;
        }

        public void LoadContent(Texture2D background) {
            menuBackground = background;    
        }

        public void LoadContent(SpriteFont arialFont, Point menuListPosition)
        {
            this.arialFont = arialFont;
            this.menuListPosition = new Vector2[]{
                menuListPosition.ToVector2(),
                menuListPosition.ToVector2() + stringSpace * scale,
                menuListPosition.ToVector2() - new Vector2(25,0)* scale,
                menuListPosition.ToVector2() + stringSpace * scale - new Vector2(18,0)* scale,
            };
        }

        public int Update(GameTime gameTime, KeyboardState selectMenu, KeyboardState selectMenuPreKey) {
            int state = 0;
            if (selectMenu.IsKeyUp(Keys.Up) && selectMenuPreKey.IsKeyDown(Keys.Up))
            {
                if (menuOne == 2 && menuTwo == 1)
                {
                    menuOne = 0;
                    menuTwo = 3;
                }
                else if (menuOne == 0 && menuTwo == 3)
                {
                    menuOne = 2;
                    menuTwo = 1;
                }
                state = 0;
            }
            else if (selectMenu.IsKeyUp(Keys.Down) && selectMenuPreKey.IsKeyDown(Keys.Down))
            {
                if (menuOne == 2 && menuTwo == 1)
                {
                    menuOne = 0;
                    menuTwo = 3;
                }
                else if (menuOne == 0 && menuTwo == 3)
                {
                    menuOne = 2;
                    menuTwo = 1;
                }
                state = 0;
            }
            else if (selectMenu.IsKeyUp(Keys.Enter) && selectMenuPreKey.IsKeyDown(Keys.Enter))
            {
                if (menuOne == 2 && menuTwo == 1)
                {
                    state = 1;
                }
                else if (menuOne == 0 && menuTwo == 3)
                {
                    state = 2;
                }
            }
            return state;
            }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(menuBackground,startPosition.ToVector2(),null,Color.White,0f,Vector2.Zero, scale, SpriteEffects.None,0f);
            spriteBatch.DrawString(arialFont, menuTexts[menuOne], menuListPosition[menuOne], Color.White, 0f, Vector2.Zero, scale*1.5f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(arialFont, menuTexts[menuTwo], menuListPosition[menuTwo], Color.White, 0f, Vector2.Zero, scale * 1.5f, SpriteEffects.None, 0f);
        }
    }
}
