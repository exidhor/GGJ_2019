using UnityEngine;
using System.Collections;

public class Rat : MonoBehaviour
{
    public float speed;
    public GameObject target;

    public float offset;
    public float espilon;

    public bool start;
    public bool has;
    public bool hasQuit;

    Vector2 min;
    Vector2 max;

    public void Init(GameObject target)
    {
        this.target = target;

        bool left = UnityEngine.Random.value > 0.5f;

        min = Camera.main.ViewportToWorldPoint(Vector3.zero);
        max = Camera.main.ViewportToWorldPoint(Vector3.one);

        float x = 0;

        if (left)
        {
            x = min.x - offset;
        }
        else
        {
            x = max.x + offset;
            transform.localScale = new Vector3(-1, 1, 1);
            speed *= -1;
        }

        float y = target.transform.position.y;

        transform.position = new Vector3(x, y, transform.position.z);

        start = true;
    }

    void LateUpdate()
    {
        if (!start) return;

        if(!has)
        {
            float d = Vector2.Distance(transform.position, target.transform.position);
            if(d < espilon)
            {
                target.transform.SetParent(transform, true);
                has = true;
            }
        }

        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if(pos.x < min.x - offset || pos.x > max.x + offset)
        {
            hasQuit = true;
        }
    }
}
