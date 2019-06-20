using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    public int damage;
    public float radius;
    public Team team;
}

public enum Team
{
    ENEMY,
    PLAYER
}