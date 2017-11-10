# GetKinectBody
kinectでbodyのjoint取ってきて可視化するもの。

Pluginsの中にあるdllの中でKinectとやり取り。
dllの中はcppで書かれてる。
なぜc#じゃないかというと私は画像引っ張ってきてそれをopenCVでごちゃごちゃすることが多いからその都合。

PlayerManagerからPlayerにデータ流して可視化してる。
なのでそこらへん読むと使い方がわかる。

適当に使いまわしてもらって構わない。
(需要なさそう)
kinectもう生産中止だしねー。v3でるのかな。出てほしい。

私の環境は
win10
vc14.0
Unity2017.2、
