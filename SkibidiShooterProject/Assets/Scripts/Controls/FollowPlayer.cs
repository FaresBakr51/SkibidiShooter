using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private bool followplayerRot;
    private void Update()
    {
        if (player)
        {
           
            Vector3 newpos = player.position;
            newpos.y = transform.position.y;
            transform.position = newpos;
            if (followplayerRot)
            {
                transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
            }
        }
    }
}
