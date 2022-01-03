using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameOfLife.Entities
{
    public class Cell : IGameEntity
    {
        public bool Alive { get; private set; }
        public bool PreviousAlive { get; private set; }

        public Rectangle Area { get; private set; }

        public List<Tuple<int, int>> Neighbours { get; private set; }

        public Cell(bool alive, Rectangle area, int xIndex, int yIndex)
        {
            Alive = alive;
            PreviousAlive = alive;

            Area = area;

            Neighbours = new List<Tuple<int, int>>();

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x != 0 || y != 0)
                    {
                        Neighbours.Add(new Tuple<int, int>(xIndex + x, yIndex + y));
                    }
                }
            }
        }

        public void Update()
        {
            PreviousAlive = Alive;
        }

        public void invertCell()
        {
            PreviousAlive = !Alive;
            Alive = !Alive;
        }

        public void setCellState(bool state)
        {
            PreviousAlive = Alive;
            Alive = state;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
