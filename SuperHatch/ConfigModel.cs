// @author Zereao
// @date 2021-03-06 17:32
// @Steam https://steamcommunity/id/hexaiolun/

namespace SuperHatch
{
    /// <summary>哈奇模型</summary>
    public class ConfigModel
    {
        /// <summary>哈奇食物</summary>
        public string ConsumeName = "Sand";

        /// <summary>哈奇产出</summary>
        public string ProduceName = "Steel";

        /// <summary>
        /// 转换率；
        /// 1 - 100%转换；2 - 吃100kg，产出200kg；0.75 - 吃100kg，产出75kg
        /// </summary>
        public float ConversionRate = 2f;

        /// <summary>每周期消耗量 kg</summary>
        public float EatenEachCycle = 10f;
    }
}