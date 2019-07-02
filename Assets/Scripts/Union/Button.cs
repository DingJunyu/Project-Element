using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.EventSystems.EventTrigger))]
public class Button : MonoBehaviour
{
    public bool destroyThis = false;
    public bool closeThis = false;
    public bool detail = false;

    // Start is called before the first frame update
    void Start() {
        Button btn = this.GetComponent<Button>();
        EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerClick;

        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(OnClick);

        trigger.triggers.Add(entry);
    }

    private void OnClick(BaseEventData pointData) {
        if (destroyThis)
            DestoryThis();
        if (closeThis)
            CloseThis();
    }

    private void DestoryThis() {
        GameObject myParent;
        myParent = transform.parent.gameObject;//メニューオブジェクトを探す
        myParent.GetComponent<RightClickMenu>().DestoryParent();//アイテムを削除
        Destroy(myParent);//メニューを削除
    }

    private void CloseThis() {
        GameObject myParent;
        myParent = transform.parent.gameObject;//メニューオブジェクトを探す
        Destroy(myParent);//メニューを削除
    }
}
