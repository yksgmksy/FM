using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemProtocol
{
    public struct ItemInfo
    {
        public Sprite item_image; //아이템 이미지
        public string item_name; //아이템 이름
        public string item_explain;
        public int item_ID; //아이템 id
        public int item_type; //아이템 타입
        public int item_effect; //아이템 이펙트
    }
}
