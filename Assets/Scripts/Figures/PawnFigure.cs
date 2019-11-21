using System;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Figures
{
    public class PawnFigure : Figure
    { 
        public override BoardCellState[,] GetMovingMap(Figure[,] figureMap)
        {
            var movingMap = new BoardCellState[8, 8];
            if (isWhite)
            {
                if (Row <= 0) return movingMap;
                if (Column > 0) movingMap[Row - 1, Column - 1] = CheckAttackCellState(figureMap[Row - 1, Column - 1]);
                movingMap[Row - 1, Column] = CheckMovedCellState(figureMap[Row - 1, Column]);
                if (Column < 7) movingMap[Row - 1, Column + 1] = CheckAttackCellState(figureMap[Row - 1, Column + 1]);
                if (!FirstStepMade) movingMap[Row - 2, Column] = CheckMovedCellState(figureMap[Row - 2, Column]); 
            }
            else
            {
                if (Row >= 7) return movingMap;
                if (Column > 0) movingMap[Row + 1, Column - 1] = CheckAttackCellState(figureMap[Row + 1, Column - 1]);
                movingMap[Row + 1, Column] = CheckMovedCellState(figureMap[Row + 1, Column]);
                if (Column < 7) movingMap[Row + 1, Column + 1] = CheckAttackCellState(figureMap[Row + 1, Column + 1]);
                if (!FirstStepMade) movingMap[Row + 2, Column] = CheckMovedCellState(figureMap[Row + 2, Column]);
            }

            return movingMap;
        } 

        private BoardCellState CheckAttackCellState(Figure comparedFigure)
        {
            if (comparedFigure != null && comparedFigure.IsWhite != isWhite) return BoardCellState.AttackAvailable;
            return BoardCellState.NotAvailable;
        }

        private BoardCellState CheckMovedCellState(Figure comparedFigure)
        {
            return comparedFigure == null ? BoardCellState.Available : BoardCellState.NotAvailable;
        }
    }
}
