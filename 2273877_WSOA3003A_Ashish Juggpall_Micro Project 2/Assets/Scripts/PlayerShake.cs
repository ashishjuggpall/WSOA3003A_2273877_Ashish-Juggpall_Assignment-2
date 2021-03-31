using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShake : MonoBehaviour
{
    public Animator PlayerAnim;

    public void PlayerShaker()
    {
        PlayerAnim.SetTrigger("PlayerShake");
    }
}
