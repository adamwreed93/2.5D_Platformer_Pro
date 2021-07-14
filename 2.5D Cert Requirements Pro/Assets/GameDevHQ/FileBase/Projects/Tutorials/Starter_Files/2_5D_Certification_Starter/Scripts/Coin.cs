using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //[SerializeField] private float _rotationSpeed = 5;
    //private bool _rotateCheck = true;

    private Animator _animator;

    [SerializeField] private int _coinType; //1 = Silver, 2 = Purple

    private void Start()
    {
        _animator = GetComponent<Animator>();

        StartCoroutine(AnimateCoin());
    }

    private void FixedUpdate()
    {
        //transform.Rotate(0, _rotationSpeed, 0, Space.World);   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_coinType == 1)
            {
                UIManager.Instance.UpdateSilverCoinUI();
                Destroy(gameObject);
            }
           
            if (_coinType == 2)
            {
                UIManager.Instance.UpdatePurpleCoinUI();
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator AnimateCoin()
    {
        while (true)
        {
            float _randomTime = Random.Range(3, 8);
            yield return new WaitForSeconds(_randomTime);
            _animator.SetBool("AnimateCoin", true);
            yield return new WaitForSeconds(1);
            _animator.SetBool("AnimateCoin", false);
        } 
    }


    /*  Spin Coin Mechanic
    private IEnumerator CoinSpinSpeedModulator()
    {
        while (true)
        {
            if (_rotateCheck == true)
            {
                _rotationSpeed -= .1f;
                yield return new WaitForSeconds(.01f);

                if (_rotationSpeed <= 0)
                {
                    yield return new WaitForSeconds(2.0f);

                    _rotateCheck = false;
                }
            }

            if (_rotateCheck == false)
            {
                _rotationSpeed += .1f;
                yield return new WaitForSeconds(.01f);

                if (_rotationSpeed >= 20)
                {
                    _rotateCheck = true;
                }
            }
        }
    }
    */


}
