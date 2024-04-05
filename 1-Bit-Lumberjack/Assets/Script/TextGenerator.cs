using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextGenerator : Singleton<TextGenerator>
{
    [SerializeField] GameObject textObj;
    [SerializeField] Transform textParent;
    [SerializeField] float moveY;

    public void GenerateText(int text)
    {
        GenerateText(text.ToString());
    }

    public void GenerateText(string text)
    {
        GameObject tmpObj = Instantiate(textObj, textParent);
        TextMeshProUGUI tmp = tmpObj.GetComponent<TextMeshProUGUI>();
        tmp.text = text;

        LeanTween.moveLocalY(tmpObj, moveY, 0.5f)
            .setEaseInOutCubic()
            .setOnComplete(() => Destroy(tmpObj));
    }

}
