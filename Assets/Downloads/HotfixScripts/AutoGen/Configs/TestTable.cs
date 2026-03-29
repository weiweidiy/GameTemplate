/*
* 此类由ConfigTools自动生成. 不要手动修改!
*/
using System.Collections;
using System.Collections.Generic;
using JFramework.Game;

namespace JFramework
{
    public partial class TestTable : BaseConfigTable<TestCfgData>
    {
    }

    public class TestCfgData : IUnique
    {
        //唯一标识
        public string Uid{ get;set;} 

        //整数值类型
        public int IntValue;

        //浮点值类型
        public float FloatValue;

        //字符串类型
        public string StringValue;

        //整数值数组类型
        public List<int> IntArray;

        //浮点值数组类型
        public List<float> FloatArray;

    }
}
