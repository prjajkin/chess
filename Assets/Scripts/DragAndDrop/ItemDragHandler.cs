using Assets.Scripts;
using Assets.Scripts.Figures;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private Figure targetFigure;
    public bool Dropped { get; set; }
    private Vector3 startPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Dropped = false;
        startPosition = transform.position;
        targetFigure.CanvasGroup.blocksRaycasts = false;
        GameManager.instance.OnStartDraggingFigure(targetFigure);

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Dropped)return;
        SetDefaultPositions();
        GameManager.instance.OnBreakDraggingFigure();
    }

    public void SetDefaultPositions()
    {
        transform.position = startPosition;
        targetFigure.CanvasGroup.blocksRaycasts = true;
    }
}
