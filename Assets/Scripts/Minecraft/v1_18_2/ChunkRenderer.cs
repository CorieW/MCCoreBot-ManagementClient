using System.Collections.Generic;
using UnityEngine;
using Minecraft;

namespace Minecraft.v1_18_2
{
    public class ChunkRenderer : IChunkRenderer
    {
        private Material _material;

        private Queue<Chunk> _in;
        private Chunk _current;

        public void Setup(Material material)
        {
            _material = material;
            _in = new Queue<Chunk>();
        }

        public void QueueChunk(Chunk chunk)
        {
            _in.Enqueue(chunk);
        }

        private GameObject RenderChunk(Chunk chunk)
        {
            return null;
            // int xSize = blocks.GetLength(0) - 1;
            // int zSize = blocks.GetLength(1) - 1;

            // GameObject newMeshObject = new GameObject($"Chunk ({chunkPos.x}, {chunkPos.z})");
            // Vector2Int totalPos = new Vector2Int(chunkPos.x * blocks.GetLength(0), chunkPos.z * blocks.GetLength(2));
            // newMeshObject.transform.position = new Vector3Int(totalPos.x, 0, totalPos.y);
            // MeshRenderer mr = newMeshObject.AddComponent<MeshRenderer>();
            // mr.material = _material;
            // MeshFilter mf = newMeshObject.AddComponent<MeshFilter>();

            // List<Vector3> vertices = new List<Vector3>();

            // for (int y = 0; y < blocks.GetLength(1); y++)
            // {
            //     for (int z = 0; z < blocks.GetLength(2); z++)
            //     {
            //         for (int x = 0; x < blocks.GetLength(0); x++)
            //         {
            //             int blockID = blocks[x, y, z];
            //             Vector3Int currPos = new Vector3Int(x, y, z);

            //             Vector3Int[] directions = new Vector3Int[]
            //             {
            //                 new Vector3Int(-1, 0, 0),
            //                 new Vector3Int(1, 0, 0),
            //                 new Vector3Int(0, -1, 0),
            //                 new Vector3Int(0, 1, 0),
            //                 new Vector3Int(0, 0, -1),
            //                 new Vector3Int(0, 0, 1)
            //             };

            //             if (blockID == 0) continue;

            //             for (int i = 0; i < directions.Length; i++)
            //             {
            //                 Vector3Int posToLookAt = new Vector3Int(x, y, z) + directions[i];

            //                 int blockIDInDirection;
            //                 // Out of bounds
            //                 // ! Blocks on chunk edge may not have faces.
            //                 if (!posToLookAt.Within(Vector3Int.zero, new Vector3Int(blocks.GetLength(0) - 1, blocks.GetLength(1) - 1, blocks.GetLength(2) - 1))) blockIDInDirection = 0;
            //                 else blockIDInDirection = blocks[posToLookAt.x, posToLookAt.y, posToLookAt.z];

            //                 // If air
            //                 if (blockIDInDirection == 0)
            //                 {
            //                     Vector3[] faceVerts = FaceCalculator.CalculateVertices(directions[i]);
            //                     if (faceVerts == null || faceVerts.Length == 0) continue;

            //                     vertices.Add(new Vector3(x, y, z) + faceVerts[0]);
            //                     vertices.Add(new Vector3(x, y, z) + faceVerts[1]);
            //                     vertices.Add(new Vector3(x, y, z) + faceVerts[2]);
            //                     vertices.Add(new Vector3(x, y, z) + faceVerts[3]);
            //                     vertices.Add(new Vector3(x, y, z) + faceVerts[4]);
            //                     vertices.Add(new Vector3(x, y, z) + faceVerts[5]);
            //                 }
            //             }
            //         }
            //     }
            // }

            // int[] triangles = new int[vertices.Count];
            // // Vector2[] uv = new Vector2[vertices.Length];

            // for (int i = 0; i < triangles.Length; i++)
            // {
            //     triangles[i] = i;
            // }

            // Mesh mesh = mf.mesh = new Mesh();
            // mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            // mesh.vertices = vertices.ToArray();
            // mesh.triangles = triangles;
            // // mesh.uv = uv;
            // mesh.RecalculateNormals();
            
            // return newMeshObject;
        }

        /// <summary> Used to calcuate the vertices for given face directions. </summary>
        private static class FaceCalculator
        {
            public static Vector3[] CalculateVertices(Vector3Int direction)
            {
                if (direction == new Vector3Int(-1, 0, 0))
                {
                    return new Vector3[]
                    {
                        new Vector3(0, 0, 0),
                        new Vector3(0, 0, 1),
                        new Vector3(0, 1, 0),
                        new Vector3(0, 1, 1),
                        new Vector3(0, 1, 0),
                        new Vector3(0, 0, 1)
                        };
                }
                else if (direction == new Vector3Int(1, 0, 0))
                {
                    return new Vector3[]
                    {
                        direction,
                        direction + new Vector3(0, 1, 0),
                        direction + new Vector3(0, 0, 1),
                        direction + new Vector3(0, 1, 0),
                        direction + new Vector3(0, 1, 1),
                        direction + new Vector3(0, 0, 1)
                    };
                }
                else if (direction == new Vector3Int(0, -1, 0))
                {
                    return new Vector3[]
                    {
                        new Vector3(0, 0, 0),
                        new Vector3(1, 0, 0),
                        new Vector3(0, 0, 1),
                        new Vector3(1, 0, 1),
                        new Vector3(0, 0, 1),
                        new Vector3(1, 0, 0)
                    };
                }
                else if (direction == new Vector3Int(0, 1, 0))
                {
                    return new Vector3[]
                    {
                        direction,
                        direction + new Vector3(0, 0, 1),
                        direction + new Vector3(1, 0, 0),
                        direction + new Vector3(0, 0, 1),
                        direction + new Vector3(1, 0, 1),
                        direction + new Vector3(1, 0, 0)
                    };
                }
                else if (direction == new Vector3Int(0, 0, -1))
                {
                    return new Vector3[]
                    {
                        new Vector3(0, 0, 0),
                        new Vector3(0, 1, 0),
                        new Vector3(1, 0, 0),
                        new Vector3(0, 1, 0),
                        new Vector3(1, 1, 0),
                        new Vector3(1, 0, 0)
                    };
                }
                else if (direction == new Vector3Int(0, 0, 1))
                {
                    return new Vector3[]
                    {
                        direction,
                        direction + new Vector3(1, 0, 0),
                        direction + new Vector3(0, 1, 0),
                        direction + new Vector3(1, 0, 0),
                        direction + new Vector3(1, 1, 0),
                        direction + new Vector3(0, 1, 0)
                    };
                }

                return null;
            }
        }
    }
}