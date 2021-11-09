using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baddie : MonoBehaviour
{
    private Rigidbody2D body;
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * 5f);
    }
    public void AssignTarget(Base target, float speed)
    {
        body.AddForce((target.transform.position - transform.position).normalized * speed
        , ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            collision.gameObject.GetComponent<Base>().Damage(1);

        }
        if (!collision.gameObject.CompareTag("Baddie"))
        {
            Destroy(this.gameObject);
        }
    }
}
