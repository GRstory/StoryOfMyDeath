using System.Collections.Generic;
using UnityEngine;

public static class NPCManager
{
    private static Dictionary<string, CharacterData> _npcDict = new Dictionary<string, CharacterData>();
    private static List<string> _idList = new List<string>();

    public static void RegisterNPC(ICharacter character, GameObject go)
    {
        _npcDict[character.CharacterId] = new CharacterData(go, character);
        _idList.Add(character.CharacterId);
    }

    public static CharacterData GetNPCObject(string name)
    {
        if(_npcDict.TryGetValue(name, out CharacterData data))
        {
            if(data == null)
            {
                _npcDict.Remove(name);
                return null;
            }
            return data;
        }
        return null;
    }
}

public class CharacterData
{
    public GameObject gameObject;
    public ICharacter character;

    public CharacterData(GameObject gameObject, ICharacter character)
    {
        this.gameObject = gameObject;
        this.character = character;
    }
}
