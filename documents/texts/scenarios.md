# シナリオ

## ユーザがライブに参加できる

メインアプリはタイトル画面から始まる。タイトル画面の基本的なUIはクライアントとコントローラで変わらない。

### ライブ開始

1. 運営がサーバ上のメインアプリを起動する。
2. サポータがサーバのIPアドレスを入力する。
3. サポータがメニューバーから「ライブを準備」ボタンを押す。
4. プレゼンタが「参加する」ボタンを押す。
5. ユーザがユーザ名を入力する。
6. ユーザがサーバのIPアドレスを入力する。
7. ユーザが「参加する」ボタンを押す。
8. クライアントが待機画面を表示する。
9. サポータがメニューバーから「ライブを開始」ボタンを押す。
   1. プレゼンタがサーバに接続していなかった場合はその旨を表示し、中断する。
10. クライアントがライブ画面を表示する。

### ライブ終了

1. サポータがメニューバーから「ライブを終了」ボタンを押す。
2. クライアントとコントローラがタイトル画面に戻る。
3. サーバが停止中の状態になる。

## プレゼンタがアバターを操作できる

### ライブ開始

既存のイテレーションとの差分。

1. `プレゼンタが「参加する」ボタンを押す。`のあと、運営がiOS端末のPose Camを起動する。
2. 初回のみ、運営が右上の設定アイコンからサーバ情報を設定する。
3. Pose Camが`tap to connect`と表示する。
4. 運営が画面をタップする。
5. Pose Camが`tap to stream`と表示する。
6. 運営が画面をタップする。
7. 運営が、プレゼンタの体が画面に収まるようにiOS端末を設置する。
8. もとのフローに戻る。

### ライブ終了

既存のイテレーションとの差分。

1. `サーバが停止中の状態になる。`のあと、運営がiOS端末のPose Camを閉じる。

## プレゼンタが音声を配信できる

ライブ画面での操作。

1. プレゼンタが発話する。
2. クライアントがプレゼンタの音声を再生する。

## ユーザがコメントを送信できる

ライブ画面での操作。

1. ユーザが「コメントを書く」ボタンを押す。
2. ユーザがコメントの内容を入力し、確定する。
3. クライアントとコントローラが、入力されたコメントを表示する。

## ユーザがエールを贈れる

ライブ画面での操作。

1. ユーザがプレゼントのボタンを押す。
2. クライアントがエール一覧を表示する。
3. ユーザが送りたいエールのボタンを押す。
4. クライアントがエール確認画面を表示する。
5. ユーザが「差し入れ」ボタンを押す。
6. ユーザが画面をスワイプする。
7. クライアントとコントローラが、贈られたエールをステージに表示する。

## プレゼンタがオブジェクトをつかめる

ライブ画面での操作。

1. プレゼンタがOculus Touchでオブジェクトに触る。
2. プレゼンタがOculus Touchのトリガーを引く。
3. つかまれたオブジェクトがアバターの手元を追跡する。
4. プレゼンタがOculus Touchのトリガーを離す。
5. つかまれたオブジェクトがアバターの手元から解放される。