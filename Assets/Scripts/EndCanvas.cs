using TMPro;
using UnityEngine;

public class EndCanvas : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI endText;

    public void Open(bool fail)
    {
        if(fail)
            endText.text = "FAILED";
        else
            endText.text = "YOU WIN";
            
        panel.SetActive(true);
    }

    // Button method
    public void TryAgain()
    {
        LoadManager.Instance.Load();
    }
}
