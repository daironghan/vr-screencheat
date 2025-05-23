using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 20;
    public float bulletPrefabLifeTime = 3f;

    private bool gameEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        // Make invisible
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Left mouse click
        //if(Input.GetKeyDown(KeyCode.Mouse0))
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameEnded)
            {
                // Return to main menu
                SceneManager.LoadScene("MainMenu"); // or use build index 0
            }
            else
            {
                FireWeapon();
            }
        }
    }

    private void FireWeapon()
    {
        // Create bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        // Shoot bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        // Destroy bullet
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    public void SetGameEnded(bool hasEnded)
    {
        gameEnded = hasEnded;
    }
}
