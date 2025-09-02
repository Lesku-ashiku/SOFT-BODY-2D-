in this repo you will find how to setup a soft bofy in a 2d game.

within this repo there will also be the scripts(commented/explained)

FIRST: in our scene make an empty object

<img width="504" height="755" alt="create empty" src="https://github.com/user-attachments/assets/1ae1d086-4fa3-40fc-b830-0b3594fdd550" />

then add a rigidbody 2D to the object in the inspector

<img width="592" height="615" alt="add rigidbody" src="https://github.com/user-attachments/assets/88353b1a-5efa-4aaa-bba8-4c382b72e0fc" />

lets also add the blob script and the BoftBodyOutline script.

then we make a prefab in the assets.

<img width="1183" height="916" alt="create prefab" src="https://github.com/user-attachments/assets/cfab0a11-a23e-47d1-8491-94e2ca8621d5" />

the prefab must have a rigidbody 2D and the blob point script attached,

when done attach the prefab  to the empty object in the blob script.

if u want the soft body to be alble to jump then add the player movment script to the empty object

if u dont want the soft body to move than set the move speed to 0.
