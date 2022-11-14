
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] private float parallaxConstant;
    void Update()
    {
        transform.Translate(Vector2.left * GameManager.Instance.GetScrollSpeed() * Time.deltaTime * parallaxConstant);
    }
}
