using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : BaseMenuController
{
    [Header("Pause Menu Variables")]
    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Button retryButton;

    protected override void Start()
    {
        pauseButton.onClick.AddListener(Open);

        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        retryButton.onClick.AddListener(OnRetryButtonClicked);

        base.Start();
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