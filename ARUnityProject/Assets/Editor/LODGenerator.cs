using UnityEngine;
using UnityEditor;
using UnityMeshSimplifier;

public class LODGenerator : EditorWindow
{
    [MenuItem("Tools/Generate LODs for Selected")]
    static void GenerateLODs()
    {
        var selected = Selection.activeGameObject;
        if (selected == null) { Debug.LogError("Seleziona il root del modello auto"); return; }

        // Rimuovi LODGroup esistente se presente
        var existing = selected.GetComponent<LODGroup>();
        if (existing != null) DestroyImmediate(existing);

        var lodGroup = selected.AddComponent<LODGroup>();
        var lods = new LOD[3];

        // LOD0 — originale
        lods[0] = new LOD(0.6f, selected.GetComponentsInChildren<Renderer>());

        // LOD1 — 40% poligoni
        var lod1Root = CreateSimplifiedCopy(selected, 0.4f, "LOD1");
        lods[1] = new LOD(0.2f, lod1Root.GetComponentsInChildren<Renderer>());

        // LOD2 — 10% poligoni
        var lod2Root = CreateSimplifiedCopy(selected, 0.1f, "LOD2");
        lods[2] = new LOD(0.05f, lod2Root.GetComponentsInChildren<Renderer>());

        lodGroup.SetLODs(lods);
        lodGroup.RecalculateBounds();

        Debug.Log($"LOD Group generato su {selected.name}");
    }

    static GameObject CreateSimplifiedCopy(GameObject source, float quality, string suffix)
    {
        var copy = Instantiate(source, source.transform.parent);
        copy.name = source.name + "_" + suffix;
        copy.transform.localPosition = source.transform.localPosition;
        copy.transform.localRotation = source.transform.localRotation;
        copy.transform.localScale = source.transform.localScale;

        foreach (var mf in copy.GetComponentsInChildren<MeshFilter>())
        {
            var simplifier = new MeshSimplifier();
            simplifier.Initialize(mf.sharedMesh);
            simplifier.SimplifyMesh(quality);
            mf.sharedMesh = simplifier.ToMesh();
        }

        // Nascondi nell'Inspector — gestito dal LODGroup
        copy.hideFlags = HideFlags.HideInHierarchy;
        return copy;
    }
}