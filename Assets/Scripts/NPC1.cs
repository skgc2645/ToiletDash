using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class NPC1 : MonoBehaviour
{
    string _message = "お？お前もサボりかよ！ちょうどいいとこで会ったな。なあ、どうせ授業つまんねぇし、一緒に抜けねぇ？俺、さっきからずっと暇でさ、誰か誘おうと思ってたんだよ。<>" +
        "　教室戻るのもダルいしさ、ちょっと外の空気でも吸いに行こうぜ。屋上とか体育館の裏とか、先生もあんまり来ねぇし、ゆっくりできるぞ。それに、二人ならバレても言い訳しやすいしな！<>" +
        "　てか、お前もここにいるってことは、もう授業なんか聞く気ないだろ？だったら今さら真面目ぶる必要ねぇって。ほら、ぐずぐずしてたら先生に見つかるぞ。さっさと行こうぜ！";




    //NPCクラスを作って継承するべき

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.IsCommnicate = true;
            UIPresenter.Instance.StartCommunication(_message);
        }
    }

}
