using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredSurvivor : MonoBehaviour
{
    Animator _anim;
    


    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player == null)
                return;

            if(player.CheckForMeditKit() == true)
            {
                _anim.SetBool("HasBeenSaved", true);
                GameManager.Instance.EnableWinningCanvas();
            }

            
        }
    }
}
