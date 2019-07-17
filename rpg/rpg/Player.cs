using System.Windows.Forms;
using System.Drawing;

public class Player
{
    //当前角色
    public static int current_player = 0;
    //行走
    public int x = 0;
    public int y = 0;
    public int face = 1;
    public int anm_frame = 1;
    public long last_walk_time = 0;
    public long walk_interval = 100;
    public int speed = 20;
    public Bitmap bitmap;
    public int x_offset = -48; //位置调整
    public int y_offset = -90;
    //是否激活
    public int is_active = 0;
    //碰撞射线长度
    public int collision_ray = 50;

    //主角状态
    public enum Status
    {
        WALK = 1,
        PANEL = 2,
        TASK = 3,
        FIGHT = 4,
    }
    public static Status status = Status.WALK;

    //鼠标操作
    public static int target_x = -1;
    public static int target_y = -1;

    //鼠标单击位置
    public static Bitmap move_flag;            //标记图片
    public static long FLAG_SHOW_TIME = 3000;             //标记时间
    public static long flag_start_time = 0;                 //开始显示时间

    //物品，技能，状态栏
    public int max_hp = 100;
    public int hp = 100;
    public int max_mp = 100;
    public int mp = 100;
    public int attack = 10;
    public int defense = 10;
    public int fspeed = 10;
    public int fortune = 10;
    public int equip_att = -1;         //武器id
    public int equip_def = -1;         //防具id
    public static int select_player = 0;
    public Bitmap status_bitmap;                     //状态面板图像
    public int[] skill = { -1,-1,-1,-1,-1,-1,-1,-1,-1,-1};                 //技能数组
    public static int money = 200;

    //战斗
    public Bitmap fbitmap;            //战斗图
    public int fx_offset = -120;        //用于确定fbitmap的位置
    public int fy_offset = -120;
    public Bitmap fface;                //角色面部图
    public Animation anm_att;          //攻击动画
    public Animation anm_item;             //物品动画
    public Animation anm_skill;            //技能动画
    public string name = "";


    public Player()
    {
        bitmap = new Bitmap(@"role/r1.png");
        bitmap.SetResolution(96,96);

        move_flag = new Bitmap(@"ui/move_flag.png");
        bitmap.SetResolution(96,96);
    }

    //简化操控key_ctrl,  整理为walk， 减少重复代码
    public static void walk(Player[] player, Map[] map, Comm.Direction direction)
    {
        Player p = player[current_player];
        //转向
        p.face = (int)direction;
        //间隔判定
        if (Comm.Time() - p.last_walk_time <= p.walk_interval)
            return;
        //移动处理
        if (direction == Comm.Direction.UP && Map.can_through(map, p.x, p.y - p.speed))      //up                             //加上判断目标位置是否可以通行
            p.y = p.y - p.speed;
        else if (direction == Comm.Direction.DOWN && Map.can_through(map, p.x, p.y + p.speed))         //down                          //行走速度由speed控制
            p.y = p.y + p.speed;
        else if (direction == Comm.Direction.LEFT && Map.can_through(map, p.x - p.speed, p.y))    //left
            p.x = p.x - p.speed;
        else if (direction == Comm.Direction.RIGHT && Map.can_through(map, p.x + p.speed, p.y))      //right
            p.x = p.x + p.speed;
        else return;
        //动画帧
        p.anm_frame = p.anm_frame + 1;
        if (p.anm_frame >= int.MaxValue) p.anm_frame = 0;     //超出上限的处理
        //时间
        p.last_walk_time = Comm.Time();
    }


