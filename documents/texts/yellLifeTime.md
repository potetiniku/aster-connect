# エールの寿命

## 概要

この文書では、エールの寿命(エールがステージから消える条件)を定義する。

## 検証

### 時間

エールは、ステージに出現してからの時間を条件に消えることがある。

参考元サービスで、適当な個体が消えるまでの時間を観察した。

| サイズ | 名前         | 必要アスタジェム | 寿命(秒) |
| ------ | ------------ | ---------------- | -------- |
| M      | 骨付き肉     | 50               | 30       |
| M      | ドーナツ     | 50               | 30       |
| M      | ラーメン     | 50               | 30       |
| S      | 肉まん       | 25               | 120      |
| S      | はにわ       | 25               | 190      |
| S      | 誕生日ケーキ | 150              | 246      |
| S      | ハンバーガー | 25               | 300      |

Mサイズのエールでは、±5秒ほどの小さなばらつきが見られた。対して、Sサイズのエールではばらつきが非常に大きかった。  
より正確な結果を得るにはもっと多くの個体を観察する必要がある。

### 全体の個数

必要に応じて実装。

エールがステージ上を覆い尽くすのを防ぐため、全体のエールの個数が一定に達した場合に古いエールを消す必要がある。

参考元サービスでは、Mサイズのエールには全体の個数に上限を設けておらず(クジラの件を見る限り)、Lサイズのエールには5個の制限を設けている。  
ただし、文字が出る前に花火が寿命を迎えるような部分もラストライブでは見られる。

## 定義

| サイズ | 時間     | 全体の個数 |
| ------ | -------- | ---------- |
| S      | 120±10秒 | 50個       |
| M      | 30±5秒   | 30個       |
| L      | 関係なし | 5個        |