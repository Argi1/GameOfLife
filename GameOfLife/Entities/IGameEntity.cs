using Microsoft.Xna.Framework.Graphics;

namespace GameOfLife.Entities
{
    public interface IGameEntity
    {
        void Update();

        void Draw(SpriteBatch spriteBatch);
    }
}