    //-----------------------------------------------------------------
    //        操控       
    //-----------------------------------------------------------------
    public static void key_ctrl(Player[] player,Map[] map,Npc[] npc,KeyEventArgs e)
    {
        Player p=player[current_player];
        if (Player.status != Status.WALK)
            return;
        //切换角色
        if (e.KeyCode == Keys.Tab) { key_change_player(player); }
 /*       //是否转向                                      //先处理转向在处理行走
        if (e.KeyCode == Keys.Up && p.face != 4)
            p.face = 4;
        else if (e.KeyCode == Keys.Down && p.face != 1)
            p.face = 1;
        else if (e.KeyCode == Keys.Left && p.face != 2)
            p.face = 2;
        else if (e.KeyCode == Keys.Right && p.face != 3)
            p.face = 3;
        //间隔判定
        if (Comm.Time() - p.last_walk_time <= p.walk_interval)
            return;
        //移动处理
        if (e.KeyCode == Keys.Up&&Map.can_through(map,p.x,p.y-p.speed))                                   //加上判断目标位置是否可以通行
            p.y = p.y - p.speed;
        else if (e.KeyCode == Keys.Down && Map.can_through(map, p.x, p.y + p.speed))                                   //行走速度由speed控制
            p.y = p.y + p.speed;
        else if (e.KeyCode == Keys.Left && Map.can_through(map, p.x - p.speed, p.y))
            p.x = p.x - p.speed;
        else if (e.KeyCode == Keys.Right && Map.can_through(map, p.x + p.speed, p.y))
            p.x = p.x + p.speed;
        else return;
        //动画帧
        p.anm_frame = p.anm_frame + 1;
        if (p.anm_frame >= int.MaxValue) p.anm_frame = 0;     //超出上限的处理
        //时间
        p.last_walk_time = Comm.Time();
 */
        //行走
        if (e.KeyCode == Keys.Up)
        {
            walk(player,map,Comm.Direction.UP);
        }
        else if (e.KeyCode == Keys.Down)
        {
            walk(player,map,Comm.Direction.DOWN);
        }
        else if (e.KeyCode == Keys.Left)
        {
            walk(player, map, Comm.Direction.LEFT);
        }
        else if (e.KeyCode == Keys.Right)
        {
            walk(player, map, Comm.Direction.RIGHT);
        }
        else if (e.KeyCode == Keys.Escape)
        {
            StatusMenu.show();
            Task.block();
        }
        //npc碰撞
        npc_collision(player,map,npc,e);
    }

    public static void key_ctrl_up(Player[] player, KeyEventArgs e)
    {
/*        Player p=player[current_player];
        //动画帧
        p.anm_frame = 0;
        p.last_walk_time = 0;
*/
        stop_walk(player);
    }

    public static void draw(Player[] player,Graphics g,int map_sx,int map_sy)
    {
        Player p=player[current_player];
        Rectangle crazycoderRgl = new Rectangle(p.bitmap.Width / 4 * (p.anm_frame% 4), p.bitmap.Height / 4 * (p.face - 1), p.bitmap.Width / 4, p.bitmap.Height / 4);//定义区域
        Bitmap bitmap0 = p.bitmap.Clone(crazycoderRgl, p.bitmap.PixelFormat);//复制小图
        g.DrawImage(bitmap0, p.x+map_sx+p.x_offset, p.y+map_sy+p.y_offset);
    }

    //-------------------------------------------------------------------
    //    切换角色
    //-------------------------------------------------------------------
    public static void key_change_player(Player[] player)
    { 
        for(int i=current_player+1;i<player.Length;i++)
            if (player[i].is_active == 1)
            {
                set_player(player,current_player,i);
                return;
            }
        for(int i=0;i<current_player;i++)
            if (player[i].is_active == 1)
            {
                set_player(player,current_player,i);
                return;
            }
    }

    public static void set_player(Player[] player, int oldindex, int newindex)//3个参数分别表示玩家数组player,旧的可控角色索引oldindex，新的可控角色索引newindex
    {
        current_player = newindex;
        player[newindex].x = player[oldindex].x;
        player[newindex].y= player[oldindex].y;
        player[newindex].face = player[oldindex].face;
    }

    //位置设置
    public static void set_pos(Player[] player, int x, int y, int face)
    {
        player[current_player].x = x;
        player[current_player].y = y;
        player[current_player].face = face;

    }

    public static int get_pos_x(Player[] player)//横坐标
    {
        return player[current_player].x;
    }

    public static int get_pos_y(Player[] player)//纵坐标
    {
        return player[current_player].y;
    }

    public static int get_pos_f(Player[] player)//面向
    {
        return player[current_player].face;
    }

    //根据角色方向计算碰撞射线终点
    public static Point get_collision_point(Player[] player)                
    {
        Player p=player[current_player];
        int collision_x = 0;
        int collision_y = 0;

        if (p.face == (int)Comm.Direction.UP)  //上
        {
            collision_x = p.x;
            collision_y = p.y - p.collision_ray;
        }
        if (p.face == (int)Comm.Direction.DOWN)   //下
        { 
            collision_x = p.x;
            collision_y = p.y + p.collision_ray;
        }
        if (p.face == (int)Comm.Direction.LEFT)   //左
        {
            collision_x = p.x-p.collision_ray;
            collision_y = p.y;
        }
        if (p.face == (int)Comm.Direction.RIGHT)    //右
        {
            collision_x = p.x + p.collision_ray;
            collision_y = p.y;
        }
        return new Point(collision_x,collision_y);
    }


