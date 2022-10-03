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

        private static Controller _INSTANCE;
        private Random _random;
        private const int _STARTSPEED = 240;
        private const int _STARTTIME = 2;
        private const double _MINTIME = 0.4;

        private double timer = 2;
        private double maxTime = _STARTTIME;
        private int nextSpeed = _STARTSPEED;
        private bool inGame = false;
        private double totalTime = 0;

        public Controller(Ship player, Text timerUI, Text startMessageUI)
        {
            this._asteroids = new EntityGroup();
            this._player = player;
            this._timerUI = timerUI;
            this._startMessageUI = startMessageUI;
            this._random = new Random();
            _INSTANCE = this;
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
            nextSpeed = _STARTSPEED;

            _timerUI.SetActive(true);
            _player.EnableControls();

            _startMessageUI.SetActive(false);
        }

        public void OnGameover()
        {
            inGame = false;
            _timerUI.SetActive(false);
            _player.DisableControls();
            _player.Reset();

            _startMessageUI.SetActive(true);
            Vector2 sizeOfText = _startMessageUI.MeasureString(_startMessageUI.GetText());
            int halfWidth = Game1.WIDTH / 2;
            _startMessageUI.SetPosition(new Vector2(halfWidth, 200) - sizeOfText / 2);

            _timerUI.SetActive(true);

            _asteroids.Destroy();
        }

        private void ProcessGameplay(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            totalTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (timer <= 0)
            {
                Entity e = null;

                int x = _random.Next(0, 255);

                if (x > 220)
                    e = new Battery(_STARTSPEED).SetSort(1);
                else
                    e = new Asteroid(nextSpeed).SetSort(1);

                _asteroids.Add(e);

                timer = maxTime;

                if (maxTime > _MINTIME)
                    maxTime -= 0.1;

                if (nextSpeed < 720)
                    nextSpeed += 4;
            }


            _timerUI.SetText("Time: " + Math.Floor(this.totalTime).ToString());
        }

        public static void GameOver()
        {
           _INSTANCE.OnGameover();
        }
    }
}
