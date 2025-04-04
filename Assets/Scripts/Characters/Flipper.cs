using UnityEngine;

public class Flipper : MonoBehaviour
{
    public void TurnAround(float movement)
    {
        float turnRight = 180;
        float turnLeft = 0;
        float indexDeterminingRotation = 0;

        if (movement > indexDeterminingRotation)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, turnRight, transform.rotation.z));
        }

        if (movement < indexDeterminingRotation)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, turnLeft, transform.rotation.z));
        }
    }
}
