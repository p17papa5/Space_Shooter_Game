using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public float shootSpeed = 7f; //shoot speed default 7 
    public GameObject impactEffect;  //impact effect

    public GameObject explosion;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(shootSpeed * Time.deltaTime, 0f, 0f);  // shot speed
    }

    private void OnTriggerEnter2D(Collider2D other)  // na siggrouete me alla antikimena pou einai kai auta colliders
    {
        Instantiate(impactEffect, transform.position, transform.rotation);  //na enfanizete to impact effect

        if(other.tag == "Space Object")  //to other einai to Collider 2D -> meteoritis
        {
            Instantiate(explosion, other.transform.position, other.transform.rotation); //prin to katastrepsi to object na dimiourgisei ena copy
                                                                                //apo to explosion kai to bazoume eki pou einai kai to other object
            Destroy(other.gameObject);  //an sigroustoun meta3i tous o meteoritis tha katastrafei

            GameManager.instance.AddScore(50); // otidipote skotoneis dinei 50 score (meteorites)
        }

        if(other.tag =="Enemy") //an einai enemy to tag
        {
            other.GetComponent<EnemyController>().HurtEnemy();
        }
        if(other.tag =="Boss") //an einai boss to tag
        {
            BossManager.instance.HurtBoss();
        }

        Destroy(this.gameObject);   // h sfera tha katastrafei molis sigrousti me kapio object h figei apo tin othoni.
                                    // to this anaferete sto siggekrimeno script p eimaste tora diladi to PlayerShot.
        
    }

    private void OnBecameInvisible()  //impact effect na katastrefete otan telionei to animation
    {
        Destroy(gameObject);        //opou exoume kapio destroy object einai gia na katastrafei afou to kaloume sinexia
                                    //gia na min gemizei h mnimi tou game
    }
}
