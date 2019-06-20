using Unity.Entities;
using UnityEngine;

public class DamageSystem : ComponentSystem
{
    private struct ProjectileFilter {
        public Transform Transform;
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
                    checkDistanceAndDamage(projectile.Transform.position, player.Transform.position, projectile.ProjectileComponent, player.HealthComponent);
                    
                    break;
                case Team.PLAYER:
                    foreach (var enemy in GetEntities<DamageableAIFilter>())
                        if(checkDistanceAndDamage(projectile.Transform.position, enemy.Transform.position, projectile.ProjectileComponent, enemy.HealthComponent))
                            GameObject.Destroy(projectile.Transform.gameObject);

                    break;
            }
        }
    }

    private bool checkDistanceAndDamage(Vector3 projectile, Vector3 target, ProjectileComponent projectileComponent, HealthComponent healthComponent) {
        if (Vector3.Distance(projectile, target) < projectileComponent.radius)
        {
            healthComponent.health -= projectileComponent.damage;

            return true;
        }

        return false;
    }
}