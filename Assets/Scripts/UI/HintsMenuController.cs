using UnityEngine;
using UnityEngine.UI;

public class HintsMenuController : BaseMenuController
{
    [Header("Hints Menu Variables")]
    [SerializeField]
    private Button hintsButton;

    protected override void Start()
    {
        hintsButton.onClick.AddListener(Open);

        base.Start();
    }
}