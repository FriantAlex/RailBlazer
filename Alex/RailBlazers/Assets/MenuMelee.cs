using UnityEngine;
using System.Collections;

public class MenuMelee : MonoBehaviour
{

    private Animator anim;
    public bool idle = false;
    public bool attack = false;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (idle)
        {
            StartCoroutine("Idle");
        }


        if (attack)
        {
            StartCoroutine("Attack");
        }


    }

    IEnumerator Idle()
    {
        yield return new WaitForSeconds(10f);
            anim.SetBool("Attacking", false);
            anim.SetBool("Idle", true);
            

            idle = false;
            attack = true;
      
        yield return new WaitForSeconds(10f);

    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(10f);

            anim.SetBool("Idle", false);
            anim.SetBool("Attacking", true);

            attack = false;
            idle = true;
        
        yield return new WaitForSeconds(10f);

    }
}