    //处理角色与npc碰撞
    public static void npc_collision(Player[] player, Map[] map, Npc[] npc, KeyEventArgs e)
    {
        Player p=player[current_player];                      //碰撞射线的端点
        Point p1 = new Point(p.x,p.y);
        Point p2 = get_collision_point(player);

        for (int i = 0; i < npc.Length; i++)                    //遍历NPC
        {
            if (npc[i] == null)
                continue;
            if (npc[i].map != Map.current_map)
                continue;

            if (npc[i].is_line_collision(p1, p2))                            //发生碰撞
            {
                if (npc[i].collision_type == Npc.Collosion_type.ENTER)                 //碰撞触发
                {
                    Task.story(i);
                    break;
                }
                else if (npc[i].collision_type == Npc.Collosion_type.KEY)                //按键触发
                {
                    if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
                    {
                        Task.story(i);
                        break;
                    }
                }
            }
        }
    }

    public static int is_reach_x(Player[] player, int target_x)           //横坐标
    {
        Player p=player[current_player];

        if (p.x - target_x > p.speed / 2) return 1;
        if (p.x - target_x < -p.speed / 2) return -1;
        return 0;
    }

    public static int is_reach_y(Player[] player, int target_y)
    {
        Player p=player[current_player];

        if (p.y - target_y > p.speed / 2) return 1;
        if (p.y - target_y < -p.speed / 2) return -1;
        return 0;
    }

    //角色处于行走状态且玩家按下鼠标左键，设置目标坐标
    public static void mouse_click(Map[] map, Player[] player, Rectangle stage, MouseEventArgs e)
    {
        if (Player.status != Status.WALK)
            return;
        if (e.Button == MouseButtons.Left)
        {
            target_x = e.X - Map.get_map_sx(map,player,stage);
            target_y = e.Y - Map.get_map_sy(map, player, stage);

            flag_start_time = Comm.Time();
        }
        else if (e.Button == MouseButtons.Right)                 //鼠标右键弹出面板
        {
            StatusMenu.show();
            Task.block();
        }
    }

    //处理角色的定时逻辑
    public static void timer_logic(Player[] player, Map[] map)
    {
        move_logic(player,map);
    }

    //处理角色行走的方法
    public static void move_logic(Player[] player, Map[] map)
    {
        if (target_x < 0 || target_y < 0)
            return;
        step_to(player,map,target_x,target_y);
    }

    public static void stop_walk(Player[] player)
    {
        Player p=player[current_player];
        //动画帧
        p.anm_frame = 0;
        p.last_walk_time = 0;
        //目标位置
        target_x = -1;
        target_y = -1;
    }

    //行走算法
    public static void step_to(Player[] player, Map[] map, int target_x, int target_y)
    {
        if (is_reach_x(player, target_x) == 0 && is_reach_y(player, target_y) == 0)         //判断是否到达目的地
        {
            stop_walk(player);
            return;
        }
        Player p=player[current_player];
        if (is_reach_x(player, target_x) > 0 && Map.can_through(map, p.x - p.speed, p.y))                      //能否往x方向行走(左)
        {                                                                                                                                           //行走
            walk(player,map,Comm.Direction.LEFT);
            return;
        }
        else if (is_reach_x(player, target_x) < 0 && Map.can_through(map, p.x + p.speed, p.y))              //能否往x反方向行走(右)
        {
            walk(player,map,Comm.Direction.RIGHT);
            return;
        }
        else if (is_reach_y(player, target_y) > 0 && Map.can_through(map, p.x, p.y - p.speed))              //能否往y正方向行走(上)
        {
            walk(player, map, Comm.Direction.UP);
            return;
        }
        else if (is_reach_y(player, target_y) < 0 && Map.can_through(map, p.x, p.y + p.speed))              //能否往y反方向行走(下)
        {
            walk(player, map, Comm.Direction.DOWN);
            return;
        }
        stop_walk(player);
    }

    //绘制鼠标标记
    public static void draw_flag(Graphics g, int map_sx, int map_sy)
    {
        if (target_x < 0 || target_y < 0)
            return;
        if (move_flag == null)
            return;
        if (Comm.Time() - flag_start_time > FLAG_SHOW_TIME)
            return;

        g.DrawImage(move_flag,map_sx+target_x-16,map_sy+target_y-17);            //16,17为偏移参数
    }

    //设置战斗
    public void fset(string name, string fbitmap_path, int fx_offset, int fy_offset, string fface_path, Animation anm_att, Animation anm_item, Animation anm_skill)
    {
        this.name = name;
        if (fbitmap_path != null && fbitmap_path != "")
        {
            this.fbitmap = new Bitmap(fbitmap_path);
            this.fbitmap.SetResolution(96, 96);
        }
        this.fx_offset = fx_offset;
        this.fy_offset = fy_offset;
        if (fface_path != null && fface_path != "")
        {
            this.fface = new Bitmap(fface_path);
            this.fface.SetResolution(96, 96);
        }
        this.anm_att = anm_att;
        this.anm_item = anm_item;
        this.anm_skill = anm_skill;

        anm_att.load();
        anm_item.load();
        anm_skill.load();

    }
}