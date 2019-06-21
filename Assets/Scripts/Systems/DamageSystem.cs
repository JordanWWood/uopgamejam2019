using System;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    private struct RestartDataFilter {
        public RestartDataComponent RestartDataComponent;
    }

    private DateTime playerDamageReset;
    
    protected override void OnUpdate() {
        foreach (var projectile in GetEntities<ProjectileFilter>()) {
            switch (projectile.ProjectileComponent.team)
            {
                case Team.ENEMY:
                    var player = GetEntities<DamageablePlayerFilter>()[0];
                    if (checkDistanceAndDamage(projectile, player.Transform.position, player.HealthComponent, true))
                        if (player.HealthComponent.health <= 0)
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                            GetEntities<RestartDataFilter>()[0].RestartDataComponent.restart = true;
                        }

                    break;
                case Team.PLAYER:
                    foreach (var enemy in GetEntities<DamageableAIFilter>())
                        if(checkDistanceAndDamage(projectile, enemy.Transform.position, enemy.HealthComponent, false))
                            GameObject.Destroy(projectile.Transform.gameObject);

                    break;
            }
        }
    }

    private bool checkDistanceAndDamage(ProjectileFilter projectile, Vector3 target, HealthComponent healthComponent, bool isPlayer) {
        if (Vector3.Distance(projectile.Transform.position, target) < projectile.ProjectileComponent.radius) {
            if (Vector3.Distance(Vector3.zero, projectile.Rigidbody.velocity) < .3) {
                return false;
            }

            if (isPlayer)
            {
                if (!(playerDamageReset < DateTime.Now))
                    return false;
                
                playerDamageReset = DateTime.Now.AddSeconds(.5f);
            }
            
            healthComponent.health -= projectile.ProjectileComponent.damage;

            return true;
        }

        return false;
    }
}