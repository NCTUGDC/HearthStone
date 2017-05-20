using UnityEngine;
using UnityEngine.EventSystems;

public class AutoZoomHandCardBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = 2 * Vector2.one;
        transform.localPosition += new Vector3(0, 50, 0);
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector2.one;
        transform.localPosition -= new Vector3(0, 50, 0);
    }
}
