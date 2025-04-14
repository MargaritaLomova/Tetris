using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private CanvasGroup panelCanvasGroup;

    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Button retryButton;

    private void Start()
    {
        pauseButton.onClick.AddListener(Open);

        closeButton.onClick.AddListener(Close);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        retryButton.onClick.AddListener(OnRetryButtonClicked);

        Close();
    }

    private void Open()
    {
        panelCanvasGroup.alpha = 1;
        panelCanvasGroup.interactable = true;
        panelCanvasGroup.blocksRaycasts = true;

        Time.timeScale = 0;
    }

    private void Close()
    {
        panelCanvasGroup.alpha = 0;
        panelCanvasGroup.interactable = false;
        panelCanvasGroup.blocksRaycasts = false;

        Time.timeScale = 1;
    }

    private void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnSettingsButtonClicked()
    {

    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

    private void OnRetryButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
}