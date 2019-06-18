using Unity.Entities;

public class EquipmentSystem : ComponentSystem
{
    public struct EquipmentFilter
    {
        public EquipmentComponent EquipmentComponent;
    }
    
    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<EquipmentFilter>())
        {
            
        }
    }
}