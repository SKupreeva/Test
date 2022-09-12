using System;
using System.Collections.Generic;
using UnityEngine;

public class EquationController : MonoBehaviour
{
    [SerializeField] private UIController _uiController;

    private Equation _equation;

    private void Awake()
    {
        _equation = new Equation();
    }

    private void Start()
    {
        _uiController.SetButtons();
        _uiController.OnButtonClicked += OnButtonClicked;
        _uiController.InitMainPanel(_equation.Expression);
    }

    private void OnDisable()
    {
        _equation.Expression = _uiController.Expression;
        _uiController.OnButtonClicked -= OnButtonClicked;
    }

    private void TryCalculateEquationResult()
    {
        var equation = _uiController.Expression;
        _equation.Expression = equation;
        if (string.IsNullOrEmpty(equation) || equation.IndexOf('/') <= 0 || equation.IndexOf('/') >= equation.Length - 1)
        {
            _uiController.ShowErrorMessage();
            return;
        }

        var numbers = Array.ConvertAll(equation.Split('/'), p => p.Trim());
        List<long> realNumbers = new List<long>();
        foreach (var number in numbers)
        {
            var value = ConvertToLong(number);
            if (value < 0)
            {
                _uiController.ShowErrorMessage();
                return;
            }
            realNumbers.Add(value);
        }

        if (realNumbers.Count != numbers.Length)
        {
            _uiController.ShowErrorMessage();
            return;
        }

        CalculateEquationResult(realNumbers);
    }

    private long ConvertToLong(string inputText)
    {
        var number = new System.Text.StringBuilder();
        foreach (char character in inputText)
        {
            if (char.IsDigit(character))
            {
                number.Append(character);
            }
            else
            {
                return -1;
            }
        }
        return long.Parse(number.ToString());
    }

    private void CalculateEquationResult(List<long> numbers)
    {
        decimal result = numbers[0];
        for (var i = 1; i < numbers.Count; i++)
        {
            result /= numbers[i];
        }

        _uiController.ShowResult(result.ToString(result % 1 == 0 ? "####" : "F"));
        _equation.Expression = result.ToString(result % 1 == 0 ? "####" : "F");
    }

    private void OnButtonClicked(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Result:
                TryCalculateEquationResult();
                break;
            case ButtonType.ErrorOk:
                _uiController.ShowSorryPanel();
                break;
            case ButtonType.NewEquation:
            {
                _uiController.InitMainPanel(string.Empty);
                _equation.Expression = string.Empty;
            }
                break;
            case ButtonType.Quit:
            {
                _equation.Expression = _uiController.Expression;
                Application.Quit();
            }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "unidentified button");
        }
    }
}
