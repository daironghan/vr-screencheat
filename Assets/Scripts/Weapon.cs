using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 20;
    public float bulletPrefabLifeTime = 3f;
    public float cooldownTime = 1f; // Cooldown duration
    public Image cooldownImage;

    private bool canFire = true;
    private bool gameEnded = false;

    void Start()
    {
        // Make invisible
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.enabled = false;
        }
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0f;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameEnded)
            {
                // Return to main menu
                SceneManager.LoadScene("MainMenu");
            }
            else if (canFire)
            {
                FireWeapon();
            }
        }
    }

    private void FireWeapon()
    {
        canFire = false; // disable firing
        StartCoroutine(WeaponCooldown()); // start cooldown

        // Create bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        // Shoot bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        // Destroy bullet
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
    }
    private IEnumerator WeaponCooldown()
    {
        float elapsed = 0f;
        cooldownImage.fillAmount = 1f;

        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            cooldownImage.fillAmount = 1f - (elapsed / cooldownTime);
            yield return null;
        }

        cooldownImage.fillAmount = 0f;
        canFire = true;
    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    public void SetGameEnded(bool hasEnded)
    {   
        // To disable weapon
        gameEnded = hasEnded;
    }
}
