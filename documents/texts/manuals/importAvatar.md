# アバターのインポート手順

## 大まかな流れ

1. VRMをUnityのプロジェクトにインポートする。
2. リグをリターゲティングする。
3. コンポーネントを追加してプレハブ化する。
4. コントローラのタイトル画面のメニューにアバターを追加する。

## VRMをUnityのプロジェクトにインポートする

1. VRMファイルをUnityエディタのProjectビューの`Assets/!Project/Live/Models/Avatars`にドラッグ&ドロップする。
2. インポートしたファイルを選択する。
3. Inspectorから`Migrate To Vrm 1`にチェックを入れ、すぐ下の`Apply`ボタンを押す。

## リグをリターゲティングする

Pose AIで動かせるように設定する。基本的に[こちら](https://github.com/PoseAI/PoseCameraAPI/blob/main/FrameworkDocumentation/RigRetargeting.md)を見ながら行う。

1. シーン`Assets/!Project/RigRetarget/RigRetarget`を開く。

2. インポートしたVRMファイルをHierarchyビューにドラッグ&ドロップする。

3. ロボットと向いている方向(Y軸)が異なる場合は揃える。

4. AnimatorのControllerに`Assets/!Project/RigRetarget/StarterAssets/Idle`を設定する。

5. `Pose AI Rig Retarget`コンポーネントを追加し、`Other Animator`に`PlayerArmature`をドロップする。

6. ゲームを再生して止める。

7. コンソールに出力された文字列(以下のようなもの)をコピーする。  

   ```
    {"PlayerArmature_to_SampleAvatar", new List<Quaternion> {new Quaternion(-0.0006008325f,-0.002679857f,0.0009657434f,0.9999959f),…,new Quaternion(0.1097801f,0.3643457f,-0.687453f,0.6185548f),}},
   ```

8. `Assets/!Project/Live/Scripts/PoseAI/PoseAIRigRetarget`を開く。

9. 26行目以降の`{"Unity_to_Mixamo", new List<Quaternion>…`の下に行を追加して文字列を貼り付ける。

10. `"PlayerArmature_to_<アバター名>"`の`PlayerArmature_to_`の部分を消す。

11. 上書きして保存する。

## コンポーネントを追加してプレハブ化する

1. `Pose AI Rig Retarget`コンポーネントを削除する。
2. `Pose AI Character Animator`コンポーネントを追加する。
3. `Override Root Name`に`Root`を入力する。
4. `Remapping`にインポートしたファイル名(拡張子なし)を入力する。
5. エディタを再生してPose Camで接続し、アバターが正しく動くことを確認する。  
   (デフォルトでは`Upper Body Only`が有効になっているため全身は動かない)
6. 以下のコンポーネントを追加する。
   - Avatar
   - Facial Expression
7. `Avatar`の`Hide Body`に以下のGameObjectを追加する。
   - Face
   - Body
   - Hair
8. プレハブ化したら`Live`シーンの`NetworkManager`の`NetworkPrefabs`にプレハブを追加する。

## コントローラのタイトル画面のメニューにアバターを追加する

未作成。