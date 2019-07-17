using System.Windows.Forms;
using rpg;
public class Task
{
 /*   public static Player.Status player_last_status = Player.Status.WALK;
    public static int p = 0;
    public static void story(int i)
    {
        if (Player.status != Player.Status.TASK)
            player_last_status = Player.status;
        Player.status = Player.Status.TASK;

        DialogResult r1;
        if (i == 0) {
            Message.show("天依", "显示对话框显示对话框显示对话框显示对话框显示对话框显示对话框", "face1_1.png", Message.Face.LEFT);
            block();
            Message.show("亚斯娜", "显示对话框显示对话框显示对话框显示对话框显示对话框显示示对话框显示对话框显示对", "face3_2.png", Message.Face.RIGHT);
            block();
            Message.show("天依", "示对话框显示对话框显示对", "face1_1.png", Message.Face.LEFT);
            block();

        }
        if (i == 1) {
            Shop.show(new int[] {0,1,2,3,-1,-1,-1});  //商人所卖物品
            //Player.status = Player.Status.WALK;
            //Fight.start(new int[] { 0, 0, -1 }, "fight/f_scene.png", 1, 0, 1, 1, 100); 
            block();
        }
        if (i == 2) { Map.change_map(Form1.map, Form1.player, Form1.npc, 1, 725, 250, 2, Form1.music_player); }
        if (i == 3) { Map.change_map(Form1.map, Form1.player, Form1.npc, 0, 30, 500, 3, Form1.music_player); }
        if (i == 4) { Form1.npc[4].play_anm(0); }
        if (i == 5) { r1 = MessageBox.Show("我会走"); };
        if (i == 6)
        {
            if (p == 0)
            {
                Message.showtip("一只鞋");
                block();
            }
            else if (p == 1)
            {
                Message.showtip("捡起鞋");
                block();
                Form1.npc[6].x = -100;
                p = 2;
            }
            else if (p == 2)
            { }
        }
        if (i == 7)
        {
            if (p == 0)
            {
                Message.show("陌生人"," 年轻人，我看你长的不错啊","face4_2.png",Message.Face.RIGHT);
                block();
                Message.show("主角","小爷我不搞基的。","face2_1.png",Message.Face.LEFT);
                block();
                Message.show("陌生人","GG,不配合啊，不然剧情咋发展啊","face4_2.png",Message.Face.RIGHT);
                block();
                Message.show("主角","mmp,哪来的屁话，快说","face2_1.png",Message.Face.LEFT);
                block();
                Message.show("陌生人", "帮我在下面找个鞋", "face4_2.png", Message.Face.RIGHT);
                block();
                p = 1;
            }
            else if (p == 1)
            {
                Message.show("陌生人", "还不快去帮我捡鞋", "face4_2.png", Message.Face.RIGHT);
                block();
            }
            else if (p == 2)
            {
                Message.show("陌生人", "孺子可教也，来我这里有一本奇书，就此传授给你。", "face4_2.png", Message.Face.RIGHT);
                block();
                Message.showtip("获得《九阳正经》");
                block();
                Item.add_item(5,1);
                p = 3;
            }
            else if (p == 3)
            {
                Message.show("陌生人", "", "face4_2.png", Message.Face.RIGHT);
                block();
            }
        }

        Player.status = player_last_status;
    }

    public static void block()
    {
        while (Player.status == Player.Status.PANEL)
            Application.DoEvents();
    }
*/


