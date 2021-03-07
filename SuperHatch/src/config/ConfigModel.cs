// @author Zereao
// @date 2021-03-06 17:32
// @Steam https://steamcommunity/id/hexaiolun/

namespace SuperHatch.config
{
    /// <summary>哈奇模型</summary>
    public class ConfigModel
    {
        /// <summary>哈奇食物</summary>
        public string ConsumeName = "Sand";

        /// <summary>哈奇产出</summary>
        public string ProduceName = "Steel";

        /// <summary>每周期消耗量，单位 千克</summary>
        public float ConsumeEachCycle = 10f;

        /// <summary>
        /// 每周期产量，单位 千克
        /// </summary>
        public float ProduceEachCycle = 1000f;

        public override string ToString()
        {
            return
                $"【ConsumeName = {ConsumeName}, ProduceName = {ProduceName}, ConsumeEachCycle = {ConsumeEachCycle}, ProduceEachCycle = {ProduceEachCycle}】";
        }
    }
}