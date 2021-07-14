using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    [SerializeField] private int _climbLadderPosition; //1 = bottom. 2 = top.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (_climbLadderPosition == 1)
            {
                player.LadderClimbEnter(1);
            }
            else if (_climbLadderPosition == 2)
            {
                player.LadderClimbEnter(2);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.LadderClimbExit();
        }
    }
}