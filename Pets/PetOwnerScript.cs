using System.Collections.Generic;
using UnityEngine;

namespace Pets
{
    public class PetOwnerScript : MonoBehaviour
    {
        public List<int> SpawnedPets { get; } = new List<int>();
    }
}