    //控制变量
    public static int[] p = new int[100];                  //100个剧情变量
    public static Task[] task;                         //保存所有任务
    public static int id = 0;                          //当前任务id
    public static int step = 0;                      //当前的步住
    public static Player.Status player_last_status = Player.Status.WALK;
    public int npc_id = -1;           //预设条件1：触发的npc
    public enum VARTYPE
    {
        ANY=0,                  //不做处理
        EQUAL=1,             //相等
        GREATER=2,          //大于
        LESS=3,              //小于
    }
    public int cvar1_index = 0;                          //预设条件2：二组剧情变量
    public int cvar1 = 0;
    public VARTYPE cvar1_type = VARTYPE.ANY;
    public int cvar2_index = 0;
    public int cvar2 = 0;
    public VARTYPE cvar2_type = VARTYPE.ANY;
    public int money = 0;                                 //预设条件3：金钱
    public VARTYPE money_type = VARTYPE.ANY;               
    //预设条件判断
    public int check_conditions(int index)
    {
        //预设条件
        //id
        if (index != npc_id)
            return -1;
        //var1
        if (cvar1_type == VARTYPE.EQUAL)
        {
            if (p[cvar1_index] != cvar1)
                return -1;
        }
        else if (cvar1_type == VARTYPE.GREATER)
        {
            if (p[cvar1_index] <= cvar1)
                return -1;
        }
        else if (cvar1_type == VARTYPE.LESS)
        {
            if (p[cvar1_index] >= cvar1)
                return -1;
        }
        //var2
        if (cvar2_type == VARTYPE.EQUAL)
        {
            if (p[cvar2_index] != cvar2)
                return -1;
        }
        else if (cvar2_type == VARTYPE.GREATER)
        {
            if (p[cvar2_index] <= cvar2)
                return -1;
        }
        else if (cvar2_type == VARTYPE.LESS)
        {
            if (p[cvar2_index] >= cvar2)
                return -1;
        }
        //money
        if (money_type == VARTYPE.EQUAL)
        {
            if (Player.money != cvar2)
                return -1;
        }
        else if (money_type == VARTYPE.GREATER)
        {
            if (Player.money <= cvar2)
                return -1;
        }
        else if (money_type == VARTYPE.LESS)
        {
            if (Player.money >= cvar2)
                return -1;
        }
        return 0;  
    }
    public enum VARRESULT
    {
        NOTHING=0,    //不处理
        ASSIGN=1,       //赋值
        ADD=2,           //加
        SUB=3,           //减
    }
    public int rvar1_index = 0;
    public int rvar1 = 0;
    public VARRESULT rvar1_type = VARRESULT.NOTHING;
    public int rvar2_index = 0;
    public int rvar2 = 0;
    public VARRESULT rvar2_type = VARRESULT.NOTHING;

    //预设结果处理
    public void deal_result()
    {
        //预设结果
        //rvar1
        if (rvar1_type == VARRESULT.ASSIGN)
        {
            p[rvar1_index] = rvar1;
        }
        else if (rvar1_type == VARRESULT.ADD)
        {
            p[rvar1_index] += rvar1;
        }
        else if (rvar1_type == VARRESULT.SUB)
        {
            p[rvar1_index] -= rvar1;
        }
        //rvar2
        if (rvar2_type == VARRESULT.ASSIGN)
        {
            p[rvar2_index] = rvar2;
        }
        else if (rvar2_type == VARRESULT.ADD)
        {
            p[rvar2_index] += rvar2;
        }
        else if (rvar2_type == VARRESULT.SUB)
        {
            p[rvar2_index] -= rvar2;
        }
    }
    public delegate int Task_event(int index,int step);
    public event Task_event evt;
    //task_event返回值
    //1处理成功  -1条件判断失败
    //其他走到第几步
    public int task_event(int task_id, int step)
    {
        int ret = 0;
        if (evt != null)
        {
            ret = evt(task_id,step);
            Task.step = ret;
        }
        return ret;
    }

