using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MyPlaneGame.Properties;

namespace MyPlaneGame
{
    /// <summary>
    /// 敌方飞机爆炸类
    /// </summary>
    class BoomEnemy:BoomFather
    {
        // 小飞机毁灭
        private Image[] imgsBoomEnemy0 =
        {
            Resources.enemy0_down11,
            Resources.enemy0_down2,
            Resources.enemy0_down3,
            Resources.enemy0_down4
        };
        // 中飞机毁灭
        private Image[] imgsBoomEnemy1 =
        {
            Resources.enemy1_hit,
            Resources.enemy1_down11,
            Resources.enemy1_down2,
            Resources.enemy1_down3,
            Resources.enemy1_down4
        };
         // 大飞机毁灭
        private Image[] imgsBoomEnemy2 =
        {
            Resources.enemy2_hit,
            Resources.enemy2_down11,
            Resources.enemy2_down2,
            Resources.enemy2_down3,
            Resources.enemy2_down4,
            Resources.enemy2_down5,
            Resources.enemy2_down6
        };

        // 构造函数
        public BoomEnemy(int x,int y, int type):base(x,y)
        {
            this.X = x;
            this.Y = y;
            this.Type = type;
        }
        // 敌方飞机类型
        public int Type
        {
            get;
            set;
        }
        // 重载Draw方法
        public override void Draw(Graphics g)
        {
            // 根据敌方飞机类型选择爆炸图像
            switch(this.Type)
            {
                case 0:
                    for(int i=0;i<imgsBoomEnemy0.Length;i++)
                    {
                        g.DrawImage(imgsBoomEnemy0[i],this.X,this.Y);
                    }
                    break;
                case 1:
                    for(int i=0; i<imgsBoomEnemy1.Length;i++)
                    {
                        g.DrawImage(imgsBoomEnemy1[i], this.X, this.Y);
                    }
                    break;
                case 2:
                    for(int i=0;i<imgsBoomEnemy2.Length;i++)
                    {
                        g.DrawImage(imgsBoomEnemy2[i], this.X, this.Y);
                    }
                    break;
            }
            // 移除敌方飞机爆炸效果
            SingleObject.GetSingle().RemoveGameObject(this);
        }

    }
}
