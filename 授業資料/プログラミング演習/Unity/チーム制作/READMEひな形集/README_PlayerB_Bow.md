# MONSTER ACTION RED ― 弓プレイヤー担当

> チーム制作の 3D モンスター討伐アクションゲームで、**弓タイプのプレイヤー**を担当しました。
> □ 長押しで弓を引き絞り、離すと矢を放つ「溜め撃ち」と、L1 集中モード中の「照準撃ち」を実装しています。

![ゲーム画面](docs/images/bow_main.png)
<!-- ↑ 自分のプレイ画面のスクリーンショット / GIF に差し替えてください -->

| 項目 | 内容 |
|---|---|
| 制作期間 | 20XX年XX月〜XX月（約 X 週間） |
| チーム人数 | 5 人（弓 / 魔法 / ボス / ショップ / 装備） |
| 担当 | 弓プレイヤー（攻撃アクション・矢・エフェクト） |
| 開発環境 | Unity 6（6000.3）/ C# / URP / Input System / TextMeshPro |
| チーム開発 | Git / GitHub（ブランチ運用・Pull Request でマージ） |
| プレイ動画 | [YouTube](https://example.com) |

---

## 担当した機能

### 1. 溜め撃ち（□ 長押し → 離して発射）
- □ を押すと弓を引くモーション（Load）、0.4 秒たつと構えたまま待つモーション（Hold）に切り替わります。
- □ を離した時点で、引いていた時間が **1 秒以上なら「溜め矢」**、未満なら「ふつうの矢」を放ちます。
- 溜め矢はダメージが大きく、見た目も大きい別の Prefab を使っています。
- 引いている途中でも回避（ローリング）でキャンセルできます。

### 2. 照準撃ち（L1 集中モード）
- 集中モード中は、カメラ中央の照準が指している点へ向かって矢を飛ばします。上下の角度も付くので、高い位置にあるボスの頭も狙えます。
- 通常時はキャラクターの正面へ水平に撃ちます。

### 3. 矢（BowArrow）
- Rigidbody で飛ばし、Trigger で当たりを判定しています。
- 当たった相手から `IDamageable` を探して、`ApplyDamage` でダメージを与えます。
- 撃った本人には当たらない、回避中（無敵）の相手はすり抜ける、何にも当たらなければ一定時間で消える、という処理を入れています。

### 4. ヒットエフェクト
- 矢が当たった位置にエフェクトを出します。

---

## 工夫したところ

### 時間の管理を「ステートの経過時間」に一本化した
共通の親クラスが持っている `StateTime`（攻撃を始めてからの秒数）だけで、引く・構える・放つ・戻るのタイミングを管理しました。
溜め時間を数える変数を別に持たなくてよくなり、コードが短くなりました。

```csharp
if (!Input.AttackHeld)                      // □ を離した
{
    if (StateTime >= chargeTime)            // 1 秒以上引いていた
        Fire(chargedDamage, true, superArrowPrefab);
    else if (StateTime >= 0.3f)
        Fire(damage, false, arrowPrefab);
}
```

### 相手の種類を気にせずダメージを与えられるようにした
矢は「当たった相手がボスかカカシか」を知りません。共通インターフェース `IDamageable` を通してダメージを渡すだけです。
そのため、テスト用のカカシで確認した処理が、コードを変えずにそのままボスにも効きました。

```csharp
IDamageable target = other.GetComponentInParent<IDamageable>();
if (target != null && target.IsAlive)
{
    var info = new DamageInfo(damage, hitPoint, transform.forward, owner, causesFlinch: flinch);
    if (target.ApplyDamage(info)) Destroy(gameObject);
}
```

### 「一度だけ実行する処理」をフラグで守った
構えのモーションへの切り替えは、毎フレーム呼ぶとモーションが最初から再生し直されてガクガクになります。
`holdAnimStarted` フラグで 1 回だけ実行するようにしました。

---

## 苦労したところと解決方法

| 問題 | 原因 | 解決 |
|---|---|---|
| ボス戦のシーンだと矢が出ない | 矢の Prefab を**シーン上のプレイヤーにだけ**設定していて、Prefab 本体が空のままだった | Prefab モードで設定し直した。シーンの上書きと Prefab の違いを理解できた |
| 矢がボスに当たっても HP が減らない | 矢が受け取ったダメージ量を保存しておらず、当たったときに消えるだけだった | ダメージ量と撃った人を変数に保存し、当たったときに `ApplyDamage` を呼ぶようにした |
| 撃った瞬間に矢が消える | 矢が自分の体の当たり判定に触れていた | 撃った本人とその子オブジェクトを当たり判定から除外した |
| 溜め矢とふつうの矢のダメージが逆 | `Fire()` に渡す引数を入れ違えていた | 動作確認用のカカシでダメージのログを見て気づき、修正した |

---

## 担当したファイル

```
Assets/Game/Scripts/Battle/Player/Types/Bow/
  ├ PlayerTypeB_Bow.cs   … 弓プレイヤーの攻撃（PlayerBattleController を継承）
  └ BowArrow.cs          … 矢の移動と当たり判定
Assets/Game/Prefabs/Arrow.prefab, superArrow.prefab
Assets/Game/Animations/Player/PlayerB_Bow.controller（BowLoad / BowHold / BowRelease を追加）
```

---

## 学んだこと
- **継承**：移動・回避・被弾などの共通処理は親クラスに任せ、自分は攻撃部分だけを `override` して書く作り方を学びました。
- **インターフェース**：`IDamageable` のおかげで、ボス担当とお互いのコードを知らなくても連携できました。
- **チーム開発**：Prefab とシーンの設定の違い、`.meta` ファイルの意味、Pull Request でのマージの流れを、実際につまずきながら覚えました。

## 今後やりたいこと
- 溜めの段階を 3 段階にして、最大溜めで 3 本同時に撃つ
- 矢が刺さったまま残る演出
- 当たり判定を SphereCast に変えて、速い矢でもすり抜けないようにする
