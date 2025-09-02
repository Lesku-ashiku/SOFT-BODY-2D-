using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //this script has change a lot from the start,and has pased from player movement to a jump script
    public float jumpForce = 8f;
    

    public bool isGrounded = false;

   
    void Update()
    {
        isGrounded = false;
        checkcolliders();
        
        
        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            foreach (Transform child in transform)
            {
                GameObject childpoint = child.gameObject;
               Rigidbody2D rb = childpoint.GetComponent<Rigidbody2D>();
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

    }
   



   
   private void checkcolliders()
{
    isGrounded = false; // reset before checking
    foreach (Transform child in transform)
    {
        if (Physics2D.OverlapCircle(
            child.position,
            (child.localScale.x / 2) + 0.01f,
            LayerMask.GetMask("Ground")))
        {
            isGrounded = true;
            break; // no need to check further
        }
    }
}

}