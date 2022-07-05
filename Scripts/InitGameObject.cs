using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameObject : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    //�Q�[���J�n���ɕK�v�ȃQ�[���I�u�W�F�N�g�𐶐�
	static void Init()
	{
        // �V���O���g���I�u�W�F�N�g����������Ă��邩�𔻒f
        if (GameObject.Find("MyCharacterStatus") == null)
        {
            GameObject firstinventory = new("MyCharacterStatus"); // GameObject�𐶐�
            firstinventory.AddComponent<MyCharacterStatus>(); // script��ǉ�
        }
        if (GameObject.Find("Inventory") == null)
        {
            GameObject firstinventory = new("Inventory"); // GameObject�𐶐�
            firstinventory.AddComponent<Inventory>(); // script��ǉ�
        }
        //------------------------------------------------------
        //myCharacterStatus = GameObject.Find("MyCharacterStatus"); // ��������MyCharacterStatus���擾
        //myStatus = myCharacterStatus.GetComponent<MyCharacterStatus>(); // script���擾
        //inventoryObj = GameObject.Find("Inventory");
        //inventory = inventoryObj.GetComponent<Inventory>();
        //------------------------------------------------------
    }
}
