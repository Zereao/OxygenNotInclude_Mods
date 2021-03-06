# 超级哈奇 | 钢之炼金哈奇 | SuperHatch

## 一、主要功能

1. 全局配置所有哈奇、所有食物的消耗产出关系。
2. 自定义哈奇针对特定食物的消耗产出关系。

补充说明：

    消耗产出关系：哈奇对食物每周期的消耗量，以及吃了食物后的产物、每周期的产量。

## 二、支持版本

### 1、支持游戏本体，DLC未测试

经过有DLC的朋友测试，当前版本的mod能够完美适配DLC。

### 2、开发计划

1. 如果没有新的功能需求，后期不再添加新功能，只提供针对游戏新版本的兼容更新。

## 三、配置文件

    配置文件路径：C:/Users/{用户名}/Documents/Klei/OxygenNotIncluded/mods/config/SuperHatch/SuperHatch.json

当Mod加载后，会尝试去上面这个路径下读取配置文件。若文件不存在，则会自动在该路径下创建默认的配置文件：

```jsonc
{
  // 全局配置
  "GlobalConfig": {
    "ConsumeEachCycle": -1.0,     // 每周期食物消耗量；单位：千克，kg
    "ProduceEachCycle": -1.0      // 每周期产物产量；单位：千克，kg
  },
  // 自定义配置
  "CustomConfig": [
    {
      "ConsumeName": "Sand",        // 食物代码，沙子
      "ProduceName": "Steel",       // 产物代码，钢
      "ConsumeEachCycle": 10.0,     // 每周期消耗量；单位：千克，kg
      "ProduceEachCycle": 1000.0    // 每周期产量；单位：千克，kg
    }
  ]
}
```

### 1、全局配置

在全局配置中，目前支持配置两个属性：每周期食物消耗量(ConsumeEachCycle) 以及 每周期产物产量(ProduceEachCycle)。默认情况下，这两个配置的值都被设置为 **-1.0**。

全局配置文件的含义是：**所有哈奇，每天对所有食物的消耗量都为：ConsumeEachCycle，并且每周期产物的产量都为：ProduceEachCycle。**

全局配置的生效规则是：

* 当 ConsumeEachCycle 的值【小于0】时，全局配置将失效，无论 ConsumeEachCycle 的值配置为多少。
* 当 ProduceEachCycle 的值【小于0】时，该属性的配置将失效。

也就是说，使用默认配置时，全局配置将不会生效。

### 2、自定义配置

自定义配置用于 **指定哈奇食谱中，特定食物的消耗产出关系。** 默认配置文件中，自定义配置的含义是：**任意哈奇** ，如果其 **默认食谱(没装该mod时的食谱)中存在沙子(Sand)这种食物**，那么就有 **消耗产出关系：** 每周期消耗10千克沙子，产出1000千克钢(Steel)。

    针对配置文件中提到的食物代码，相关详情可以参考：[https://oni-db.com](https://oni-db.com)。需要注意的是，**对于某些名称中存在 空格 的物品代码，需要去掉空格。比如：铜矿(Copper Ore)，要写为【CopperOre】，不含括号**。

需要注意的是：好吃好奇的默认食谱中，就有沙子。这就意味着，当该mod装好后，使用默认配置时，好吃哈奇对沙子的消耗产出关系就跟上面说的一样。而对于其他食物，比如砂岩、淤泥根等，配置文件中没有配置，则其消耗产出关系不受影响。再比如，光滑哈奇的默认食谱中没有沙子，则其消耗产出关系不受该配置影响。

自定义配置还支持配置多条消耗产出关系：

```jsonc
{
  // 全局配置
  "GlobalConfig": {
    "ConsumeEachCycle": -1.0,     // 每周期食物消耗量；单位：千克，kg
    "ProduceEachCycle": -1.0      // 每周期产物产量；单位：千克，kg
  },
  // 自定义配置
  "CustomConfig": [
    {
      "ConsumeName": "Sand",        // 食物代码，沙子
      "ProduceName": "Steel",       // 产物代码，钢
      "ConsumeEachCycle": 10.0,     // 每周期消耗量；单位：千克，kg
      "ProduceEachCycle": 1000.0    // 每周期产量；单位：千克，kg
    },
    {
      "ConsumeName": "SedimentaryRock",   // 食物代码，沉积岩
      "ProduceName": "Glass",             // 产物代码，玻璃
      "ConsumeEachCycle": 5.0,            // 每周期消耗量；单位：千克，kg
      "ProduceEachCycle": 1000.0          // 每周期产量；单位：千克，kg
    }
  ]
}
```
上述配置的自定义配置中，第一条配置，由于只有好吃哈奇吃沙子，故只影响好吃好奇对于沙子的消耗产出关系。

而对于第二条配置，消耗物是 沉积岩(SedimentaryRock)，而 好吃哈奇、石壳哈奇 都可以吃沉积岩。故这条配置会同时影响 好吃哈奇、石壳哈奇 对沉积岩的消耗产出关系。
所以上述配置会造成的影响有：

* 好吃好奇，每周期可以吃10千克沙子，并产出1000千克钢；**或者** 吃5千克沉积岩，产出1000千克玻璃(Glass)；
* 石壳哈奇，每周期可以吃5千克沉积岩，产出1000千克玻璃；

### 3、全局配置与自定义配置同时配置

全局配置与自定义配置的优先级为：**自定义配置 > 全局配置**。

```jsonc
{
  // 全局配置
  "GlobalConfig": {
    "ConsumeEachCycle": 100.0,      // 每周期食物消耗量；单位：千克，kg
    "ProduceEachCycle": 2000.0      // 每周期产物产量；单位：千克，kg
  },
  // 自定义配置
  "CustomConfig": [
    {
      "ConsumeName": "Sand",        // 食物代码，沙子
      "ProduceName": "Steel",       // 产物代码，钢
      "ConsumeEachCycle": 10.0,     // 每周期消耗量；单位：千克，kg
      "ProduceEachCycle": 1000.0    // 每周期产量；单位：千克，kg
    }
  ]
}
```

例如，当有上面的配置时，将会产生下面的影响：

* 除了沙子外，哈奇对所有食物的需求为每天100千克，产出为每天2000千克。产物为原始产物(和没装该mod时的产物一样)。
* 哈奇对沙子的需求为每天10千克，产物为钢，产量为每天1000千克。

## 四、捐赠

1. 暂不需要，交个朋友~