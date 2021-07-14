using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField] private GameObject _handPose, _standPos; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge_Grab_Checker"))
        {
            var player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.GrabLedge(_handPose, this);
            }
        }
    }

    public Vector3 GetStandPos()
    {
        return _standPos.transform.position;
    }
}
