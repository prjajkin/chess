using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Figures
{
    public class KnightFigure : Figure
    {
        public override BoardCellState[,] GetMovingMap(Figure[,] figureMap)
        {
            var movingMap = new BoardCellState[8, 8];
            if (Row > 0)
            {
                if (Column < 6) movingMap[Row - 1, Column + 2] = GetCellState(figureMap[Row - 1, Column + 2]);
                if (Column > 1) movingMap[Row - 1, Column - 2] = GetCellState(figureMap[Row - 1, Column - 2]);
                if (Row > 1)
                {
                    if (Column < 7) movingMap[Row - 2, Column + 1] = GetCellState(figureMap[Row - 2, Column + 1]);
                    if (Column > 0) movingMap[Row - 2, Column - 1] = GetCellState(figureMap[Row - 2, Column - 1]);
                }
            }
            if (Row < 7)
            {
                if (Column < 6) movingMap[Row + 1, Column + 2] = GetCellState(figureMap[Row + 1, Column + 2]);
                if (Column > 1) movingMap[Row + 1, Column - 2] = GetCellState(figureMap[Row + 1, Column - 2]);
                if (Row < 6)
                {
                    if (Column > 0) movingMap[Row + 2, Column - 1] = GetCellState(figureMap[Row + 2, Column - 1]);
                    if (Column < 7) movingMap[Row + 2, Column + 1] = GetCellState(figureMap[Row + 2, Column + 1]);
                }
            }

            return movingMap;
        }
    }
}
