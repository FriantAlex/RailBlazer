using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public int health;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        if(health <= 0)
        {
            //Debug.Log("Game Over");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Debug.Log("Player was hit");
            health--;
        }
    }
}
