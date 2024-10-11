using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Particle
{
    [RequireComponent(typeof(ParticleSystem))]
    public class StarrySkyController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particleStars;

        public void StartMove(float duration)
        {
            
        }
    }
}
