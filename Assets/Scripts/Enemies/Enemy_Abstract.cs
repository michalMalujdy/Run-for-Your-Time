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
    public bool isHidden = false;
    protected PolygonCollider2D weaponCollider;
    public WeaponCollisionWithPlayer weaponCollisionWithPlayer;

    public abstract void Attack();
    public abstract void Die();
    public abstract void LureAttack(Vector2 playerPosition);
    public abstract void SetSortingLayer(string layerName);

    public PolygonCollider2D getWeaponCollider()
    {
        return weaponCollider;
    }
    

    public void SwitchBooleanAfterTime(string name, bool state, float time)
    {
        StartCoroutine(SwitchBoolean(name, state, time));
    }

    public IEnumerator SwitchBoolean(string name, bool state, float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool(name, state);
    }

    public Transform getBody()
    {
        return transform.FindChild("body");
    }

    public void adjustOrientationToPlayer()
    {
        if(GameObject.FindWithTag("Player").GetComponent<Transform>().position.x < GetComponent<Transform>().position.x)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(GetComponent<Transform>().rotation.x, GetComponent<Transform>().rotation.y - 180.0f, GetComponent<Transform>().rotation.z));
        }
    }
}
