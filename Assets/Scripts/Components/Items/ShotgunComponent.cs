using System;
using UnityEngine;

public class ShotgunComponent : RangedComponent {
    public float spread;
    public int pelletCount;

    private DateTime last;
    
    public override void action(Vector3 location, Vector3 forwardVector) {
        //use forwardvector to spawn projectiles in front of the weapon with differing velocities. use muzzle point.
        if (DateTime.Now < last) return;
        
        for (int i = 0; i < pelletCount; i++) {
            var prefab = Instantiate(bulletPrefab, muzzlePoint.transform.position, Quaternion.identity);

            var projectileComponent = prefab.GetComponent<ProjectileComponent>();
            projectileComponent.damage = 4;
            projectileComponent.radius = .2f;
            projectileComponent.team = Team.PLAYER;

            prefab.GetComponent<Rigidbody>().velocity += forwardVector * 35;
        }

        last = DateTime.Now.AddSeconds(1);
    }
}