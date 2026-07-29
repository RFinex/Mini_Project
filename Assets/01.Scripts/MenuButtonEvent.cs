using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform arrow;
    private RectTransform rect;

    [SerializeField] private float arrowXPos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        arrow = UIManager.instance.EventArrow;
    }

    private void GetArrow()
    {
        if (UIManager.instance != null)
        {
            arrow = UIManager.instance.EventArrow;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (arrow == null)
        {
            Debug.Log("arrow Null");
            GetArrow();
        }

        arrow.gameObject.SetActive(true);

        Vector2 arrowPos = rect.anchoredPosition;
        arrowPos.x += arrowXPos;
        arrow.anchoredPosition = arrowPos;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (arrow == null)
            return;

        arrow.gameObject.SetActive(false);
    }
}
