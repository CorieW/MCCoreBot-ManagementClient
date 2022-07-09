using System.Collections.Generic;
using UnityEngine;
using Minecraft;

namespace Minecraft.v1_18_2
{
    public class ChunkRenderer : AbstractChunkRenderer
    {
        public static int CHUNK_HEIGHT = 384;

        public ChunkRenderer() : base()
        { }

        private Block[,,] ConvertBlockStateArrayToBlockMap(int[] blockStates)
        {
            Block[,,] blocks = new Block[CHUNK_WIDTH_N_LENGTH, CHUNK_HEIGHT, CHUNK_WIDTH_N_LENGTH];

            for (int y = 0, i = 0; y < CHUNK_HEIGHT; y++)
            {
                for (int z = 0; z < CHUNK_WIDTH_N_LENGTH; z++)
                {
                    for (int x = 0; x < CHUNK_WIDTH_N_LENGTH; x++, i++)
                    {
                        blocks[x, y, z] = Registry.Instance.BlockStatesRegistry.SafeGetObjectFor(blockStates[i]);
                    }
                }
            }

            return blocks;
        }

        protected override ChunkMeshData GenerateChunkMeshData(int[] blockStates)
        {
            Block[,,] blocks = ConvertBlockStateArrayToBlockMap(blockStates);

            int xSize = blocks.GetLength(0) - 1;
            int zSize = blocks.GetLength(1) - 1;

            List<Vector3> vertices = new List<Vector3>();

            for (int y = 0; y < blocks.GetLength(1); y++)
            {
                for (int z = 0; z < blocks.GetLength(2); z++)
                {
                    for (int x = 0; x < blocks.GetLength(0); x++)
                    {
                        Block block = blocks[x, y, z];
                        Vector3Int currPos = new Vector3Int(x, y, z);

                        Vector3Int[] directions = new Vector3Int[]
                        {
                            new Vector3Int(-1, 0, 0),
                            new Vector3Int(1, 0, 0),
                            new Vector3Int(0, -1, 0),
                            new Vector3Int(0, 1, 0),
                            new Vector3Int(0, 0, -1),
                            new Vector3Int(0, 0, 1)
                        };

                        if (block.displayName.Contains("Air")) continue;

                        // Look in each direction, if the direction is transparent, then set face.
                        for (int i = 0; i < directions.Length; i++)
                        {
                            Vector3Int posToLookAt = new Vector3Int(x, y, z) + directions[i];

                            bool transparentInDirection;

                            // Out of bounds
                            if (!posToLookAt.Within(Vector3Int.zero, new Vector3Int(blocks.GetLength(0) - 1, blocks.GetLength(1) - 1, blocks.GetLength(2) - 1))) transparentInDirection = block.displayName.Contains("Air");
                            else transparentInDirection = blocks[posToLookAt.x, posToLookAt.y, posToLookAt.z].displayName.Contains("Air");

                            // If transparent
                            if (transparentInDirection)
                            {
                                Vector3[] faceVerts = FaceCalculator.CalculateVertices(directions[i]);
                                if (faceVerts == null || faceVerts.Length == 0) continue;

                                vertices.Add(new Vector3(x, y, z) + faceVerts[0]);
                                vertices.Add(new Vector3(x, y, z) + faceVerts[1]);
                                vertices.Add(new Vector3(x, y, z) + faceVerts[2]);
                                vertices.Add(new Vector3(x, y, z) + faceVerts[3]);
                                vertices.Add(new Vector3(x, y, z) + faceVerts[4]);
                                vertices.Add(new Vector3(x, y, z) + faceVerts[5]);
                            }
                        }
                    }
                }
            }

            int[] triangles = new int[vertices.Count];
            // Vector2[] uv = new Vector2[vertices.Length];

            for (int i = 0; i < triangles.Length; i++)
            {
                triangles[i] = i;
            }

            return new ChunkMeshData(vertices.ToArray(), triangles, null);
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