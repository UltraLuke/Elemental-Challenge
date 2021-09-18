using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EN UN FUTURO SE USARA PARA AGREGAR ATAQUES A LOS ENEMIGOS

public class TurretEnemy : MonoBehaviour
{
    private EnemyBody enemyBody;
    private string animName;
    public GameObject searchArea;
    public List<Transform> triggers = new List<Transform>();
    public GameObject bullet;
    public List<Vector2> directions = new List<Vector2>();

    public bool flipped;

    private bool enableDetection;

    void Start ()
	{
		enemyBody = GetComponent<EnemyBody>();
        animName = "Shoot";
        enableDetection = true;

        if(flipped)
            enemyBody.FlipCharacterX();
    }

    public void Shoot()
    {
        if (enableDetection)
        {
            enableDetection = false;
            StartCoroutine(ShotController(13, 10f));
        }
    }

    public void OnDestroy()
    {
        Destroy(searchArea.gameObject);
    }

    private IEnumerator ShotController(int numShots, float shootingTime)
    {
        for (int i = 0; i < numShots * 2; i++)
        {
            if (i % 2 == 0)
            {
                enemyBody.LinearShoot(triggers, bullet, directions);
                enemyBody.AnimationExe(animName);
                enemyBody.PlaySound(19);
            }

            yield return new WaitForSeconds(shootingTime / (numShots * 2f));
        }
        enableDetection = true;
    }
}