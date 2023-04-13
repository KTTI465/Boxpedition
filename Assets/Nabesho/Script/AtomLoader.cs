using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* (1) 名前空間の設定 */
using CriWare;
using CriWare.Assets;


public class AtomLoader : MonoBehaviour
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

    }
}
