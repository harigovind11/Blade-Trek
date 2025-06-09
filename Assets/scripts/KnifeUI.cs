using UnityEngine;

public class KnifeUI : MonoBehaviour
{
    public void RotateLeftDown()
    {
        if (KnifeSpawner.instance.currentKnife != null)
            KnifeSpawner.instance.currentKnife.SetRotateLeft(true);
    }

    public void RotateRightDown()
    {
        if (KnifeSpawner.instance.currentKnife != null)
            KnifeSpawner.instance.currentKnife.SetRotateRight(true);
    }

    public void RotateUp()
    {
        if (KnifeSpawner.instance.currentKnife != null)
        {
            KnifeSpawner.instance.currentKnife.SetRotateLeft(false);
            KnifeSpawner.instance.currentKnife.SetRotateRight(false);
        }
    }
    
    public void OnThrowButtonPressed()
    {
        if (KnifeSpawner.instance.currentKnife != null)
            KnifeSpawner.instance.currentKnife.ThrowKnife();
    }
}