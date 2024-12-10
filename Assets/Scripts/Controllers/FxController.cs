using So;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers
{
    public class FxController : ControllerBase
    {
        public FxData fxData;
        
        public void CreateEffects(Box spawnPoint)
        {
            if (Random.value >= 0.5f) // 50% chance
                return;
            
            var currentFx = fxData.GetRandomFx();
            var rotation = Quaternion.Euler(90, 0, 0); // Add 90 degrees to the X-axis
            var created = Instantiate(currentFx, spawnPoint.transform.position, rotation);
            var component = created.GetComponent<ParticleSystem>();
            component.Play();
            Destroy(created, component.main.duration);
        }
    }
}