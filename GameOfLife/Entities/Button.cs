using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameOfLife.Entities
{
    class Button : IGameEntity
    {
        public Vector2 Position { get; set; }

        private string _name;

        private SpriteFont _font;

        private MouseState _previousMouseState;
        private MouseState _mouseState;

        private bool _isHovering;

        private Texture2D _texture;

        public Button(Vector2 position, string name, SpriteFont font, MouseState previousMouseState, MouseState mouseState, bool isHovering, Texture2D texture)
        {
            Position = position;
            _name = name;
            _font = font;
            _previousMouseState = previousMouseState;
            _mouseState = mouseState;
            _isHovering = isHovering;
            _texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
