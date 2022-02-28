using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets
{
    public class TerrainEditor : MonoBehaviour
    {
        [System.Serializable]
        public class SplatHeights
        {
            public int texureIndex;
            public int startHeight;
        }
        public GameObject player;
        public GameObject[] trees;
        public GameObject[] grass;
        public GameObject[] extra;
        public GameObject[] rocks       ;
        public int offsetMax = 20;
        public int offsetMin = 5;

        public SplatHeights[] splatHeights;

        public Terrain Terrain;


        private float amplitute = DataHolder.Amplitude;
        private float persistance = DataHolder.Persistance;
        private int octaves = DataHolder.Oktawy;
        GameObject water;

        public void Start()
        {
            System.Random random = new System.Random();
            //generacja ziarna
            int seed = random.Next(1000, 9999);
            //wartość multipliera dla normalizacji
            float multipluier = seed / 10000 + 0.24F;
            float max = 0;
            float min = 999999999;
            //losowe ziarno w klasie z Algorytmem
            Perlin2D perlin = new Perlin2D(seed);
            //rozniar terenu
            var resolution = this.Terrain.terrainData.heightmapResolution;
            var mesh = new float[resolution, resolution];
            for (int x = 0; x < resolution; x++)
                for (int y = 0; y < resolution; y++)
                {//wypielniamy tablicę
                    mesh[x, y] = perlin.Noise((x + 0.1f)/175, (y + 0.1f)/175, octaves, persistance , amplitute);
                    //szukamy max i min
                    if (mesh[x, y] > max)
                        max = mesh[x, y];
                    if (mesh[x, y] < min)
                        min = mesh[x, y];
                }
            //normalizacja
            for (int x = 0; x < resolution; x++)
                for (int y = 0; y < resolution; y++)
                {
                    mesh[x, y] = (mesh[x, y] - min) / (max - min);
                    mesh[x, y] = mesh[x, y] * multipluier;

                }
            //wystawienie wysokości każdego punktu terenu
            this.Terrain.terrainData.SetHeights(0, 0, mesh);
            //kolore terenu
            paintterrain();
            //generacja losowa drzew kwiatów itd.
            GenerateObjects();
            player.transform.position = new Vector3(500, Terrain.SampleHeight(new Vector3(500,0,500))+2, 500);
            //cam.transform.position = player.transform.position;
            
            
        }

        public void paintterrain()
        {
            TerrainData terrainData = Terrain.activeTerrain.terrainData;
            float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];


            for (int y = 0; y < terrainData.alphamapHeight; y++)
                for (int x = 0; x < terrainData.alphamapWidth; x++)
                {
                    float terrainHeight = terrainData.GetHeight(y, x);
                    float[] splat = new float[splatHeights.Length];
                    {
                        for (int i = 0; i < splatHeights.Length; i++)
                        {
                            if (i == splatHeights.Length - 1 && terrainHeight >= splatHeights[i].startHeight)
                                splat[i] = 1;

                            else if (terrainHeight >= splatHeights[i].startHeight && terrainHeight <=
                                splatHeights[i + 1].startHeight)
                                splat[i] = 1;
                        }
                        for (int j = 0; j < splatHeights.Length; j++)
                        {
                            splatmapData[x,y,j] = splat[j];
                        }
                    }

                }
            terrainData.SetAlphamaps(0,0, splatmapData);
            

        }
        public void GenerateObjects()
        {
            Vector3 spawnPos = Vector3.zero; ;
            int countrocks = 0;
            int extracount = 0;
            int resolution = this.Terrain.terrainData.heightmapResolution;
            TerrainData terrainData = this.Terrain.terrainData;
            for (int x = offsetMax; x< 1000 - offsetMax; x+= 40)
            {
                for(int z = offsetMax; z < 1000 - offsetMax; z+= 40)
                {

                    int randomoffset = UnityEngine.Random.Range(offsetMin,offsetMax);
                    Vector3 vector3 = new Vector3(z + randomoffset, 0, x + randomoffset);
                    if (Terrain.SampleHeight(vector3) >= 70 && Terrain.SampleHeight(vector3) < 120)
                    {
                        
                            
                            PickAndSpawn(new Vector3(z + randomoffset - 10, Terrain.SampleHeight(new Vector3(z + randomoffset - 5, 0, x + randomoffset - 5))-1F, x + randomoffset - 10)
                                , Quaternion.identity, extra);
                            extracount = 0;
                        
                        spawnPos = new Vector3(z+randomoffset, Terrain.SampleHeight(vector3)-4, x+randomoffset);
                        PickAndSpawn(spawnPos, Quaternion.identity, trees);
                        
                    }
                    else if (Terrain.SampleHeight(vector3) <= 60 && countrocks > 10)
                    {
                        spawnPos = new Vector3(z + randomoffset, Terrain.SampleHeight(vector3)-4, x + randomoffset);
                        PickAndSpawn(spawnPos, Quaternion.identity, rocks);
                        countrocks = 0;
                    }
                    else countrocks++;
                }
            }
            


          }

        void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn, GameObject[] list)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Length);
            Instantiate(list[randomIndex], positionToSpawn, rotationToSpawn);
        }
    }
}
