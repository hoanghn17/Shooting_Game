using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletHole : MonoBehaviour
{
    private new Camera camera;

    private Vector3 mousePos;

    public GameObject bulletHoles;

    private GameManager gameManager;

    public TextMeshProUGUI ammoText;

    public AudioClip shootSound;

    private AudioSource shootAudio;

    public bool reloading;

    private float reloadTime = 1; // Delay time when ammo = 0

    public float subTotal = 3; // Total time reload

    public float totalBullet = 6; 

    private float timeBullet = 1; // Delay time when time ++

    public float ammo = 6;

    public Image redBulletImage;

    // Start is called before the first frame update

    void Awake()
    {
        camera = Camera.main;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        shootAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0) && !reloading)
            {
                shootAudio.PlayOneShot(shootSound, 1f);
                Vector3 holePos = new Vector3(transform.position.x, transform.position.y, 2);
                var curHole = Instantiate(bulletHoles, holePos, bulletHoles.transform.rotation);
                Destroy(curHole, 3);
                UploadBullet(ammo);

                if (ammo == 0)
                {
                    reloading = true;
                }
                Ammo();
            }

            if (reloading)
            {
                reloadTime -= Time.deltaTime;
                if (reloadTime <= 0 && subTotal <= totalBullet)
                {
                    subTotal++;
                    UploadBullet(subTotal);
                    reloadTime = timeBullet;
                }
            }

            if (subTotal >= totalBullet)
            {
                reloading = false;
                subTotal = 0;
            }
            UpdateMousePosition();
        }
    }

    void UpdateMousePosition()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 curBullet = new Vector3(mousePos.x, mousePos.y, 2);
        transform.position = curBullet;
    }

    void SpawnBulletHole()
    {
        Vector3 holePos = new Vector3(transform.position.x, transform.position.y, 2);
        Instantiate(bulletHoles, holePos, bulletHoles.transform.rotation);
    }

    public void Ammo()
    {
        ammo--;
        if (ammo <= 0)
        {
            ammo = 6;
            reloading = true;
        }
        ammoText.text = " " + ammo;
    }

    public void UploadBullet(float numberBullet)
    {
        if (totalBullet == numberBullet)
        {
            redBulletImage.fillAmount = 1;
            return;
        }
        var sub = totalBullet - numberBullet;

        var percellDownTemp = Mathf.Round((sub / totalBullet) * 100f) / 100f;

        redBulletImage.fillAmount = 1 - percellDownTemp;

        if (redBulletImage.fillAmount < 0)
        {
            redBulletImage.fillAmount = 0;
        }
    }
}
