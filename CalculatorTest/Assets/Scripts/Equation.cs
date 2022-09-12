using UnityEngine;

public class Equation
{
    private static string _expressionStringKey = "equationString";

    public string Expression
    {
        get => PlayerPrefs.GetString(_expressionStringKey);
        set
        {
            if (string.Equals(PlayerPrefs.GetString(_expressionStringKey), value))
            {
                return;
            }

            PlayerPrefs.SetString(_expressionStringKey, value);
            PlayerPrefs.Save();
        }
    }
}
