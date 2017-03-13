using UnityEngine;

public class ReferenceToGUIDAttribute : PropertyAttribute
{
    public System.Type type;

    public ReferenceToGUIDAttribute(System.Type type)
    {
        this.type = type;
    }
}
