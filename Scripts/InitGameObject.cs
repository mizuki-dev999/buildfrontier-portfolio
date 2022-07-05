using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameObject : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    //ゲーム開始時に必要なゲームオブジェクトを生成
	static void Init()
	{
        // シングルトンオブジェクトが生成されているかを判断
        if (GameObject.Find("MyCharacterStatus") == null)
        {
            GameObject firstinventory = new("MyCharacterStatus"); // GameObjectを生成
            firstinventory.AddComponent<MyCharacterStatus>(); // scriptを追加
        }
        if (GameObject.Find("Inventory") == null)
        {
            GameObject firstinventory = new("Inventory"); // GameObjectを生成
            firstinventory.AddComponent<Inventory>(); // scriptを追加
        }
        //------------------------------------------------------
        //myCharacterStatus = GameObject.Find("MyCharacterStatus"); // 生成したMyCharacterStatusを取得
        //myStatus = myCharacterStatus.GetComponent<MyCharacterStatus>(); // scriptを取得
        //inventoryObj = GameObject.Find("Inventory");
        //inventory = inventoryObj.GetComponent<Inventory>();
        //------------------------------------------------------
    }
}
