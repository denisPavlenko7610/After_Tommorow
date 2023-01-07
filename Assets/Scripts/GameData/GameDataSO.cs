using UnityEngine;
using UnityEngine.Serialization;

namespace AfterTomorrow.GameData
{
    [CreateAssetMenu(fileName = "GameData", menuName = "GameData")]
    public class GameDataSO : ScriptableObject
    {
        [FormerlySerializedAs("PlayerData")] public PlayerDataSO playerDataSo;
    }
}