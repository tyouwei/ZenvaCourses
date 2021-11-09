using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody2D body;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClips;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClips[0]);
        Destroy(this.gameObject, 3f);
    }
    public void Launch(Vector2 direction, float speed)
    {
        body.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward * 5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(audioClips[1]);
        Destroy(this.gameObject, .33f);
    }
}
