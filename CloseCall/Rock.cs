using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloseCall
{
    class Rock
    {
        private Texture2D   _texture;
        private float       _rotation;

        public Vector2      Position    { get; set; }
        public float        Speed       { get; set; }
        public float        Gravity     { get; set; }
        public bool         Bounced     { get; set; }

        public Rectangle    Bounds
        {
            get
            {
                return new Rectangle(
                        (int)(Position.X - _texture.Width / 2),
                        (int)(Position.Y - _texture.Height / 2),
                        _texture.Width,
                        _texture.Height);
            }
        }

        public Rock(Vector2 position)
        {
            Position = position;
            _texture = Game1.instance.Content.Load<Texture2D>("Rock");
        }

        public void Update()
        {
            if (Position.Y < 415)
            {
                Position = new Vector2(Position.X + Speed, Position.Y + Gravity);
                // Als er een botsing is geweest met de rots -> roteren
                // ALs de snelheid voor de botsing te klein is mag de rots niet roteren
                if (Bounced && Speed > 1)
                    _rotation += 0.1f;
            }
        }

        public void Draw()
        {
            Game1.spriteBatch.Draw(
                _texture,
                Position,
                null,
                Color.White,
                _rotation,
                new Vector2(_texture.Width / 2, _texture.Height / 2),
                1,
                SpriteEffects.None,
                0);
        }
    }
}
