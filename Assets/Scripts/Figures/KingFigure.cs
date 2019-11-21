using System;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Figures
{
    public class KingFigure : Figure
    { 
        public override BoardCellState[,] GetMovingMap(Figure[,] figureMap)
        {
            var movingMap = new BoardCellState[8, 8];

            if (Row > 0)
            {
                if (Column < 7)movingMap[Row - 1, Column + 1] = GetCellState(figureMap[Row - 1, Column + 1]);
                movingMap[Row - 1, Column] = GetCellState(figureMap[Row - 1, Column]);
                if (Column > 0)movingMap[Row - 1, Column - 1] = GetCellState(figureMap[Row - 1, Column - 1]);
            }
            if (Column > 0) movingMap[Row, Column - 1] = GetCellState(figureMap[Row, Column - 1]);
            if (Column < 7) movingMap[Row, Column + 1] = GetCellState(figureMap[Row, Column + 1]);
            if (Row < 7)
            {
                if (Column < 7)movingMap[Row + 1, Column + 1] = GetCellState(figureMap[Row + 1, Column + 1]);
                movingMap[Row + 1, Column] = GetCellState(figureMap[Row + 1, Column]);
                if (Column > 0)movingMap[Row + 1, Column - 1] = GetCellState(figureMap[Row + 1, Column - 1]);
            }

            return movingMap;
        }

        public bool CheckCheckmate(Figure[,] figureMap)
        {
            
            //Check Up.
            var stepRow = Row;
            var stepColumn = Column;
            while (stepRow > 0)
            {
                stepRow--;

                if(CheckmateByQueenRook(figureMap[stepRow, stepColumn], out var cellState)) return true;
                if (cellState != BoardCellState.Available) break;
            }

            //Check Right.
            stepRow = Row;
            stepColumn = Column;
            while (stepColumn < 7)
            {
                stepColumn++;

                if (CheckmateByQueenRook(figureMap[stepRow, stepColumn], out var cellState)) return true;
                if (cellState != BoardCellState.Available) break;
            }

            //Check Left.
            stepRow = Row;
            stepColumn = Column;
            while (stepColumn > 0)
            {
                stepColumn--;

                if (CheckmateByQueenRook(figureMap[stepRow, stepColumn], out var cellState)) return true;
                if (cellState != BoardCellState.Available) break;
            }

            //Check Down.
            stepRow = Row;
            stepColumn = Column;
            while (stepRow < 7)
            {
                stepRow++;

                if (CheckmateByQueenRook(figureMap[stepRow, stepColumn], out var cellState)) return true;
                if (cellState != BoardCellState.Available) break;
            }

            //Check Up-Left.
            stepRow = Row;
            stepColumn = Column;
            while (stepRow > 0 && stepColumn > 0)
            {
                stepRow--;
                stepColumn--;

                if (CheckmateByQueenBishop(figureMap[stepRow, stepColumn], out var cellState)) return true;
                if (cellState != BoardCellState.Available) break;
            }

            //Check Up-Right.
            stepRow = Row;
            stepColumn = Column;
            while (stepRow > 0 && stepColumn < 7)
            {
                stepRow--;
                stepColumn++;

                if (CheckmateByQueenBishop(figureMap[stepRow, stepColumn], out var cellState)) return true;
                if (cellState != BoardCellState.Available) break;
            }

            //Check Down-Left.
            stepRow = Row;
            stepColumn = Column;
            while (stepRow < 7 && stepColumn > 0)
            {
                stepRow++;
                stepColumn--;

                if (CheckmateByQueenBishop(figureMap[stepRow, stepColumn], out var cellState)) return true;
                if (cellState != BoardCellState.Available) break;
            }

            //Check Down-Right.
            stepRow = Row;
            stepColumn = Column;
            while (stepRow < 7 && stepColumn < 7)
            {
                stepRow++;
                stepColumn++;

                if (CheckmateByQueenBishop(figureMap[stepRow, stepColumn], out var cellState)) return true;
                if (cellState != BoardCellState.Available) break;
            }

            //Check Pawns and Knights.
            if (Row > 0)
            {
                if (Column < 7)
                {
                    if (isWhite && CheckmateByPawn(figureMap[Row - 1, Column + 1])) return true;
                    if (Column < 6 && CheckmateByKnight(figureMap[Row - 1, Column + 2]))return true;
                }

                if (Column > 0)
                {
                    if (isWhite && CheckmateByPawn(figureMap[Row-1, Column-1])) return true;
                    if (Column > 1 && CheckmateByKnight(figureMap[Row - 1, Column - 2]))return true;
                }

                if (Row > 1)
                {
                    if (Column < 7 && CheckmateByKnight(figureMap[Row - 2, Column + 1]))return true; 
                    if (Column > 0 && CheckmateByKnight(figureMap[Row - 2, Column - 1]))return true; 
                }
            }
            if (Row < 7)
            {
                if (Column < 7)
                {
                    if (isWhite && CheckmateByPawn(figureMap[Row + 1, Column + 1])) return true;
                    if (Column < 6 && CheckmateByKnight(figureMap[Row + 1, Column + 2])) return true;
                    
                }

                if (Column > 0)
                {
                    if (isWhite && CheckmateByPawn(figureMap[Row + 1, Column - 1])) return true;
                    if (Column > 1 && CheckmateByKnight(figureMap[Row + 1, Column - 2]))return true;
                }

                if (Row < 6)
                {
                    if (Column > 0 && CheckmateByKnight(figureMap[Row + 2, Column - 1]))return true; ;
                    if (Column < 7 && CheckmateByKnight(figureMap[Row + 2, Column + 1]))return true; ;
                }
            }

            return false;
        }

        private bool CheckmateByKnight(Figure figureCell)
        {
            var cellState = GetCellState(figureCell);
            var figureType = figureCell?.GetType();
            return cellState == BoardCellState.AttackAvailable && figureType == typeof(KnightFigure);
        }
        private bool CheckmateByPawn(Figure figureCell)
        {
            var cellState = GetCellState(figureCell);
            var figureType = figureCell?.GetType();
            return cellState == BoardCellState.AttackAvailable && figureType == typeof(PawnFigure);
        }
        private bool CheckmateByQueenRook(Figure figureCell, out BoardCellState cellState)
        {
            cellState = GetCellState(figureCell);
            var figureType = figureCell?.GetType();
            return cellState == BoardCellState.AttackAvailable &&
                   (figureType == typeof(QueenFigure) || figureType == typeof(RookFigure));
        }

        private bool CheckmateByQueenBishop(Figure figureCell, out BoardCellState cellState)
        {
            cellState = GetCellState(figureCell);
            var figureType = figureCell?.GetType();
            return cellState == BoardCellState.AttackAvailable &&
                   (figureType == typeof(QueenFigure) || figureType == typeof(BishopFigure)) ;
        }
        
        void OnDestroy()
        {
            GameManager.instance.GameEnd();
        }
    }
}
