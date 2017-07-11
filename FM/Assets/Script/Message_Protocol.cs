
using UnityEngine;

namespace MessageProtocol
{

    public struct sendmessage
    {
        public bool isAnswer; //대답이있는가?
        public int answer_num; //몇개의 대답이 있는가?
        public int dialog_subject; //대화의 주제는 무엇인가?
        public int dialog_num; //몇번째 대화인가?
    }
}