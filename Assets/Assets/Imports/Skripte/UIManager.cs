using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerzzzzzz : MonoBehaviour
{
    [SerializeField]
    private Text textCount;
    private int killCount;

    private static UIManagerzzzzzz _instance;
    public static UIManagerzzzzzz Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        killCount = 0;
    }

    public void UnitKilled()
    {
        killCount++;
        textCount.text = killCount.ToString();
    }

}
