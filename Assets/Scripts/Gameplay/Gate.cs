using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [Header("General")]
    [SerializeField] bool isInvisible;
    [SerializeField] AudioClip gateLoweredSound;

    [Header("Time")]
    [SerializeField] bool timeBased;
    [SerializeField] float timeToWait;

    [Header("Enemies")]
    [SerializeField] bool enemyBased;
    [SerializeField] List<GameObject> enemies;

    [Header("Barrier")]
    [SerializeField] bool blocksPath;

    BoxCollider2D gateCollider;
    PolygonCollider2D areaCollider;
    SpriteRenderer spriteRenderer;
    Rigidbody2D gateRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        gateRigidbody = GetComponent<Rigidbody2D>();
        gateCollider = GetComponent<BoxCollider2D>();
        areaCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvisible)
        {
            gateCollider.enabled = false;
            spriteRenderer.enabled = false;
            return;
        }
        else
        {
            gateCollider.enabled = true;
            spriteRenderer.enabled = true;
        }

        if (enemyBased)
        {
            for (int index = 0; index < enemies.Count; index++)
            {
                if (!enemies[index])
                {
                    enemies.RemoveAt(index);
                }
            }

            if (enemies.Count <= 0)
            {
                RemoveGate();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            if (timeBased)
            {
                isInvisible = false;
                StartCoroutine(BlockUntilTimeEnds());
            }
            else if (blocksPath)
            {
                isInvisible = false;
            }
            else if (enemyBased)
            {
                isInvisible = false;
            }
        }
    }

    IEnumerator BlockUntilTimeEnds()
    {
        yield return new WaitForSeconds(timeToWait);
        RemoveGate();
    }

    void RemoveGate()
    {
        AudioSource.PlayClipAtPoint(gateLoweredSound, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
