using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CriWare;
using CriWare.Assets;

public class BGMplayer : MonoBehaviour
{
    public static BGMplayer instance;

    //ACF アセット
    public CriAtomAcfAsset acf;

    //ACB アセット
    public CriAtomAcbAsset acb;

    //プレイヤー
    private CriAtomExPlayer player;

    public List<string> SoundTitles;
    public int NowPlaySoundNumber = 0;

    //private float volume = 1.0f;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    IEnumerator Start()
    {
        while (!CriWareInitializer.IsInitialized())
        {
            yield return null;
        }

        //ACFのロード
        acf.Register();

        //DSP バス設定の適応
        CriAtomEx.AttachDspBusSetting("Bus1");

        //ACB ファイルのロード
        acb.LoadImmediate();

        //プレーヤーのインスタンスを生成
        player = new CriAtomExPlayer();

        //データ・キューの指定
        player.SetCue(acb.Handle, SoundTitles[0]);
    }


    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.F5))
        {
            //プレーヤーを再生
            player.Start();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            //プレーヤーを停止
            player.Stop();
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            //プレーヤーを一時停止する
            player.Pause(true);
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            //プレーヤーを再開する
            player.Pause(false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //AISAC コントロール値を0に設定する
            player.SetAisacControl("Any", 0.0f);

            //再生しているキューに対してパラメーターを適用する
            player.UpdateAll();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //AISAC コントロール値を1に設定する
            player.SetAisacControl("Any", 1.0f);

            //再生しているキューに対してパラメーターを適用する
            player.UpdateAll();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //ボリュームを下げる
            volume = Mathf.Max(volume - 0.1f, 0.0f);
            player.SetVolume(volume);
            player.SetPitch(-600.0f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //ボリュームを上げる
            volume = Mathf.Min(volume + 0.1f, 1.0f);
            player.SetVolume(volume);
            player.SetPitch(0.0f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //ボリュームを元に戻す
            volume = 1.0f;
            player.SetVolume(volume);

            //ピッチを元に戻す
            player.SetPitch(0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //再生するキューを一つ後ろにする
            NowPlaySoundNumber = Mathf.Min(NowPlaySoundNumber + 1, SoundTitles.Count);
            player.SetCue(acb.Handle, SoundTitles[NowPlaySoundNumber]);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //再生するキューひとつ前にする
            NowPlaySoundNumber = Mathf.Max(NowPlaySoundNumber - 1, 0);
            player.SetCue(acb.Handle, SoundTitles[NowPlaySoundNumber]);
        }
        */
    }

    public void BGMplay(string SETitle)
    {
        player.SetCue(acb.Handle, SETitle);
        player.Start();

    }
}
