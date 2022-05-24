using UnityEngine;

public class Walking : MonoBehaviour
{
    public float speed = 5;
    Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }
    void Update()
    {
            Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
            rig.velocity = transform.rotation * new Vector3(targetVelocity.x, rig.velocity.y, targetVelocity.y);
    }
}