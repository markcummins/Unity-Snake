using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    void Start()
    {
        RandomizePosition();
    }

    void Update()
    {

    }

    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
        print(bounds);

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        print(this.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
        }

    }
}
