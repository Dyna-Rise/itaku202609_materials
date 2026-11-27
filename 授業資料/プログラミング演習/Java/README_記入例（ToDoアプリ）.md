# ToDo リスト（JavaFX）

C# の経験をもとに、Java を2日間で学んで作成したデスクトップアプリです。

![画面](screenshot.png)

## できること
- やることの追加・完了・削除
- 残り件数の表示
- 終了しても内容を保存（テキストファイル）
- 自分で追加した機能：**期限日を付けて、期限が近い順に並べ替え**（DatePicker と Comparator を使用）

## 使った技術
- Java 25（Eclipse Temurin）
- JavaFX / FXML / Scene Builder
- Gradle
- IntelliJ IDEA

## 動かし方
1. このリポジトリをクローンする
2. IntelliJ IDEA で開く（JDK 25 を設定）
3. `HelloApplication` を実行する

## 工夫した点・学んだこと
- C# の LINQ（Where・Count）にあたる処理を、Java の Stream API（filter・count）で書き直した。考え方は同じで、名前が違うだけだと分かった
- C# のプロパティがない代わりに、getter と JavaFX の BooleanProperty を使い、チェックボックスと画面を自動で同期させた
- 文字列の比較は `==` ではなく `equals` を使う必要があることを、保存ファイルの読み込みで体験した

## 今後やってみたいこと
- 保存先をテキストファイルからデータベース（SQLite）に変える
- Spring Boot で同じ ToDo を Web アプリにしてみる
