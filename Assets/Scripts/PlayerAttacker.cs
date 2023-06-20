using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] float attackSoundIntensity; 
    SoundMaker soundMaker;
    public bool attack; 

    private void Awake()
    {
        soundMaker = GetComponent<SoundMaker>();
    }
    private void OnAttack(InputValue value)
    {
        if (attack)
            attack = false;
        else
            attack = true;
        soundMaker.TriggerSound(transform, attackSoundIntensity);
        Debug.Log("Attack"); 
    }

    private void OnDrawGizmos()
    {
        if (!attack)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackSoundIntensity);
        }
    }
}
