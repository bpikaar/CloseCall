using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloseCall
{
    class World
    {
        private Car         _car;
        private Counter     _counter;
        private Rock        _rock;
        private Texture2D   _background;

        private string      _score = "";
        private SpriteFont  _spVerdana;

        public World()
        {
            _car        = new Car(new Vector2(0, 172), 0.3f, Color.PaleGreen);
            _counter    = new Counter();
            _rock       = new Rock(new Vector2(626, 178));
            _background = Game1.instance.Content.Load<Texture2D>("cliff");
            _spVerdana  = Game1.instance.Content.Load<SpriteFont>("Verdana");
        }

        public void Update()
        {
            _car.Update();
            _counter.Update();
            _rock.Update();

            //handle counter
            if (_counter.CountDownReady)
            {
                _car.Run();
                _counter.CountDownReady = false;
            }

            // TODO: Add your update logic here
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if(!_counter.Active) _car.Brake();
            }

            if (keyboardState.IsKeyDown(Keys.R))
            {
                Game1.instance.Reset();
            }
            
            // Collision detection 
            if (_car.Bounds.Intersects(_rock.Bounds))
            {
                _rock.Speed     = _car.Speed;
                _car.Speed      = 0;
                _car.Crashed    = true;
                _rock.Bounced   = true;
            }

            // When the road ends, the rock will fall down
            if (!_rock.Bounds.Intersects(new Rectangle(0, 0, 630, Game1.instance.GraphicsDevice.Viewport.Height)))
                _rock.Gravity = 9.78f;

            if (_car.Crashed)
                _score = "You Crashed";
            else
                _score = "Distance: " + (_rock.Position.X - _car.Bounds.Right).ToString();
        }

        public void Draw()
        {
            Game1.spriteBatch.Draw(_background, Game1.instance.GraphicsDevice.Viewport.Bounds, Color.White);
            _car.Draw();
            _counter.Draw();
            _rock.Draw();
            Game1.spriteBatch.DrawString(_spVerdana, _score, new Vector2(185, 70), Color.DarkGreen);
        }
    }
}
