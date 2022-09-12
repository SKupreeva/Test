using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    Result,
    ErrorOk,
    NewEquation,
    Quit
}

[RequireComponent(typeof(Button))]
public class ButtonTypeController : MonoBehaviour
{
    [SerializeField] private ButtonType _type;

    public Button Button { get; private set; }
    public ButtonType Type => _type;

    private void Awake()
    {
        Button = GetComponent<Button>();
    }
}
