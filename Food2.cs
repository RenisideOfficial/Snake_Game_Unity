using UnityEngine;
using System.Collections;

 public class Food2 : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public float timer = 5f;
    public float timer2 = 5f;
    public SpriteRenderer food2_sprite;
    public BoxCollider2D food2_collider;


    public void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    void Start()
    {       
        StartCoroutine(wait3());
    }

    public IEnumerator wait3()
    {
        food2_collider.enabled = false;
        food2_sprite.enabled = false;
        RandomizePosition();
        yield return new WaitForSeconds(timer);       
        food2_collider.enabled = true;
        food2_sprite.enabled = true;
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(timer);
        food2_collider.enabled = true;
        food2_sprite.enabled = true;
        StartCoroutine(wait2());
    }

    public IEnumerator wait2()
    {
        yield return new WaitForSeconds(timer2);
        if (food2_collider.enabled == true && food2_sprite.enabled == true)
        {
            food2_collider.enabled = false;
            food2_sprite.enabled = false;
            RandomizePosition();
            StartCoroutine(wait());
        }
    }
       
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            food2_collider.enabled = false;
            food2_sprite.enabled = false;
            RandomizePosition();
            StartCoroutine(wait());
        }
    }
}
