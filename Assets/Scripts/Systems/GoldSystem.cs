using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class GoldSystem : ComponentSystem {
    private struct GoldData {
        public ComponentArray<GoldComponent> goldComponents;
    }

    [Inject] private GoldData goldData;
    
    protected override void OnUpdate() {
        GoldComponent component = goldData.goldComponents[0];
        component.scoreText.GetComponent<Text>().text = $"Score: {component.score}";
    }
}