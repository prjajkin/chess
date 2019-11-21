using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BoardRow : MonoBehaviour
    {
        [SerializeField] private BoardCell[] cells = new BoardCell[8];
        
        public BoardCell[] Cells => cells;
    }
}
