#  サブシステム間インターフェース

## 通信の種類

### ソケット通信

C#のソケット通信の機能による通信。HTTPのように応答を受け取れるので、上りと下りがある。

- ポート: TCP/5239
- 形式: 文字列

### NetCode for GameObjects

Unityの`NetCode for GameObjects`による通信。

- ポート: UDP/5239
- 形式: NetCode for GameObjects依存
  - おそらく中身はメソッドとその引数

### Pose Cam

Pose AIのPose Camによる通信。姿勢情報を伝送するために使用する。

- ポート: UDP/39828以降。プレゼンタが増えるごとに1ずつ増加
- 形式: Pose Cam依存

## コンソール→サーバ

### コンソールから命令を出す

ソケット通信を用いる。

- 上り
  - 命令の種類を表す文字列
- 下り
  - 処理結果(成功したか、失敗したか)

## クライアント/コントローラ→サーバ

### サーバの状態を確認する

ソケット通信を用いる。

- 上り
  - 「サーバの状態を確認する」ことを表す文字列
- 下り
  - 現在のサーバの状態(停止中か、準備中か、ライブ中か)

### 撮影形式を送信する

NetCode for GameObjectsを用いる。  
コントローラから以下の情報を送信する。

- 撮影範囲(`Mode`): この内のいずれかを指定する(`Room Body Only`と`Use Upper Body Only`は違いがわからなかったため不使用)。
  - 横長 (`Room`)
  - 縦長 (`Portrait`)
  - 椅子 (`Desktop`)
- 上半身のみ反映する (`Use Upper Body Only`)

## サーバ→クライアント/コントローラ

### 画面遷移指示

NetCode for GameObjectsを用いる。

![コミュニケーション図のようなもの](..\diagrams\export\communications\1_joinLive.svg)

## Pose Cam→サーバ

### 姿勢情報を送信する

Pose Camを用いる。