using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloseCall
{
    class Car
    {
        private Texture2D   _texture;
        private Wheel       _wheel1;
        private Wheel       _wheel2;
        private bool        _run;
        private bool        _brake;

        public Vector2      Position    { get; set; }
        public Color        Color       { get; set; }
        public float        Speed       { get; set; }
        public int          Direction   { get; set; }
        public float        Scale       { get; set; }
        public float        Gravity     { get; set; }

        public bool         HasStopped  { get; set; }
        public bool         Crashed     { get; set; }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                        (int)Position.X,
                        (int)Position.Y,
                        (int)(_texture.Width * Scale),
                        (int)((_texture.Height + _wheel1.Texture.Height / 2) * Scale));
            }
        }
        public Car(Vector2 position, float scale, Color color)
        {
            _texture        = Game1.instance.Content.Load<Texture2D>("Car_body");
            _run            = false;

            Scale           = scale;
            Position        = position;
            Color           = color;

            _wheel1         = new Wheel(this, new Vector2(67, 62), Color.Yellow);
            _wheel2         = new Wheel(this, new Vector2(212, 62), Color.Yellow);
        }
        public void Update()
        {
            if (_run)
            {
                Position = new Vector2(Position.X + Speed, Position.Y + Gravity);
                _wheel1.Update();
                _wheel2.Update();
            }
            if(_brake)
            {
                Speed -= 0.08f;
                if (Speed < 0.4f) Speed = 0;
            }
        }

        public void Draw()
        {
            Game1.spriteBatch.Draw(_texture, Position, null, Color, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);

            _wheel1.Draw();
            _wheel2.Draw();
        }

        public void Brake()
        {
            _brake = true;
        }

        public void Run()
        {
            Speed = 5;
            _run = true;
        }
    }
}
