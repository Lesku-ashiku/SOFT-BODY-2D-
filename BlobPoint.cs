using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class BlobPoint : MonoBehaviour
{   
    //the value of this variables its defined in the inspector
    public int moveSpeed, maxvelcityx, GravityScale, maxgravity;//pretty much self explenatory except for gravityscale
    public float GravityAttenuant;//this a factor that gets multiplied to the rb.gravityscale every frame giving an acceleration effect
    Rigidbody2D rb;
    Vector2 maxvelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxvelocity = new Vector2(maxvelcityx, 0);
        //we fill gravity scale with the initial gravity scale of the rigidbody 
        //this way when we are going to set it back to the original we have a variable to tell us whats the original
        //here we could have inserted a specific number, but setting it as a variable lets us test the different combination a lot faster,
        //as all we need is to change the gravityscale in the rb component.
        rb.gravityScale = GravityScale;
    }
    void Update()
    {
        float move = Input.GetAxis("Horizontal");//input

        rb.AddForce(new Vector2(move * moveSpeed, 0));
        //this adds a force moving the object
        if (rb.velocity.x > maxvelocity.x)
        {
            rb.velocity = new Vector2(maxvelcityx, rb.velocity.y);
        }
        if (rb.velocity.x < -maxvelcityx)
        {
             rb.velocity = new Vector2(-maxvelcityx, rb.velocity.y);
        }//this 2 ifs check if the velocity is bigger than the max velocity if yes set velocity as max velocity
        if (rb.velocity.y < -1)//check if its going downwords
        {
            rb.gravityScale = rb.gravityScale * GravityAttenuant;//moltiply gravity(acceleration)

        }
        
        else
        {
            rb.gravityScale = GravityScale;//set it back to standard
        }
        if (rb.gravityScale > maxgravity)
        {
            rb.gravityScale = maxgravity;//line 36
        }
    }

   
}
