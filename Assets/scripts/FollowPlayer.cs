using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public GameObject Camera;

    public float MinDistance = 0;
    public float MaxDistance = 27;

    // Update is called once per frame
    void Update()
    {
        // if the player is past the middle, for atleast level one, because there's a wall on the right and I honestly don't see myself change that for every level. If I do, I can alter the code.
        if (Player.transform.position.x > MaxDistance) 
        { 
            Camera.transform.position  = new Vector3(MaxDistance,0,-1);
        } 
        else if (Player.transform.position.x > 0)
        {
            // follow the player along the X axis, while keeping the y and X, I tried using a vector2, but the z would be 0 and nothing would be visible
            Camera.transform.position = new Vector3(Player.transform.position.x, 0, -1);
        }
        else
        {
            Camera.transform.position  = new Vector3(MinDistance,0,-1);
        }
    }
}
