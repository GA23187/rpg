using System.Drawing;
using System.Windows.Forms;

public class Npc
{
    //位置
    public int map = -1;    //npc所在地图id
    public int x = 0;          //坐标
    public int y = 0;
    public int x_offset = -50;           //绘图偏移量
    public int y_offset = -90;
    //显示
    public string bitmap_path="";          //图片路径
    public Bitmap bitmap;
    public bool visible = true;                        //是否可见

    //碰撞区域
    public int region_x = 35;
    public int region_y = 35;

    //动画
    public Animation[] anm;      //anm 代表这个npc所能使用的所有动画
    public int anm_frame = 0;     //当前播放的帧
    public int current_anm = -1;    //当前使用的动画，对应于anm数组的下标。-1表示没有播放
    public long last_anm_time = 0;    //上一帧播放的时间，用于调控播放速

   //人物类
    public Comm.Direction face = Comm.Direction.DOWN;
    public int walk_frame = 0;            //行走的动画帧
    public long last_walk_time = 0;            //最后一次行走的时间
    public long walk_interval = 80;                // 行走间隔
    public int speed = 40;                          //速度
    public Comm.Direction idle_walk_direction = Comm.Direction.DOWN;                 //    行走方向，上下和左右
    public int idle_walk_time = 0;                                                       //往每个方向走的帧数，也是控制住每个方向行走的距离
    public int idle_walk_time_now = 0;                                         //当前行走的时间


    //定义枚举    普通和人物类型npc
    public enum Npc_type
    {
        NORMAL=0,
        CHARACTER=1,
    }
    public Npc_type npc_type = Npc_type.NORMAL;

    //鼠标碰撞区域
    public int mc_xoffset = -20;
    public int mc_yoffset = -20;
    public int mc_w = 50;
    public int mc_h = 50;
    public static int mc_distance_x = 100;                     //在一定距离内单击才有效
    public static int mc_distance_y = 200;

    //加载
    public void load()
    {
        if (bitmap_path != "")
        {
            bitmap = new Bitmap(bitmap_path);
            bitmap.SetResolution(96,96);
        }

        if (anm != null)                 //动画资源
        {
            for (int i = 0; i < anm.Length; i++)
                anm[i].load();
        }
        //鼠标碰撞区域
        if (bitmap != null)
        {
            if (mc_w == 0)
                mc_w = bitmap.Width;
            if (mc_h == 0)
                mc_h = bitmap.Height;
        }
        else if (npc_type == Npc_type.CHARACTER)
        {
            if (mc_w == 0)
                mc_w = bitmap.Width / 4;
            if (mc_h == 0)
                mc_h = bitmap.Height / 4;
        }
        else
        {
            if (mc_w == 0)
                mc_w = region_x;
            if (mc_h == 0)
                mc_h = region_y;
        }

    }

    //卸载
    public void unload()
    {
        if (bitmap != null)
        {
            bitmap = null;
        }

        if (anm != null)                    //动画资源
        {
            for (int i = 0; i < anm.Length; i++)
                anm[i].unload();
        }
    }
    //绘图，判断当前是否播放动画
    public void draw(Graphics g, int map_sx, int map_sy)
    {
              //绘制角色
        if (visible != true)
            return;
        if(current_anm < 0)
          {
              if (npc_type == Npc_type.NORMAL)
              {
                  if (bitmap != null)
                      g.DrawImage(bitmap, map_sx + x + x_offset, map_sy + y + y_offset);
              }
              else if (npc_type == Npc_type.CHARACTER)
              {
                  draw_character(g,map_sx,map_sy);    //绘制Character类型对象
              }
          }
      
/*        if (visible != true)
            return;
      if (current_anm < 0)
        {
            if (bitmap != null)
                g.DrawImage(bitmap, map_sx + x + x_offset, map_sy + y + y_offset);   //绘制npc
        }
*/
        else 
        {
            draw_anm(g,map_sx,map_sy);           //绘制动画
        }
    }

    //碰撞
    public bool is_collision(int collision_x, int collision_y)
    {
        Rectangle rect = new Rectangle(x-region_x/2,y-region_y/2,region_x,region_y);
        return rect.Contains(new Point(collision_x,collision_y));
    }

