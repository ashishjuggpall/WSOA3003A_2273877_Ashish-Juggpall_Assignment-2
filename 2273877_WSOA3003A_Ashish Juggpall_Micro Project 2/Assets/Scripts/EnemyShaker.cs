using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShaker : MonoBehaviour
{
    public Animator EnemyAnim;

    public void EnemyShake()
    {
        EnemyAnim.SetTrigger("EnemyShake");
    }
}
