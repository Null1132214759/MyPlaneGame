using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPlaneGame
{
    /// <summary>
    /// 抽象类：飞机爆炸基类
    /// </summary>
    abstract class BoomFather:GameObject
    {
        // 只需得到爆炸飞机的坐标并播放爆炸图片
        public BoomFather(int x,int y)
            :base(x,y)
        {

        }
    }
}
