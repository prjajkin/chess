using Assets.Scripts.Figures;
using Assets.Scripts.UI;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts
{
    [SerializeField]
    public class FiguresMap
    {
        private Figure[,] figureMap = new Figure[8, 8];
        private Board board => GameManager.instance.Board;
        private Transform figuresParent;

        public FiguresMap(Transform figuresParent)
        {
            this.figuresParent = figuresParent;
        }

        public BoardCellState[,] GetMovingMap(int row, int column)
        {
            return figureMap[row, column]?.GetMovingMap(figureMap);
        }

        public void AddFigureOnBoard(GameObject prefab, int row, int column)
        {
            var figure = PrefabsUtility.InstantiatePrefab<Figure>(prefab, figuresParent, board.GetCellPosition(row, column));
            figure.SetPosition(row, column);
            figureMap[row, column] = figure;
        }

        public void BlockAllFiguresOnBoard()
        {
            for (var row = 0; row < 8; row++)
            {
                for (var column = 0; column < 8; column++)
                {
                    if (figureMap[row, column] == null) continue;
                    figureMap[row, column].CanvasGroup.blocksRaycasts = false;
                }
            }
        }

        public void BlockFiguresByColor(bool blockWhite)
        {
            for (var row = 0; row < 8; row++)
            {
                for (var column = 0; column < 8; column++)
                {
                    if (figureMap[row, column] == null) continue;
                    figureMap[row, column].CanvasGroup.blocksRaycasts = figureMap[row, column].IsWhite != blockWhite;
                }
            }
        }

        public void MoveFigure(Figure movedFigure, int targetRow, int targetColumn)
        {
            var targetFigure = figureMap[targetRow, targetColumn];

            if (targetFigure != null)
            {
                if (targetFigure.IsWhite == movedFigure.IsWhite)
                {
                    Debug.LogError("Wrong Algorithm for moving of figures.");
                    return;
                }

                figureMap[targetRow, targetColumn] = null;
                Object.Destroy(targetFigure.gameObject);
            }

            figureMap[targetRow, targetColumn] = movedFigure;
            figureMap[movedFigure.Row, movedFigure.Column] = null;
            movedFigure.SetPosition(targetRow, targetColumn);
            movedFigure.Move(board.GetCellPosition(targetRow, targetColumn), GameManager.instance.NextStep);
        }
    }
}

