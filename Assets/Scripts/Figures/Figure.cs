using System;
using Assets.Scripts.UI;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Figures
{
    public abstract class Figure : MonoBehaviour
    {
        [SerializeField] protected bool isWhite;
        [SerializeField] private CanvasGroup canvasGroup;

        public int Row { get; private set; }
        public int Column { get; private set; }
        public bool IsWhite=> isWhite;
        public CanvasGroup CanvasGroup =>canvasGroup;

        public abstract BoardCellState[,] GetMovingMap(Figure[,] figureMap);

        public bool FirstStepMade { get; private set; }

        public void SetPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public virtual void Move( Vector3 position, Action onComplete)
        {
            transform.DOMove(position, 0.5f).
                OnComplete(() => onComplete?.Invoke());
            FirstStepMade = true;
        }

        protected BoardCellState GetCellState(Figure comparedFigure)
        {
            if (comparedFigure == null)
            {
                return BoardCellState.Available;
            }

            return comparedFigure.IsWhite != isWhite
                ? BoardCellState.AttackAvailable
                : BoardCellState.NotAvailable;
        }
    }
}
