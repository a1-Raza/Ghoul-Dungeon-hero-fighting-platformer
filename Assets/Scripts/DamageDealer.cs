using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damageToDeal = 50;

    PolygonCollider2D attackHitbox;

    // Start is called before the first frame update
    void Start()
    {
        attackHitbox = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackHitbox.enabled)
        {
            attackHitbox.transform.localPosition = new Vector2(attackHitbox.transform.localPosition.x, attackHitbox.transform.localPosition.y + Time.deltaTime * 0.005f);
        }
        else
        {
            attackHitbox.transform.localPosition = new Vector2(0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision is CapsuleCollider2D)) { return; }

        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damageToDeal);
        }
    }
}
