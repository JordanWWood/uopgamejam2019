using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputSystem : ComponentSystem {
    private struct InputFiler {
        public PlayerRotationComponent RotationComponent;
    }
    
    private struct MovementFilter {
        public MovementComponent MovementComponent;
    }
    
    private struct ShootFilter {
        public PlayerComponent PlayerComponent;
        public ShootComponent ShootComponent;
    }
    
    private struct RestartFilter {
        public RestartDataComponent RestartDataComponent;
    }

    protected override void OnUpdate() {
        foreach (var entity in GetEntities<MovementFilter>()) {
            // Keyboard input
            var output = new Vector2();
            
            if (Input.GetKey(KeyCode.W))
                output.x = 1;
            else if (Input.GetKey(KeyCode.S))
                output.x = -1;
            
            if (Input.GetKey(KeyCode.A))
                output.y = 1;
            else if (Input.GetKey(KeyCode.D))
                output.y = -1;

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
                output.x = 0;
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                output.y = 0;

            if (Input.GetKey(KeyCode.Escape)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                GetEntities<RestartFilter>()[0].RestartDataComponent.restart = true;
            }

            entity.MovementComponent.direction = output;
        }

        foreach (var entity in GetEntities<InputFiler>()) {
            // Mouse input
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
 
            if(Physics.Raycast(ray,out hit,100)) {
                entity.RotationComponent.mouseWorldPosition = hit.point;
            }
        }
        
        // Mouse button
        foreach (var entity in GetEntities<ShootFilter>())
            entity.ShootComponent.isFiring = Input.GetKey(KeyCode.Mouse0);;
    }
}
