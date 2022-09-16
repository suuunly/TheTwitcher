using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public Gun Gun;
    public TextMeshProUGUI Text;

    private void Start()
    {
        Gun.AmmoStatus += UpdateAmmo;
    }

    private void UpdateAmmo()
    {
        Text.text = Gun.AmmoLeft.ToString();
    }

}
