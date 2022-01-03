using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameOfLife.Entities
{
    public class CellListController
    {
        private List<List<Cell>> _cellList = new List<List<Cell>>();

        private readonly Texture2D _aliveCellTexture;
        private readonly Texture2D _deadCellTexture;

        public CellListController(Texture2D aliveCell, Texture2D deadCell)
        {
            _aliveCellTexture = aliveCell;
            _deadCellTexture = deadCell;
        }

        public void Update()
        {
            for (int x = 0; x < _cellList.Count; x++)
            {
                for (int y = 0; y < _cellList[x].Count; y++)
                {
                    var cell = _cellList[x][y];
                    int aliveCount = 0;

                    foreach (var neighbourIndex in cell.Neighbours)
                    {
                        var neighbourActualX = neighbourIndex.Item1;
                        var neighbourActualY = neighbourIndex.Item2;

                        if(neighbourActualX < 0)
                            neighbourActualX = _cellList.Count - 1;

                        if(neighbourActualY < 0)
                            neighbourActualY = _cellList[x].Count - 1;

                        if (neighbourActualX > _cellList.Count - 1)
                            neighbourActualX = 0;

                        if (neighbourActualY > _cellList[x].Count - 1)
                            neighbourActualY = 0;

                        if (_cellList[neighbourActualX][neighbourActualY].PreviousAlive)
                        {
                            aliveCount++;
                        }
                    }
                    if (!cell.Alive)
                    {
                        if (aliveCount == 3)
                        {
                            _cellList[x][y].setCellState(true);
                        }
                    }
                    else
                    {
                        if (aliveCount < 2)
                        {
                            _cellList[x][y].setCellState(false);
                        }
                        else if (aliveCount > 3)
                        {
                            _cellList[x][y].setCellState(false);
                        }
                    }
                }
            }
            for (int x = 0; x < _cellList.Count; x++)
            {
                for (int y = 0; y < _cellList[x].Count; y++)
                {
                    _cellList[x][y].Update();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < _cellList.Count; x++)
            {
                for (int y = 0; y < _cellList[0].Count; y++)
                {
                    if (_cellList[x][y].Alive)
                    {
                        spriteBatch.Draw(_aliveCellTexture, _cellList[x][y].Area, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(_deadCellTexture, _cellList[x][y].Area, Color.White);
                    }
                }
            }
        }

        public void LoadCellList(int X, int Y)
        {
            for (int x = 0; x < X; x++)
            {
                List<Cell> cellListData = new List<Cell>();
                for (int y = 0; y < Y; y++)
                {
                    cellListData.Add(new Cell(false, new Rectangle(x * 10, y * 10, 10, 10), x, y));
                }

                _cellList.Add(cellListData);
            }
        }
        public void InvertCellAtPosition(int x, int y)
        {
            _cellList[x][y].invertCell();
        }
    }
}
