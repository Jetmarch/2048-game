using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PushButton : MonoBehaviour
{
    private int pushCount = 0;
    [SerializeField] private TextMeshProUGUI pushText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Push()
    {
        pushCount++;

        pushText.text = $"PUSH COUNT: {pushCount}";
    }
}