    public int var1 = 0;    //辅助变量
    public int var2 = 0;
    public int var3 = 0;
    public int var4 = 0;
    //——---------------------------------------
    //             set的重裁
    //-------------------------------------------
    public void set(int npc_id,Task_event evt,int cvar1_index,int cvar1,VARTYPE cvar1_type,int cvar2_index,int cvar2,VARTYPE cvar2_type,int money,VARTYPE money_type,int rvar1_index,int rvar1,VARRESULT rvar1_type,int rvar2_index,int rvar2,VARRESULT rvar2_type,int var1,int var2,int var3,int var4)
    {
        this.npc_id = npc_id;
        this.evt += evt;
        this.cvar1 = cvar1;
        this.cvar1_index = cvar1_index;
        this.cvar1_type = cvar1_type;
        this.cvar2 = cvar2;
        this.cvar2_index = cvar2_index;
        this.cvar2_type = cvar2_type;
        this.money = money;
        this.money_type = money_type;
        this.rvar1 = rvar1;
        this.rvar1_index = rvar1_index;
        this.rvar1_type = rvar1_type;
        this.rvar2 = rvar2;
        this.rvar2_index = rvar2_index;
        this.rvar2_type = rvar2_type;
        this.var1 = var1;
        this.var2 = var2;
        this.var3 = var3;
        this.var4 = var4;
 
    }
    public void set(int npc_id, Task_event evt, int cvar1_index, int cvar1, VARTYPE cvar1_type, int rvar1_index, int rvar1, VARRESULT rvar1_type, int var1, int var2, int var3, int var4)
    {
        set(npc_id,evt,cvar1_index,cvar1,cvar1_type,0,0,VARTYPE.ANY,0,VARTYPE.ANY,rvar1_index, rvar1,rvar1_type,0,0,VARRESULT.NOTHING, var1, var2, var3, var4);
    }
    public void set(int npc_id, Task_event evt, int cvar1_index, int cvar1, VARTYPE cvar1_type, int rvar1_index, int rvar1, VARRESULT rvar1_type)
    {
        set(npc_id,evt,cvar1_index,cvar1,cvar1_type,rvar1_index,rvar1,rvar1_type,0,0,0,0);
    }
    public void set(int npc_id, Task_event evt, int var1, int var2, int var3, int var4)
    {
        set(npc_id,evt,0,0,VARTYPE.ANY,0,0,VARRESULT.NOTHING,var1,var2,var3,var4);
    }
    public void set(int npc_id, Task_event evt)
    {
        set(npc_id,evt,0,0,0,0);
    }
    //----------------------------------------------------------------------------------
    // 任务流程，普通的调用还是步骤调用
    //-----------------------------------------------------------------------------------------
    public static void story(int npc_id, int type)//0-正常 1-回调
    {
        //保存状态
        if (Player.status != Player.Status.TASK)
            player_last_status = Player.status;
        Player.status = Player.Status.TASK;
        //事件
        if (task == null) return;
        if (type == 1 && id >= 0)
        {
            int ret = task[id].task_event(id,step);
            if (ret == 0)
                task[id].deal_result();
        }
        else if(type==0)
            for (int i = task.Length - 1; i >= 0; i--)
            {
                if (task[i] == null) continue;
                if (task[i].check_conditions(npc_id) != 0) continue;
                id = i;
                step = 0;
                int ret = task[i].task_event(i,step);
                if (ret == 0)
                    task[i].deal_result();
                break;
            }
        //恢复状态
        Player.status = player_last_status;
    }
    public static void story(int i)
    {
        story(i,0);
    }
    //切换地图
    //var1 map_id
    //var2 var3 坐标
    //var4 面向
    public static int change_map(int task_id, int step)
    {
        if (task == null) return -1;
        if (task[task_id] == null) return - 1;
        int map_id = task[task_id].var1;
        int x = task[task_id].var2;
        int y = task[task_id].var3;
        int f = task[task_id].var4;
        Map.change_map(Form1.map,Form1.player,Form1.npc,map_id,x,y,f,Form1.music_player);
        return 0;
    }

    //------------------------------------------------------------------------------
    //                             功能封装
    //------------------------------------------------------------------------------
    public static void change_map(int map_id, int x, int y, int face) //切换地图
    {
        Map.change_map(Form1.map, Form1.player, Form1.npc, map_id, x, y, face, Form1.music_player);
    }
    public static void talk(string name, string str, string face, Message.Face fpos)//对话
    {
        Message.show(name,str,face,fpos);
        block();
    }
    public static void talk(string name, string str, string face)
    {
        Message.show(name,str,face,Message.Face.LEFT);
        block();
 
    }
    public static void tip(string str)//提示
    {
        Message.showtip(str);
        block();
    }
    public static void set_npc_pos(int npc_id, int x, int y)//设置npc位置
    {
        if (Form1.npc == null) return;
        if (Form1.npc[npc_id] == null) return;

        Form1.npc[npc_id].x = x;
        Form1.npc[npc_id].y = y;
    }
    public static void play_npc_anm(int npc_id, int anm_id)//播放npc动画
    {
        if (Form1.npc == null) return;
        if (Form1.npc[npc_id] == null) return;
        Form1.npc[4].play_anm(anm_id);
 
    }
    public static void fight(int [] enemy,string bg_path,int isgameover,int winitem1,int winitem2,int winitem3,int losemoney)//战斗
    {
        Fight.start(enemy,bg_path,isgameover,winitem1,winitem2,winitem3,losemoney);
    }
    public static void add_item(int item_id, int num)//增减物品
    {
        Item.add_item(item_id,num);
    }
    public static void learn_skill(int p_id, int skill_id, int type)//学习技能
    {
        Skill.learn_skill(p_id,skill_id,type);
    }
    public static void recover()//恢复
    {
        if (Form1.player == null) return;
        for (int i = 0; i < Form1.player.Length; i++)
        {
            if (Form1.player[i] == null) continue;
            Form1.player[i].hp = Form1.player[i].max_hp;
            Form1.player[i].mp = Form1.player[i].max_mp;
        }
        tip("完全恢复！");
    }
    public static void save()//保存
    {
        
    }
    public static void block()//阻断
    {
        Form1.set_timer_interval(100);
        while (Player.status == Player.Status.PANEL)
            Application.DoEvents();
        Form1.set_timer_interval(50);
    }


}