using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class EnviromentGenerator : MonoBehaviour
    {
        Player player;
        public GameObject[] Items;
        public int GridX;
        public int GridZ;
        public float Offset = 10f;
        public Vector3 GridOrigin = Vector3.zero;


        private void Start()
        {Spawngrid();
            
        }
        public void Spawngrid()
        {
            for (int x =0;x < GridX; x++)
                for (int z =0;z < GridZ; z++)
                {
                    Vector3 spawnPosition = new Vector3(x*Offset, Terrain.activeTerrain.terrainData.GetHeight(x,z), z*Offset) + GridOrigin;
                    PickAndSpawn(spawnPosition, Quaternion.identity);
                }
        }

        void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
        {
            int randomIndex = Random.Range(0, Items.Length);
            GameObject clone = Instantiate(Items[randomIndex],positionToSpawn,rotationToSpawn);
        }
        
        
    }
}
