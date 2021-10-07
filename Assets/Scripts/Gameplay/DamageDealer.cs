using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damageToDeal = 50;
    
    float damageMultiplier;

    GameObject hitboxGameobject;
    Player playerClass;

    // Start is called before the first frame update
    void Start()
    {
        hitboxGameobject = this.gameObject;

        playerClass = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerClass)
        {
            damageMultiplier = playerClass.GetAttackMultiplier();
        }
        else
        {
            damageMultiplier = 1;
        }


        if (hitboxGameobject.activeSelf)
        {
            hitboxGameobject.transform.localPosition = new Vector2(hitboxGameobject.transform.localPosition.x, hitboxGameobject.transform.localPosition.y + Time.deltaTime * 0.005f);
        }
        else
        {
            hitboxGameobject.transform.localPosition = new Vector2(0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision is CapsuleCollider2D)) { return; }

        if (collision.gameObject.GetComponent<Health>())
        {
            //Debug.Log((int)((double)damageToDeal * damageMultiplier));
            collision.gameObject.GetComponent<Health>().TakeDamage((int) ((double) damageToDeal * damageMultiplier));
        }
    }

    public int GetDamageToDeal() { return damageToDeal; }

    public void SetDamageToDeal(int damage) { damageToDeal = damage; }
}
