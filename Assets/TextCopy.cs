using UnityEngine;
using TMPro;

public class TextCopy : MonoBehaviour
{
    public TMP_Text[] target;
    public TMP_Text[] mine;

    private void OnEnable()
    {
        FromTargetToMine();
    }

    void FromTargetToMine()
    {
        if (!target.Length.Equals(mine.Length))
        {
            return;
        }

        for (int i = 0; i < mine.Length; i++)
        {
            mine[i].text = target[i].text;
        }
    }
}
