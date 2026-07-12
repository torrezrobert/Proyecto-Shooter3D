using UnityEngine;
using UnityEditor;
using System.Linq;

public class OrdenadorJerarquia
{
    // Esto crea un nuevo botón en el menú al hacer clic derecho
    [MenuItem("GameObject/Ordenar Hijos Numéricamente", false, 0)]
    static void OrdenarHijos()
    {
        // Obtiene el objeto que tienes seleccionado (tu Contenedor_Paredes)
        Transform padre = Selection.activeTransform;
        if (padre == null) return;

        // Convierte los hijos en una lista y los ordena con lógica "Natural" (1, 2, 3... 10)
        var hijos = padre.Cast<Transform>().ToList();
        hijos.Sort((a, b) => EditorUtility.NaturalCompare(a.name, b.name));

        // Aplica el nuevo orden en la jerarquía
        for (int i = 0; i < hijos.Count; i++)
        {
            hijos[i].SetSiblingIndex(i);
        }
    }
}
