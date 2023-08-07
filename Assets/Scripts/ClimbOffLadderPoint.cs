using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbOffLadderPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ladder_Detector"))
        {
            var player = other.GetComponentInParent<Player>();

            if (player == null)
                return;

            player.GettingOffLadder();

        }
    }
}
