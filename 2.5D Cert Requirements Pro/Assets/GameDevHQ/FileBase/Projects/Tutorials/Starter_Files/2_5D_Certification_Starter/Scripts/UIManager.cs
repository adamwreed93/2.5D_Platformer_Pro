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
    [SerializeField] private GameObject _winGameText;
    [SerializeField] private GameObject _masterWinGameText;
    [SerializeField] private GameObject _restartGameText;
    [SerializeField] private GameObject _silverCoinChallengeText;
    [SerializeField] private GameObject _purpleCoinChallengeText;

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

    public void WinGame()
    {
        if (_silverCoinCount < 23)
        {
            _winGameText.SetActive(true);
            _silverCoinChallengeText.SetActive(true);
            StartCoroutine(TextFlash());
        }

        if (_silverCoinCount == 23 && _purpleCoinCount < 4)
        {
            _winGameText.SetActive(true);
            _purpleCoinChallengeText.SetActive(true);
            StartCoroutine(TextFlash());
        }

        if (_silverCoinCount == 23 && _purpleCoinCount == 4)
        {
            _masterWinGameText.SetActive(true);
            StartCoroutine(TextFlash());
        }

        _restartGameText.SetActive(true);
    }

    private IEnumerator TextFlash()
    {
        Text restartText = _restartGameText.GetComponent<Text>();
        while (true)
        {
            restartText.enabled = true;
            yield return new WaitForSeconds(.5f);
            restartText.enabled = false;
            yield return new WaitForSeconds(.5f);
        }
    }
}