    //线碰撞
    public bool is_line_collision(Point p1, Point p2)
    {
        if (is_collision(p2.X, p2.Y)) return true;              //p2点

        int px, py;                                                       //1/2点
        px = p1.X + (p2.X - p1.Y) / 2;
        py = p1.Y + (p2.Y - p1.Y) / 2;
        if (is_collision(px, py)) return true;

        px = p2.X - (p2.X - p1.X) / 4;                           //3/4点
        py = p2.Y - (p2.Y - p1.Y) / 4;
        if (is_collision(px, py)) return true;

        return false;
    }

    //定义枚举    触发类型
    public enum Collosion_type
    {
        KEY=1,
        ENTER=2,
    }
    public Collosion_type collision_type = Collosion_type.KEY; //默认触发为按键

    //动画绘图
    public void draw_anm(Graphics g, int map_sx, int map_sy)
    {
        if (anm == null || current_anm >= anm.Length || anm[current_anm] == null || anm[current_anm].bitmap_path == null)
        {
            current_anm = -1;
            anm_frame = 0;
            last_anm_time = 0;
            return;
        }

        anm[current_anm].draw(g, anm_frame, map_sx + x + x_offset, y + map_sy + y_offset);      //调用Animation类的draw方法

        if (Comm.Time() - last_anm_time >= Animation.RATE)
        {
            anm_frame = anm_frame + 1;
            last_anm_time = Comm.Time();
            if (anm_frame / anm[current_anm].anm_rate >= anm[current_anm].max_frame)
            {
                current_anm = -1;
                anm_frame = 0;
                last_anm_time = 0;
            }
        }

    }

    //事件,设置当前要播放的动画
    public void play_anm(int index)
    {
        current_anm = index;
        anm_frame=0;
    }

   //画character角色
    public void draw_character(Graphics g, int map_sx, int map_sy)
    {
        Rectangle rent = new Rectangle(bitmap.Width/4*(walk_frame%4),bitmap.Height/4*((int)face-1),bitmap.Width/4,bitmap.Height/4);
        Bitmap bitmap0 = bitmap.Clone(rent,bitmap.PixelFormat);
        g.DrawImage(bitmap0,map_sx+x+x_offset,map_sy+y+y_offset);
    }


    //---------------------------------------------------
    //isblock为false表示不受地图限制

    //处理人物类npc的行走
    public void walk(Map[] map, Comm.Direction direction, bool isblock) 
    {
        //转向
        face = direction;
        //间隔判断
        if (Comm.Time() - last_walk_time <= walk_interval)
            return;
        //行走
        //up
        if (direction == Comm.Direction.UP && (!isblock || Map.can_through(map, x, y - speed)))
        {
            y = y - speed;
        }
        //down
        else if (direction == Comm.Direction.DOWN && (!isblock || Map.can_through(map, x, y + speed)))
        {
            y = y + speed;
        }
          //left
        else if (direction == Comm.Direction.LEFT && (!isblock || Map.can_through(map, x - speed, y)))
        {
            x = x - speed;
        }
            //right
        else if (direction == Comm.Direction.RIGHT && (!isblock || Map.can_through(map, x + speed, y)))
        {
            x = x+ speed;
        }
        //动画帧
        walk_frame = walk_frame + 1;
        if (walk_frame >= int.MaxValue) walk_frame = 0;
        //时间
        last_walk_time = Comm.Time();

    }

    //处理停止行走的状态
    public void stop_walk()
    {
        walk_frame = 0;
        last_walk_time = 0;
    }

