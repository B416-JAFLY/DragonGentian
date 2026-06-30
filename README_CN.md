# 龙胆 (Dragon Gentian) — 杀戮尖塔2 模组

[![Build and Release](https://github.com/B416-JAFLY/DragonGentian/actions/workflows/build.yml/badge.svg)](https://github.com/B416-JAFLY/DragonGentian/actions/workflows/build.yml)

[English](README.md)

---

### 概述

**龙胆 (Dragon Gentian)** 是一个为 **杀戮尖塔2** 设计的自定义卡牌模组，为 **储君 (Regent)** 角色添加了一张独特的 **能力牌**。打出后获得「辉星与能量 1:1 互通」的效果——任何一种资源都可以当作另一种来使用。

![预览](screenshots/preview.png)

---

### 卡牌详情

| 属性 | 基础 | 升级后 |
|------|------|--------|
| **名称** | 龙胆 | 龙胆+ |
| **费用** | 2 能量 | 1 能量 |
| **类型** | 能力 | 能力 |
| **稀有度** | 罕见 | 罕见 |
| **卡池** | 储君 | 储君 |
| **升级加成** | — | 费用 -1，获得 **固有** |

**效果（能力 — 龙胆）：**
> 你可以把辉星当作能量，能量当作辉星来使用。

打出后，为角色施加 **龙胆** Buff。效果持续期间：
- 能量不够，辉星富余 → 自动用辉星补能量（1:1）
- 辉星不够，能量富余 → 自动用能量补辉星（1:1）
- 转换在每张卡打出时自动计算，无需手动操作

---

### 技术实现

模组使用 **Harmony** 补丁劫持了三个关键游戏方法：

1. `CardEnergyCost.GetWithModifiers` — 判断卡牌能否打出（能量检查）
2. `CardEnergyCost.GetAmountToSpend` — 决定实际扣除多少能量
3. `CardModel.GetStarCostWithModifiers` — 决定辉星费用

`SplitComputer` 算法会检测玩家当前的能量和辉星储备，然后重新计算混合费用，最大化资源利用。

---

### 安装方法

1. **环境要求：**
   - 杀戮尖塔2 **v0.107.0** 或更高
   - [BaseLib](https://github.com/Alchyr/StS2-BaseLib) **v3.3.0** 或更高

2. **安装步骤：**
   - 从 [Releases](https://github.com/B416-JAFLY/DragonGentian/releases) 下载最新版
   - 解压到 `<STS2>/mods/DragonGentian/`
   - 最终目录结构：
     ```
     mods/DragonGentian/
       DragonGentian.dll
       DragonGentian.json
       DragonGentian.pck
     ```

3. **启动游戏** — 模组自动加载。

---

### 使用方法

1. 选择 **储君 (Regent)** 开始新一局游戏
2. **龙胆**卡牌会自动加入初始卡组（开发测试用途）
3. 打出该卡激活 Buff
4. Buff 栏会出现「龙胆」图标
5. 之后整场战斗中辉星和能量均可互通使用！

> **提示：** 自动加入初始卡组是开发便利设定。正式发布版本中，这张牌会正常通过卡牌奖励、商店等方式出现在储君的卡池中。

---

### 从源码构建

```bash
# 前置条件：.NET 9 SDK, Godot 4.5.1 Mono

# 1. 导入资源（生成 .import 文件）
godot --headless --path . --import

# 2. 编译 DLL
dotnet build -c Release

# 3. 导出 PCK
godot --headless --path . --export-pack "BasicExport" DragonGentian.pck

# 4. 部署到 mods 目录（构建目标自动处理）
```

---

### 致谢

- **作者:** oran
- **卡面绘制:** oran
- **底层支持:** BaseLib by Alchyr, Harmony

---

### 许可证

MIT
