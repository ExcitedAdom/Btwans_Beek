using UnityEngine;

public class ScrollBG : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private Renderer bgRenderer;

    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
