using System.Collections.Generic;
using UnityEngine;

public class CodeLockMain : MonoBehaviour
{

    [SerializeField] private Door targetDoor;
    [SerializeField] private TextMesh codeLockText;

    [SerializeField] private List<int> theCode = new List<int>();
    [SerializeField] private List<int> currentCode = new List<int>();

    public void AddNumberToCode(int number)
    {
        if (currentCode.Count < theCode.Count)
        {
            codeLockText.text += number.ToString();
            currentCode.Add(number);
        }
        if (currentCode.Count == theCode.Count)
        {
            if (CheckTheCode())
            {
                Debug.Log("Opening Door!");
                targetDoor.OpenDoor();
            }
            else
            {
                ClearTheCode();
            }
        }
    }

    private bool CheckTheCode()
    {
        for (int i = 0; i < theCode.Count; i++)
        {
            if (theCode[i] != currentCode[i])
            {
                return false;
            }
        }
        return true;
    }

    private void ClearTheCode()
    {
        currentCode.Clear();
        codeLockText.text = "";
    }

}
