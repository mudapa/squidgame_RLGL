using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollController : MonoBehaviour
{
    public float minTimer, maxTimer;

    public bool isGreenLight = true;

    public Animator animator;
    public readonly string greenLight = "GreenLight";

    public Transform ShotPoint;
    public GameObject bulletPrefab;
    public bool hasShoot;

    private AudioSource audioSource;
    public AudioClip redLightSfx, greenLightSfx, shootSfx;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeLightCoroutine());
    }

    IEnumerator ChangeLightCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minTimer, maxTimer));

        if(isGreenLight)
        {
            animator.SetBool(greenLight, false);
            audioSource.PlayOneShot(redLightSfx);
            yield return new WaitForSeconds(0.7f);
            isGreenLight = false;
            print("Red Light");
            //GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            isGreenLight = true;
            audioSource.PlayOneShot(greenLightSfx);
            animator.SetBool(greenLight, true);
            print("Green Light");
            //GetComponent<Renderer>().material.color = Color.green;
        }

        StartCoroutine(ChangeLightCoroutine());
    }

    public void ShootPlayer(Transform PlayerTarget)
    {
        if(hasShoot) return;
        audioSource.PlayOneShot(shootSfx);
        GameObject bulletGo = Instantiate(bulletPrefab, ShotPoint.position, Quaternion.identity);
        bulletGo.GetComponent<BulletMovement>().playerTartget = PlayerTarget;
        hasShoot = true;
    }
}
