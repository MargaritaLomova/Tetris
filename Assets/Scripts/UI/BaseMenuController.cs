using UnityEngine;
using UnityEngine.UI;

public class BaseMenuController : MonoBehaviour
{
    [Header("Base Menu Variables")]
    [SerializeField]
    protected CanvasGroup menuCanvasGroup;

    [SerializeField]
    protected Button closeButton;

    protected virtual void Start()
    {
        closeButton.onClick.AddListener(Close);

        Close();
    }

    public void Open()
    {
        menuCanvasGroup.alpha = 1;
        menuCanvasGroup.interactable = true;
        menuCanvasGroup.blocksRaycasts = true;

        Time.timeScale = 0;
    }

    public void Close()
    {
        menuCanvasGroup.alpha = 0;
        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;

        Time.timeScale = 1;
    }
}