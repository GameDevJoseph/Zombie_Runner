using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] Transform _climbLadderPosition;
    [SerializeField] Vector3 _climbPositionOffset;

    [SerializeField] Transform _finalStandPosition;
    [SerializeField] Vector3 _finalStandPositionOffset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder_Detector"))
        {
            var player = other.GetComponentInParent<Player>();

            if (player == null)
                return;



            player.SetLadderGrabPosition(_climbLadderPosition.position + _climbPositionOffset, _finalStandPosition.position + _finalStandPositionOffset);

        }
    }
}
