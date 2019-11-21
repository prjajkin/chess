using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Figures
{
    public class RookFigure : Figure
    {
        public override BoardCellState[,] GetMovingMap(Figure[,] figureMap)
        {
            var movingMap = new BoardCellState[8, 8];

            //Check Up.
            var stepRow = Row;
            var stepColumn = Column;
            while (stepRow > 0)
            {
                stepRow--;
                movingMap[stepRow, stepColumn] = GetCellState(figureMap[stepRow, stepColumn]);
                if (movingMap[stepRow, stepColumn] != BoardCellState.Available) break;
            }

            //Check Right.
            stepRow = Row;
            stepColumn = Column;
            while (stepColumn < 7)
            { 
                stepColumn++;
                movingMap[stepRow, stepColumn] = GetCellState(figureMap[stepRow, stepColumn]);
                if (movingMap[stepRow, stepColumn] != BoardCellState.Available) break;
            }

            //Check Left.
            stepRow = Row;
            stepColumn = Column;
            while (  stepColumn > 0)
            { 
                stepColumn--;
                movingMap[stepRow, stepColumn] = GetCellState(figureMap[stepRow, stepColumn]);
                if (movingMap[stepRow, stepColumn] != BoardCellState.Available) break;
            }

            //Check Down.
            stepRow = Row;
            stepColumn = Column;
            while (stepRow < 7  )
            {
                stepRow++; 
                movingMap[stepRow, stepColumn] = GetCellState(figureMap[stepRow, stepColumn]);
                if (movingMap[stepRow, stepColumn] != BoardCellState.Available) break;
            }

            return movingMap;
        }
    }
}
