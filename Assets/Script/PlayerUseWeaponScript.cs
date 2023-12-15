using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseWeaponScript : MonoBehaviour
{
    public List<GameObject> weapons; // 存放不同槍枝的列表
    private int currentWeaponIndex = 0; // 當前槍枝的索引

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 按下 H 鍵時切換槍枝
        if (Input.GetKeyDown(KeyCode.H))
        {
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        // // 關閉當前槍枝
        // weapons[currentWeaponIndex].SetActive(false);

        // // 切換到下一個槍枝
        // currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

        // // 啟用新的槍枝
        // weapons[currentWeaponIndex].SetActive(true);
    }
}
