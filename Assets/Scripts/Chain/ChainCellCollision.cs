using UnityEngine;
using System.Collections;

public class ChainCellCollision : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ChainCell")
        {
            Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
        }
    }
}
