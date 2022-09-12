using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _errorMessage;
    [SerializeField] private GameObject _sorryPanel;

    [SerializeField] private TMP_InputField _input;

    [SerializeField] private ButtonTypeController[] _buttons;

    public UnityAction<ButtonType> OnButtonClicked;

    public string Expression => _input.text;

    private void OnDisable()
    {
        foreach (var button in _buttons)
        {
            button.Button.onClick.RemoveAllListeners();
        }
    }

    public void SetButtons()
    {
        foreach (var button in _buttons)
        {
            button.Button.onClick.AddListener(() => OnButtonClicked(button.Type));
        }
    }

    public void InitMainPanel(string equation)
    {
        ChangePanelState(true, false);
        _input.text = equation;
    }

    public void ShowErrorMessage() => ChangePanelState(true, true);
    public void ShowSorryPanel() => ChangePanelState(false, false);

    public void ShowResult(string result)
    {
        _input.text = result;
    }

    private void ChangePanelState(bool isMainActive, bool isErrorActive)
    {
        _mainPanel.SetActive(isMainActive);
        _sorryPanel.SetActive(!isMainActive);
        _errorMessage.SetActive(isErrorActive);
    }
}
