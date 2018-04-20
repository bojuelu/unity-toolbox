/// <summary>
/// Get unity primitive object.
/// Author: Octopoid (Homepage: http://answers.unity3d.com/users/696130/octopoid.html)
/// Refrence: http://answers.unity3d.com/questions/514293/changing-a-gameobjects-primitive-mesh.html
/// </summary>

using System.Collections.Generic;
using UnityEngine;

namespace UnityToolbox
{
    public static class PrimitiveHelper
    {
        private static Dictionary<PrimitiveType, Mesh> primitiveMeshes = new Dictionary<PrimitiveType, Mesh>();

        public static GameObject CreatePrimitive(PrimitiveType type, bool withCollider)
        {
            if (withCollider) { return GameObject.CreatePrimitive(type); }

            GameObject gameObject = new GameObject(type.ToString());
            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = PrimitiveHelper.GetPrimitiveMesh(type);
            gameObject.AddComponent<MeshRenderer>();

            return gameObject;
        }

        public static Mesh GetPrimitiveMesh(PrimitiveType type)
        {
            if (!PrimitiveHelper.primitiveMeshes.ContainsKey(type))
            {
                PrimitiveHelper.CreatePrimitiveMesh(type);
            }

            return PrimitiveHelper.primitiveMeshes[type];
        }

        private static Mesh CreatePrimitiveMesh(PrimitiveType type)
        {
            GameObject gameObject = GameObject.CreatePrimitive(type);
            Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
            GameObject.Destroy(gameObject);

            PrimitiveHelper.primitiveMeshes[type] = mesh;
            return mesh;
        }
    }    
}
