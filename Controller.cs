using LeoTheLegion.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public class Controller
    {
        private EntityGroup _asteroids;

        private Ship _player;
        private Text _timerUI, _startMessageUI;

        public double timer = 2;
        public double maxTime = 2;
        public int nextSpeed = 240;
        public bool inGame = false;
        public double totalTime = 0;

        public Controller(Ship player, Text timerUI, Text startMessageUI)
        {
            this._asteroids = new EntityGroup();
            this._player = player;
            this._timerUI = timerUI;
            this._startMessageUI = startMessageUI;
        }

        public void Update(GameTime gameTime)
        {
            if(inGame)
            {
                ProcessGameplay(gameTime);
            }
            else
            {
                KeyboardState kState = Keyboard.GetState();

                if (kState.IsKeyDown(Keys.Enter))
                {
                    OnStartGameplay();
                }

            }
        }

        public void OnStartGameplay()
        {
            inGame = true;
            totalTime = 0;
            timer = 2;
            maxTime = 2;
            nextSpeed = 240;

            _timerUI.SetActive(true);
            _player.EnableControls();

            _startMessageUI.SetActive(false);
        }

        public void OnGameover()
        {
            inGame = false;
            _timerUI.SetActive(false);
            _player.DisableControls();
            _player.ResetPosition();

            _startMessageUI.SetActive(true);
            Vector2 sizeOfText = _startMessageUI.MeasureString(_startMessageUI.GetText());
            int halfWidth = Game1.WIDTH / 2;
            _startMessageUI.SetPosition(new Vector2(halfWidth, 200) - sizeOfText / 2);

            _timerUI.SetActive(true);
        }

        private void ProcessGameplay(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            totalTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (timer <= 0)
            {
                _asteroids.Add(new Asteroid(nextSpeed).SetSort(1));
                timer = maxTime;

                if (maxTime > 0.5)
                    maxTime -= 0.1;

                if (nextSpeed < 720)
                    nextSpeed += 4;
            }


            _timerUI.SetText("Time: " + Math.Floor(this.totalTime).ToString());
        }

        
    }
}
