using UnityEngine;

namespace OccaSoftware.Buto.Demo
{
    public class DoChangeFogMaterial : MonoBehaviour
    {
        [SerializeField] private Material[] materials;
        private int count = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<ButoFogComponent>().SetFogMaterial(materials[count]);

                count++;
                if (count >= materials.Length)
                    count = 0;
            }
        }
    }

}