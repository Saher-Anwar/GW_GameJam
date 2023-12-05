/// <summary>
/// Don't specifically need anything here other than the fact it's persistent.
/// I like to keep one main object which is never killed, with sub-systems as children.
/// Basically any children of this game object will not be destroyed when you're switching between different scenes
/// </summary>
public class Systems : PersistentSingleton<Systems>
{
    
}
