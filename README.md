## 概要
フリーソフトで公開されている『[WifiMonitor](https://www.projectgroup.info/software/WifiMonitor.html)』を学習も兼ねてそれっぽく作ってみた。<br>

### 動作について
netsh コマンドを実行して、その出力結果をリストビューに表示<br>
取得した結果をループ処理で１秒間隔に更新<br>
<br>

## 開発環境
- Windows11 Pro (23H2)
- Visual Studio 2022 x64 - Ver.17.13.6
- .NET Framework 4.8
<br>

## 更新履歴
|更新日|バージョン|<div align="center">更新内容</div>|
:-:|:-:|:-
|2025.05.13|Ver.1.0.0|初版作成|
|2025.07.02|Ver.1.1.0|・[ツール] メニューを追加<br>・[電波取得一時停止] ボタンを追加<br>・取得中ランプを表示（赤色に点滅）<br>・接続している無線の受信速度を表示|
|2025.07.03|Ver.1.2.0|・接続している無線の項目を「太字」かつ背景色を「ライトグレー」で強調表示|
