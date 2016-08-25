using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;


public abstract class Enemy_Abstract: MonoBehaviour
{
    public float speed;
    public float damage;
    public string type;
    public Animator animator;

    public abstract void Attack();
    public abstract void Die();

    public void SwitchBooleanAfterTime(string name, bool state, float time)
    {
        StartCoroutine(SwitchBoolean(name, state, time));
    }

    public IEnumerator SwitchBoolean(string name, bool state, float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool(name, state);
    }
}
