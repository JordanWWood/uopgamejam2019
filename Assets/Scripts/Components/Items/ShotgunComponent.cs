using UnityEngine;

public class ShotgunComponent : RangedComponent {
    public float spread;
    public int pelletCount;
    
    
    public override void action(Vector3 location, Vector3 forwardVector) {
        //use forwardvector to spawn projectiles in front of the weapon with differing velocities. use muzzle point.
    }
}