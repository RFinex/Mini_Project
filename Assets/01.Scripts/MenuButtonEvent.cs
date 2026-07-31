using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform arrow;
    private RectTransform rect;

    [SerializeField] private float popupSize;
    [SerializeField] private float normalSize;
    [SerializeField] private float popupDelay;

    private Tween tween;

    [SerializeField] private float arrowXPos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        StartCoroutine(lateStart());
    }

    private IEnumerator lateStart()
    {
        yield return null;
        yield return null;

        GetArrow();
    }

    private void GetArrow()
    {
        if (UIManager.instance != null)
        {
            arrow = UIManager.instance.EventArrow;
        }
    }

    private void ButtonEnter()
    {
        tween?.Kill();

        tween = transform.DOScale(popupSize, popupDelay)
            .SetLink(gameObject);
    }

    private void ButtonExit()
    {
        tween?.Kill();

        tween = transform.DOScale(normalSize, popupDelay)
            .SetLink(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (arrow == null)
        {
            Debug.Log("arrow Null");
            GetArrow();
        }

        ButtonEnter();
        arrow.gameObject.SetActive(true);

        Vector2 arrowPos = rect.anchoredPosition;
        arrowPos.x += arrowXPos;
        arrow.anchoredPosition = arrowPos;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (arrow == null)
            return;

        ButtonExit();
        arrow.gameObject.SetActive(false);
    }
}
