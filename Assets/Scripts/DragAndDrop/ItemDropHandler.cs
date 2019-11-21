using Assets.Scripts;
using Assets.Scripts.Figures;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField] private BoardCell boardCell;
    public void OnDrop(PointerEventData eventData)
    {
        var pointer = eventData.pointerDrag;
        if (pointer == null) return;
        var dragHandler = pointer.GetComponent<ItemDragHandler>();
        dragHandler.Dropped = true;
        dragHandler.SetDefaultPositions();
        GameManager.instance.OnDroppedFigure(pointer.GetComponent<Figure>(), boardCell.Row, boardCell.Column);
        Debug.Log("Dropped object was: " + pointer);
    }
}