    //定时处理npc的一些逻辑//处理行走时间间隔，调用walk来处理npc行走
    public void timer_logic(Map[] map)
    {
/*     if (idle_walk_time != 0)
            walk(map,Comm.Direction.DOWN,false);
*/
      if (npc_type == Npc_type.CHARACTER && idle_walk_time != 0)                        //人物类npc才会执行
        {
            Comm.Direction direction;                                                                    //根据idle_walk_time_now的正负判断方向大于等于为正
            if (idle_walk_time_now >= 0)
                direction = idle_walk_direction;
            else
                direction = Comm.opposite_direction(idle_walk_direction);

            walk(map,direction,true);                                   //处理行走

            if (idle_walk_time_now >= 0)                                  //正方向处理时间
            {
                idle_walk_time_now = idle_walk_time_now + 1;
                if (idle_walk_time_now > idle_walk_time)
                    idle_walk_time_now = -1;                          //  转向
            }
            else if (idle_walk_time_now < 0)
            {
                idle_walk_time_now = idle_walk_time_now - 1;
                if (idle_walk_time_now < -idle_walk_time)
                    idle_walk_time_now = 1;
            }
        }

    }

    //鼠标碰撞
    public bool is_mouse_collision(int collision_x, int collisoin_y)
    {
        //有图
        if (bitmap != null)
        {
            if (npc_type == Npc_type.NORMAL)        //普通型
            {
                int center_x = x + x_offset + bitmap.Width / 2;
                int center_y = y + y_offset + bitmap.Height / 2;
                Rectangle rect = new Rectangle(center_x - mc_w / 2, center_y - mc_h / 2, mc_w, mc_h);
                return rect.Contains(new Point(collision_x, collisoin_y));

            }
            else                                             //人物型
            {
                int center_x = x + x_offset + bitmap.Width / 4 / 2;
                int center_y = y + y_offset + bitmap.Height / 4 / 2;
                Rectangle rect = new Rectangle(center_x - mc_w / 2, center_y - mc_h / 2, mc_w, mc_h);
                return rect.Contains(new Point(collision_x, collisoin_y));
            }
        }
        //无图
        else
        {
            Rectangle rect = new Rectangle(x-mc_w/2,y-mc_h/2,mc_w,mc_h);
            return rect.Contains(new Point(collision_x, collisoin_y));
        }
    }

    //距离检测
    public bool check_mc_distance(Npc npc, int player_x, int player_y)
    {
        Rectangle rect = new Rectangle(npc.x-mc_distance_x/2,npc.y-mc_distance_y/2,mc_distance_x,mc_distance_y);
        return rect.Contains(new Point(player_x,player_y));
    }

    //鼠标操作
    public static void mouse_click(Map[] map, Player[] player, Npc[] npc, Rectangle stage, MouseEventArgs e)
    {
        if (Player.status != Player.Status.WALK)
            return;

        if (npc == null)
            return;

        for (int i = 0; i < npc.Length; i++)
        {
            if (npc[i] == null)
                continue;

            if (npc[i].map != Map.current_map)
                continue;

            int collision_x = e.X - Map.get_map_sx(map,player,stage);                                        //获取坐标
            int collision_y = e.Y - Map.get_map_sy(map, player, stage);                                
            if (!npc[i].is_mouse_collision(collision_x, collision_y))                                    //碰撞检测
                continue;

            //距离
            if (!npc[i].check_mc_distance(npc[i], Player.get_pos_x(player), Player.get_pos_y(player)))                       //  距离检测
            {
                Player.stop_walk(player);                                                    //距离太远了
                Message.showtip("请走近点");
                Task.block();
                continue;
            }
            Player.stop_walk(player);                                                          //触发事件
            Task.story(i);
        }
    }

    //检测鼠标是否在npc上
    public static int check_mouse_collision(Map[] map, Player[] player, Npc[] npc, Rectangle stage, MouseEventArgs e)
    {
        if (Player.status != Player.Status.WALK)                           //状态判断
            return 0;
        if (npc == null)
            return 0;
        for (int i = 0; i < npc.Length; i++)
        {
            if (npc[i] == null)
                continue;
            if (npc[i].map != Map.current_map)
                continue;

            int collision_x = e.X - Map.get_map_sx(map, player, stage);                     //获取碰撞坐标
            int collision_y = e.Y - Map.get_map_sy(map, player, stage);
            if (npc[i].is_mouse_collision(collision_x, collision_y))                              //判断是发生碰撞
            {
                return 1;                                         //碰到为1
            }
        }
        return 0;                                     //没有为0

    }

}