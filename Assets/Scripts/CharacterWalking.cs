using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterWalking : MonoBehaviour
{
    public float speed = 5;

    Rigidbody rig;
    static public bool canMove = true;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
            rig.velocity = transform.rotation * new Vector3(targetVelocity.x, rig.velocity.y, targetVelocity.y);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Respawn")
        {
            //StopAllCoroutines();
            FindObjectOfType<GameController>().StopShowingText();
            FindObjectOfType<GameController>().ShowText("I need to take my tools first",3f);     
        }
        if (other.gameObject.tag == "forestTrigger1")
        {
            other.gameObject.SetActive(false);
            FindObjectOfType<GameController>().StopShowingText();
            FindObjectOfType<GameController>().ShowText("The strange symbol I got reminds me of something. But what...", 10f);
        }
        if (other.gameObject.tag == "forestTrigger2")
        {
            other.gameObject.SetActive(false);
            FindObjectOfType<GameController>().StopShowingText();
            FindObjectOfType<GameController>().ShowText("Press 'V' to use the visor", 3f);
        }

        if (other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Game");
        }
    }
}