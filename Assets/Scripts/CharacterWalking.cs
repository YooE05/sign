using UnityEngine;

public class CharacterWalking : MonoBehaviour
{
    public float speed = 5;

    Rigidbody rig;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float targetMovingSpeed = speed;
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        rig.velocity = transform.rotation * new Vector3(targetVelocity.x, rig.velocity.y, targetVelocity.y);
    }
}
