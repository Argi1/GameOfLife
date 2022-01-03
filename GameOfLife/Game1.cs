using GameOfLife.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfLife
{
    public class Game1 : Game
    {

        private Texture2D _aliveCellTexture;
        private Texture2D _deadCellTexture;

        private CellListController _cellListController;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MouseState _previousMouseState;
        private KeyboardState _previousKeyboardState;

        private const int _screenWidth = 1280;
        private const int _screenHeight = 720;

        private bool _running;

        private const float _cellUpdateDelay = 500;
        private double _elapsedTime;

        private const int _cellListWidth = 128;
        private const int _cellListHeight = 67;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.ApplyChanges();

            base.Initialize();

            _running = false;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _aliveCellTexture = Content.Load<Texture2D>("green");
            _deadCellTexture = Content.Load<Texture2D>("black");

            _cellListController = new CellListController(_aliveCellTexture, _deadCellTexture);

            _cellListController.LoadCellList(_cellListWidth, _cellListHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            var elapsedTime = gameTime.ElapsedGameTime.TotalMilliseconds;

            _elapsedTime += elapsedTime;

            var mouseState = Mouse.GetState();
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Space) && !_previousKeyboardState.IsKeyDown(Keys.Space))
            {
                _running = !_running;
                _elapsedTime = 0;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton != ButtonState.Pressed)
            {
                var mousePosition = new Point(mouseState.X, mouseState.Y);

                if (GraphicsDevice.Viewport.Bounds.Contains(mousePosition))
                {
                    if (mousePosition.Y < _cellListHeight * 10)
                    {
                        var x = mousePosition.X / 10;
                        var y = mousePosition.Y / 10;

                        _cellListController.InvertCellAtPosition(x, y);
                    }
                }
            }
            if (_running && _elapsedTime >= _cellUpdateDelay)
            {
                _cellListController.Update();

                _elapsedTime = 0;
            }

            _previousMouseState = mouseState;
            _previousKeyboardState = keyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin();
            _cellListController.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}
