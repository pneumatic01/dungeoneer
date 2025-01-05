
using UnityEngine;

public static class ComponentExtensions 
{
    public static Component CloneComponent(Component original, GameObject destination)
    {
        if (original == null || destination == null)
            return null;

        // Get the type of the original component (e.g., Heal)
        System.Type type = original.GetType();

        // Add a new component of the same type to the destination
        Component copy = destination.AddComponent(type);

        // Copy all fields and properties
        foreach (var field in type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance))
        {
            field.SetValue(copy, field.GetValue(original));
        }
        foreach (var property in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
        {
            if (property.CanWrite && property.CanRead && property.Name != "name")
            {
                property.SetValue(copy, property.GetValue(original));
            }
        }
        return copy;
    }
}
