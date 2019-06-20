using Unity.Entities;
using UnityEngine;

public class DamageSystem : ComponentSystem
{
    private struct ProjectileFilter {
        public Transform Transform;
        public Rigidbody Rigidbody;
        public ProjectileComponent ProjectileComponent;
    }

    private struct DamageableAIFilter {
        public Transform Transform;
        public HealthComponent HealthComponent;
        public AIActorComponent AiActorComponent;
    }

    private struct DamageablePlayerFilter {
        public Transform Transform;
        public HealthComponent HealthComponent;
        public PlayerComponent PlayerComponent;
    }

    protected override void OnUpdate() {
        foreach (var projectile in GetEntities<ProjectileFilter>()) {
            switch (projectile.ProjectileComponent.team)
            {
                case Team.ENEMY:
                    var player = GetEntities<DamageablePlayerFilter>()[0];
                    checkDistanceAndDamage(projectile, player.Transform.position, player.HealthComponent);
                    
                    break;
                case Team.PLAYER:
                    foreach (var enemy in GetEntities<DamageableAIFilter>())
                        if(checkDistanceAndDamage(projectile, enemy.Transform.position, enemy.HealthComponent))
                            GameObject.Destroy(projectile.Transform.gameObject);

                    break;
            }
        }
    }

    private bool checkDistanceAndDamage(ProjectileFilter projectile, Vector3 target, HealthComponent healthComponent) {
        if (Vector3.Distance(projectile.Transform.position, target) < projectile.ProjectileComponent.radius) {
            if (Vector3.Distance(Vector3.zero, projectile.Rigidbody.velocity) < .5) {
                Debug.Log(Vector3.Distance(Vector3.zero, projectile.Rigidbody.velocity));
                return false;
            }

            healthComponent.health -= projectile.ProjectileComponent.damage;

            return true;
        }

        return false;
    }
}