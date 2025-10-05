using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float xThreshold = 3f;

    private void Start()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 camPos = transform.position;

        if (Mathf.Abs(player.position.x - camPos.x) > xThreshold)
        {
            camPos.x = player.position.x;
        }

        transform.position = new Vector3(camPos.x, camPos.y, transform.position.z);
    }
}
