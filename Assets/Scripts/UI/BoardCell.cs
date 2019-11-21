using System;
using Assets.Scripts.UI.Elements;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BoardCell : MonoBehaviour
    {
        [SerializeField] private MultiImage frameMultiImage;
        [SerializeField] private CanvasGroup canvasGroup;
        public int Row { get; private set; }
        public int Column { get; private set; }

        public void Initialize(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void SetCellState(BoardCellState cellState)
        {
            canvasGroup.blocksRaycasts = cellState!= BoardCellState.NotAvailable;
            switch (cellState)
            {
                case BoardCellState.NotAvailable:
                    frameMultiImage.ChangeColor(0);
                    break;
                case BoardCellState.Available:
                case BoardCellState.CastleAvailable:
                    frameMultiImage.ChangeColor(1);
                    break;
                case BoardCellState.AttackAvailable:
                    frameMultiImage.ChangeColor(2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellState), cellState, null);
            }
            
        }
    }

    [Flags]
    public enum BoardCellState
    {
        NotAvailable= 0,
        Available = 1,
        AttackAvailable = 2,
        CastleAvailable = 4,
    }
}
