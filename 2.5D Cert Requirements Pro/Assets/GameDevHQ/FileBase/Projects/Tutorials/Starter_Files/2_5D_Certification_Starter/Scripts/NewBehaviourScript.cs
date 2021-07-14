using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private int _a = 1;
    [SerializeField] private int _b = 1;
    private int _c;

    [SerializeField] private int[] _fibonachiNumbers;
    private int _currentA = 0;
    private int _currentB = 1;
    private int _currentC = 2;


    void Start()
    {
        _c = _a + _b;
        _fibonachiNumbers[_currentA] = _a;
        _fibonachiNumbers[_currentB] = _b;
        _fibonachiNumbers[_currentC] = _c;

        for (int i = 0; i < 100; i++)
        {
            _currentA++;
            _currentB++;
            _currentC++;

            _a = _b;
            _b = _c;
            _c = _a + _b;

            _fibonachiNumbers[_currentA] = _a;
            _fibonachiNumbers[_currentB] = _b;
            _fibonachiNumbers[_currentC] = _c;

            Debug.Log(_c);
        }
    }
}
