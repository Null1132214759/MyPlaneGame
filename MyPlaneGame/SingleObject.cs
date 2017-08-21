using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPlaneGame
{
    /// <summary>
    /// 单例设计模式：创建单例类
    /// </summary>
    class SingleObject
    {
        // 1.构造函数私有化
        private SingleObject()
        { }
        // 2.声明全局唯一对象
        private static SingleObject single = null;
        // 3.声明一个返回该对象的方法
        public static SingleObject GetSingle()
        {
            if (single == null)
            {
                single = new SingleObject();
            }
            return single;
        }
        #region 单一对象
        // 存储游戏背景唯一对象
        public GameBackground background
        {
            get;
            set;
        }
        // 存储游戏唯一标题对象
        public GameTitle title
        {
            get;
            set;
        }
        // 存储玩家飞机唯一对象
        public PlaneHero hero
        {
            get;
            set;
        }
        // 存储敌方飞机对象集合单一实例
        public List<PlaneEnemy> PlaneEnemyList = new List<PlaneEnemy>();
        // 存储玩家飞机子弹集合单一实例
        List<BulletHero> BulletHeroList = new List<BulletHero>();
        // 存储敌方飞机子弹单一集合实例
        List<BulletEnemy> BulletEnemyList = new List<BulletEnemy>();
        // 存储玩家飞机爆炸效果单一集合实例
        List<BoomHero> BoomHeroList = new List<BoomHero>();
        // 存储敌方飞机爆炸效果单一集合实例
        List<BoomEnemy> BoomEnemyList = new List<BoomEnemy>();

        #endregion

        // 玩家得分
        public int Score
        {
            get;
            set;
        }

        // 将游戏对象添加到窗体
        public void AddGameObject(GameObject go)
        {
            if(go is GameBackground)
            {
                this.background = go as GameBackground;
            }
            else if(go is GameTitle)
            {
                this.title = go as GameTitle;
            }
            else if(go is PlaneHero)
            {
                this.hero = go as PlaneHero;
            }
            else if(go is BulletHero)
            {
                this.BulletHeroList.Add(go as BulletHero);
            }
            else if(go is PlaneEnemy)
            {
                this.PlaneEnemyList.Add(go as PlaneEnemy);
            }
            else if(go is BulletEnemy)
            {
                this.BulletEnemyList.Add(go as BulletEnemy);
            }
            else if(go is BoomHero)
            {
                this.BoomHeroList.Add(go as BoomHero);
            }
            else if(go is BoomEnemy)
            {
                this.BoomEnemyList.Add(go as BoomEnemy);
            }
        }


        // 将游戏对象从窗体删除
        public void RemoveGameObject(GameObject go)
        {
            if(go is GameTitle)
            {
                this.title = null;
            }
            else if(go is PlaneHero)
            {
                this.hero = null;
            }
            else if(go is BulletHero)
            {
                this.BulletHeroList.Remove(go as BulletHero);
            }
            else if(go is PlaneEnemy)
            {
                this.PlaneEnemyList.Remove(go as PlaneEnemy);
            }
            else if(go is BulletEnemy)
            {
                this.BulletEnemyList.Remove(go as BulletEnemy);
            }
            else if(go is BoomHero)
            {
                this.BoomHeroList.Remove(go as BoomHero);
            }
            else if(go is BoomEnemy)
            {
                this.BoomEnemyList.Remove(go as BoomEnemy);
            }
        }

        // 绘制开始界面
        public void DrawFirst(Graphics g)
        {
            if (this.background != null)
            {
                this.background.Draw(g); // 绘制游戏背景
            }
            if (this.title != null)
            {
                this.title.Draw(g); // 绘制游戏标题
            }
            if (this.hero != null)
            {
                this.hero.Draw(g); //绘制玩家飞机
            }
        }

        // 绘制游戏对象
        public void DrawObject(Graphics g)
        {
            if (this.background != null)
            {
                this.background.Draw(g); // 绘制游戏背景
            }
            if(this.hero!=null)
            {
                this.hero.Draw(g); //绘制玩家飞机
            }
            if (BulletHeroList != null) 
            {
                for(int i=0;i<BulletHeroList.Count;i++) 
                {
                    BulletHeroList[i].Draw(g); // 绘制玩家飞机子弹
                }
            }
            if (this.PlaneEnemyList != null)
            {
                for(int i=0;i<PlaneEnemyList.Count;i++)
                {
                    PlaneEnemyList[i].Draw(g); // 绘制敌方飞机
                }
            }
            if(this.BulletEnemyList!=null)
            {
                for(int i=0;i<BulletEnemyList.Count;i++)
                {
                    BulletEnemyList[i].Draw(g);  // 绘制敌方飞机子弹
                }
            }
            if(this.BoomHeroList!=null)
            {
                for(int i=0;i<BoomHeroList.Count;i++)
                {
                    BoomHeroList[i].Draw(g);  // 绘制玩家飞机爆炸效果
                }
            }
            if(this.BoomEnemyList!=null)
            {
                for(int i=0;i<BoomEnemyList.Count;i++)
                {
                    BoomEnemyList[i].Draw(g);  // 绘制敌方飞机爆炸效果

                }
            }
        }

        // 游戏对象碰撞检测
        public void MeetCheck()
        {
            // 1. 玩家子弹是否击中敌方飞机
            for(int i=0;i<BulletHeroList.Count;i++)
            {
                for(int j=0;j<PlaneEnemyList.Count;j++)
                {
                    if(BulletHeroList[i].GetRectangle().IntersectsWith(PlaneEnemyList[j].GetRectangle()))
                    {
                        // 1.敌人生命值减少
                        PlaneEnemyList[j].Life -= BulletHeroList[i].Power;
                        // 2.判断敌方飞机是否坠毁
                        PlaneEnemyList[j].IsOver();
                        // 3. 销毁玩家子弹对象
                        BulletHeroList.Remove(BulletHeroList[i]);
                        break;
                    }
                }
            }

            // 2.判断敌人子弹是否打到玩家飞机
            for(int i=0;i<BulletEnemyList.Count;i++)
            {
                if(BulletEnemyList[i].GetRectangle().IntersectsWith(hero.GetRectangle()))
                {
                    // 使玩家发生爆炸
                    hero.IsOver();
                    break;
                }
            }

            // 判断玩家飞机是否与敌人飞机发生碰撞
            for(int i=0;i<PlaneEnemyList.Count;i++)
            {
                if (PlaneEnemyList[i].GetRectangle().IntersectsWith(this.hero.GetRectangle()))
                {
                    // 敌人死亡
                    PlaneEnemyList[i].Life = 0;
                    PlaneEnemyList[i].IsOver();
                    break;
                }
            }
        }
    }
}
