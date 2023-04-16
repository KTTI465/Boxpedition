using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CriWare;
using CriWare.Assets;

public class SEplayer : MonoBehaviour
{

    /* (9) 全データがロード済みかどうか */

    public bool isLoaded
    {
        /* (11) 各データがロード済みかどうかをチェック */
        get
        {
            bool value = false;
            foreach (var acbAsset in acbAssets)
            {
                value |= acbAsset.Loaded;
            }
            return value;
        }
    }

    /* (2) ACF アセット */
    public CriAtomAcfAsset acfAsset;

    /* (3) ACB アセットの配列 */
    /* Note: ACB は複数ファイルになっている可能性があるため */
    public CriAtomAcbAsset[] acbAssets;

    /* (10) レジスト済みかどうかのフラグ */
    private bool acfIsRegisterd = false;

    private CriAtomExPlayer player;


    /* (4) コルーチン化 */
    IEnumerator Start()
    {
        /* (5) ライブラリの初期化済みチェック */
        while (!CriWareInitializer.IsInitialized())
        {
            yield return null;

        }

        /* (6) ACF データの登録 */
        acfIsRegisterd = acfAsset.Register();

        /* (7) デフォルトの DSP バス設定を適用 */
        CriAtomEx.AttachDspBusSetting(CriAtomExAcf.GetDspSettingNameByIndex(0));

        /* (8) ACB データのロード */
        foreach (var acbAsset in acbAssets)
        {
            acbAsset.LoadImmediate();
        }

        player = new CriAtomExPlayer();

        player.SetCue(acbAssets[0].Handle, "crash");

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

    public void SFXplay(string SETitle)
    {
        player.SetCue(acbAssets[0].Handle, SETitle);
        player.Start();

    }
}
