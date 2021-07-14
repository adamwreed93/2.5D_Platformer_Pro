using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is NULL!");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    [SerializeField] private Text _silverCoinText;
    [SerializeField] private Text _purpleCoinText;

    private int _silverCoinCount;
    private int _purpleCoinCount;


    public void UpdateSilverCoinUI()
    {
        _silverCoinCount++;
        _silverCoinText.text = ("x " + _silverCoinCount);
    }

    public void UpdatePurpleCoinUI()
    {
        _purpleCoinCount++;
        _purpleCoinText.text = ("x " + _purpleCoinCount);
    }
}
