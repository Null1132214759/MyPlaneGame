using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MyPlaneGame.Properties;

namespace MyPlaneGame
{
    /// <summary>
    /// 玩家飞机子弹类
    /// </summary>
    class BulletHero:BulletFather
    {
        private static Image imgBulletHero = Resources.bullet0;
        // 构造函数
        public BulletHero(PlaneFather pf, int speed,int power)
            :base(pf,imgBulletHero,speed,power)
        {

        }
    }
}
