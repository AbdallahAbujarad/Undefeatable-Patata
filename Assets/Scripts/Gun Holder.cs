using UnityEngine;

public class GunHolder : MonoBehaviour
{
    float lerp = 80;
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.right = Vector2.Lerp(transform.right,new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y), lerp * Time.deltaTime);
    }
}
