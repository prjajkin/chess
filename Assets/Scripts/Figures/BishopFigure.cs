using Assets.Scripts.UI;

namespace Assets.Scripts.Figures
{
    public class BishopFigure : Figure
    {
        public override BoardCellState[,] GetMovingMap(Figure[,] figureMap)
        {
            var movingMap = new BoardCellState[8, 8];

            //Check Up-Left.
            var stepRow = Row;
            var stepColumn = Column;
            while (stepRow > 0 && stepColumn > 0)
            {
                stepRow--;
                stepColumn--;
                movingMap[stepRow, stepColumn] = GetCellState( figureMap[stepRow, stepColumn]);
                if (movingMap[stepRow, stepColumn] != BoardCellState.Available) break;
            }

            //Check Up-Right.
            stepRow = Row;
            stepColumn = Column;
            while (stepRow > 0 && stepColumn < 7)
            {
                stepRow--;
                stepColumn++;
                movingMap[stepRow, stepColumn] = GetCellState( figureMap[stepRow, stepColumn]);
                if (movingMap[stepRow, stepColumn] != BoardCellState.Available) break;
            }

            //Check Down-Left.
            stepRow = Row;
            stepColumn = Column;
            while (stepRow < 7 && stepColumn > 0)
            {
                stepRow++;
                stepColumn--;
                movingMap[stepRow, stepColumn] = GetCellState( figureMap[stepRow, stepColumn]);
                if (movingMap[stepRow, stepColumn] != BoardCellState.Available) break;
            }

            //Check Down-Right.
            stepRow = Row;
            stepColumn = Column;
            while (stepRow < 7 && stepColumn < 7)
            {
                stepRow++;
                stepColumn++;
                movingMap[stepRow, stepColumn] = GetCellState( figureMap[stepRow, stepColumn]);
                if (movingMap[stepRow, stepColumn] != BoardCellState.Available) break;
            }

            return movingMap;
        }


    }
}
