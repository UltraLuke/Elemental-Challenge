using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EN UN FUTURO SE USARA PARA AGREGAR ATAQUES A LOS ENEMIGOS

public class SavageEnemy : MonoBehaviour
{
    private EnemyBody enemyBody;
    private bool playerSeen;
    public bool flipped;
    /*public int maskNum;
    public int maskNum2;*/
    private int layerMask = (1 << 8) | (1 << 9) | (1 << 19);
    public float distance;

    private float aliveTime;

	void Start ()
	{
		enemyBody = GetComponent<EnemyBody>();
        playerSeen = false;

        if (flipped)
        {
            enemyBody.FlipCharacterX();
        }

        aliveTime = 5f;
	}
	
	void Update ()
    {
        Detector();

        if (playerSeen)
        {
            enemyBody.SavageMove(flipped);
            aliveTime -= Time.deltaTime;
        }

        if(aliveTime <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void Detector()
    {
        RaycastHit2D playerDetector = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), transform.right, distance, layerMask);

        if (playerDetector.collider != null)
        {
            playerSeen = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.right * distance);
    }
    /*void Update()
    {
        alive = CheckHP();
        if (alive)
        {
            RaycastHit2D playerDetector = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), transform.right, distanciaCast, hitLayerMask);

            if (playerDetector.collider != null)
            {
                if (playerDetector.collider.gameObject.layer == 8)
                {
                    playerSeen = true;
                }
                else
                {
                    playerSeen = false;
                    //CancelInvoke("Atacar");
                }
            }
            else
            {
                playerSeen = false;
                //CancelInvoke("Atacar");
            }

            if (playerSeen && !attacking)
            {
                PrepararAtaque();
            }
        }
        else
        {
            CancelInvoke();
            gameObject.SetActive(false);
        }
    }*/
}