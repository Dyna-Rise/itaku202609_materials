# MONSTER ACTION RED ― 魔法プレイヤー担当

> チーム制作の 3D モンスター討伐アクションゲームで、**魔法タイプのプレイヤー**を担当しました。
> □ を短く押すと魔法弾、長押しすると自分の周りに範囲魔法が出る「押し方で技が変わる」操作を実装しています。

![ゲーム画面](docs/images/magic_main.png)
<!-- ↑ 魔法弾と範囲魔法が出ている瞬間の GIF がおすすめです -->

| 項目 | 内容 |
|---|---|
| 制作期間 | 20XX年XX月〜XX月（約 X 週間） |
| チーム人数 | 5 人（弓 / 魔法 / ボス / ショップ / 装備） |
| 担当 | 魔法プレイヤー（魔法弾・範囲魔法・エフェクト・モーション） |
| 開発環境 | Unity 6（6000.3）/ C# / URP / Input System / TextMeshPro |
| チーム開発 | Git / GitHub（ブランチ運用・Pull Request でマージ） |
| プレイ動画 | [YouTube](https://example.com) |

---

## 担当した機能

### 1. 魔法弾（□ を短く押す）
- 詠唱時間（0.3 秒）のあとに、正面へ魔法弾を撃ちます。
- 弾は何かに当たると小さく爆発し、周囲 1m の敵にダメージを与えます。

### 2. 範囲魔法（□ 長押し）
- 詠唱時間を過ぎても □ を押し続けていると「溜め」に入り、自分を中心に半径 5m の範囲魔法を放ちます。
- 範囲内のすべての敵にダメージを与えます。
- エフェクトを出して、3 秒後に自動で消します。

### 3. 状態の切り替え（Cast / Charge / Burst）
攻撃の段階を `enum Step` で管理し、`switch` 文で分けて処理しています。

```
□ を押す → Cast（詠唱）
             ├ 0.3 秒の時点で離していた → 魔法弾を撃つ → 移動に戻る
             └ 0.3 秒の時点で押していた → Charge（溜め）→ Burst（範囲魔法）→ 0.8 秒で移動に戻る
```

### 4. モーションとエフェクトの選定
- アセットの詠唱モーション（Directional / Omnidirectional / Call）を試し、見た目が自然なものを選びました。
- 魔法弾と範囲魔法の見た目（色・大きさ・エフェクト）を調整しました。

---

## 工夫したところ

### 範囲魔法で「同じボスに何度も当たる」のを防いだ
ボスは頭・胴・しっぽなど、部位ごとに当たり判定を持っています。そのまま範囲内の当たり判定を全部数えると、1 回の魔法で何度もダメージが入ってしまいます。
部位から持ち主（ボス本体）を取り出して、**ボス単位で 1 回だけ**当たるようにしました。

```csharp
Collider[] hits = Physics.OverlapSphere(transform.position + Vector3.up, burstRadius,
                                        ~0, QueryTriggerInteraction.Collide);
List<IDamageable> alreadyHit = new List<IDamageable>();

foreach (Collider col in hits)
{
    if (col.transform.IsChildOf(transform)) continue;           // 自分の体は除外
    IDamageable target = col.GetComponentInParent<IDamageable>();
    if (target == null || !target.IsAlive) continue;

    IDamageable boss = target;
    if (target is IDamagePart part) boss = part.Owner;          // 部位なら本体を取り出す
    if (alreadyHit.Contains(boss)) continue;                     // もう当てたボスは飛ばす
    alreadyHit.Add(boss);

    target.ApplyDamage(new DamageInfo(burstDamage * AttackMultiplier, col.bounds.center, dir, gameObject));
}
```

### 段階ごとの経過時間を測れるようにした
段階を切り替えたときの時刻を `stepStartTime` に覚えておき、`StateTime - stepStartTime` で「今の段階に入ってから何秒か」を計算しています。
溜めた長さに関係なく、範囲魔法のあとは毎回同じ時間で移動に戻れます。

### スキルの「攻撃力アップ」を反映した
ダメージには、ショップで買えるスキルの倍率 `AttackMultiplier` をかけています。ほかの担当が作った成長要素と連動するようにしました。

---

## 苦労したところと解決方法

| 問題 | 原因 | 解決 |
|---|---|---|
| 魔法弾がボスに当たってもダメージが入らない | 弾に「当たったら消える」処理しかなかった | 弾が受け取ったダメージ量を保存し、当たった周囲に `ApplyDamage` を呼ぶようにした |
| 最初の詠唱モーションがボウリングの投球のように見えた | 片手で前へ放るモーションだった | 両手で溜める Load と放つ Cast を組み合わせ直した |
| 保存前のファイルをコミットしてしまった | Unity で保存（Ctrl+S）する前に Git でコミットしていた | 「Unity で保存 → Git の変更一覧を確認 → コミット」の順番を決めて守るようにした |

---

## 担当したファイル

```
Assets/Game/Scripts/Battle/Player/Types/Magic/
  ├ PlayerTypeC_Magic.cs   … 魔法プレイヤーの攻撃（PlayerBattleController を継承）
  └ MagicBullet.cs         … 魔法弾の移動と爆発
Assets/Game/Prefabs/Player/MagicBullet.prefab, BurstEffect.prefab
Assets/Game/Animations/Player/PlayerC_Magic.controller（Cast / ChargeLoad / ChargeCast を追加）
```

---

## 学んだこと
- **enum と switch による状態管理**：「今どの段階か」を変数で持ち、段階ごとに処理を分けると、複雑な操作も整理して書けることが分かりました。
- **Physics.OverlapSphere**：範囲攻撃の作り方と、Trigger の当たり判定を拾うには `QueryTriggerInteraction.Collide` の指定が必要なことを学びました。
- **インターフェース**：`IDamageable` / `IDamagePart` を使うと、ボスの中身を知らなくても部位単位で正しくダメージを与えられました。

## 今後やりたいこと
- 溜めの長さで範囲魔法を 3 段階に強化する
- 集中モード中は照準の方向へ魔法弾を撃つ
- 溜め中にオーラのエフェクトを出し、中断されたら消す
