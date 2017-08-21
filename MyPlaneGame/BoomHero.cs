using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MyPlaneGame.Properties;

namespace MyPlaneGame
{
    /// <summary>
    /// 玩家飞机爆炸类
    /// </summary>
    class BoomHero : BoomFather
    {
        // 存储玩家飞机爆炸图片
        private Image[] imgsBoomHero =
        {
            Resources.hero_blowup_n1,
            Resources.hero_blowup_n2,
            Resources.hero_blowup_n3,
            Resources.hero_blowup_n4
        };

        // 构造函数
        public BoomHero(int x, int y) : base(x, y)
        {
            this.X = x;
            this.Y = y;
        }

        // 重写Draw方法
        public override void Draw(Graphics g)
        {
            // 顺序播放玩家飞机爆炸图片
            for (int i = 0; i < imgsBoomHero.Length; i++)
            {
                g.DrawImage(this.imgsBoomHero[i], this.X-20, this.Y-20);
            }
            // 爆炸后移除玩家飞机爆炸效果
            SingleObject.GetSingle().RemoveGameObject(this);
        }
    }
}
