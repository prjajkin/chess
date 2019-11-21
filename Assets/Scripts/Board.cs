using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private BoardRow[] boardRows = new BoardRow[8];

        public void Initialize()
        {
            for (var row = 0; row < boardRows.Length; row++)
            {
                var boardRow = boardRows[row];
                for (var column = 0; column < boardRow.Cells.Length; column++)
                {
                    var cell = boardRow.Cells[column];
                    cell.Initialize(row, column);
                }
            }
        }

        public BoardCell GetCell(int row, int column) => boardRows[row].Cells[column];
        public Vector3 GetCellPosition(int row, int column) => boardRows[row].Cells[column].transform.position;

        public void HighlightBoard(BoardCellState[,] movingMap)
        {

            for (var row = 0; row < 8; row++)
            {
                for (var column = 0; column < 8; column++)
                {
                    GetCell(row, column).SetCellState(movingMap[row, column]);
                }
            }
        }

        public void UnHighlightBoard()
        {
            for (var row = 0; row < 8; row++)
            {
                for (var column = 0; column < 8; column++)
                {
                    GetCell(row, column).SetCellState(BoardCellState.NotAvailable);
                }
            }
        }
    }
}




