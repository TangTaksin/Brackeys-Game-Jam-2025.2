using UnityEngine;

public class SpikeHazard : HazardBase
{
    protected override void ApplyEffect(GameObject target)
    {
        PlayerMovement player = target.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.Die();
        }
    }
}
