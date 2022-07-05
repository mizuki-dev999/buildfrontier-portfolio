using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapDatabase : ScriptableObject
{
    public List<MapData> MapDataList = new List<MapData>();

    [System.Serializable]
    public class MapData
    {
        // ƒ}ƒbƒv‚Ì‘®«î•ñ
        public string Name = "‚È‚Ü‚¦";
        public int Id;
        public Sprite sprite;
        public int StageNum;
        public int[] EncountEnemyIdList;
        public int BossEnemyId;
    }
}
