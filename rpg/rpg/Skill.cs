using System.Drawing;
using rpg;
public class Skill
{
    //skill技能数组
    public static Skill[] skill;

    public int mp = 10;
    public string name = "";
    public string description = "";
    public Bitmap bitmap;

    public int value1=0;
    public int value2=0;
    public int value3 = 0;
    public int value4 = 0;
    public int value5 = 0;

    //战斗使用
    public int canfuse = 0;                     //能否在战斗中使用
    public int fvalue1 = 0;                     //攻击值参数
    public int fvalue2 = 0;
    public Animation fanm;

    public void set(string name,string description,string bitmap_path,int mp,int value1,int value2,int value3,int value4,int value5)
    {
        this.name = name;
        this.description = description;

        if (bitmap_path != null && bitmap_path != "")
        {
            bitmap = new Bitmap(bitmap_path);
            bitmap.SetResolution(96,96);
        }

        this.mp = mp;
        this.value1 = value1;
        this.value2 = value2;
        this.value3 = value3;
        this.value4 = value4;
        this.value5 = value5;
 
    }

    public delegate void Use_event(Skill skill );                        //定义委托
    public event Use_event use_event;
    public void use()
    {
        if (Form1.player[Player.select_player].mp < mp)                        //mp判断
            return;

        Form1.player[Player.select_player].mp -= mp;                           //减去mp
        if (use_event != null)
            use_event(this);                                                     //使用技能
    }

    //type 0-解除  1-学得
    public static void learn_skill(int player_index, int index, int type)             //角色id，技能id,type
    {
        if (skill == null) return;
        if (index < 0) return;
        if (index >= skill.Length) return;
        if (skill[index] == null) return;

        if (type == 0)                                            //解除技能
        {
            for (int i = 0; i < Form1.player[player_index].skill.Length; i++)
            {
                if (Form1.player[player_index].skill[i] == index)
                    Form1.player[player_index].skill[i] = -1;                                        //设置空位
            }
        }
        else                                                         //学习技能
        {
            for (int i = 0; i < Form1.player[player_index].skill.Length; i++)
            {
                if (Form1.player[player_index].skill[i] == index)                              //已经习得
                    return;  
            }
            for (int i = 0; i < Form1.player[player_index].skill.Length; i++)
            {
                if (Form1.player[player_index].skill[i] == -1)                             //找到空位
                {
                    Form1.player[player_index].skill[i] = index;                           // 添加技能
                    return;
                }
            }
        }
    }

    //添加hp
    public static void add_hp(Skill skill)
    {
        Player player = Form1.player[Player.select_player];
        player.hp += skill.value1;
        if (player.hp > player.max_hp)
            player.hp = player.max_hp;
        if (player.hp < 0)
            player.hp = 0;

    }
    //减少hp
    public static void low_hp(Skill skill)
    {
        /*Player player = Form1.player[Player.select_player];
        player.hp -= skill.value1;
        if (player.hp > player.max_hp)
            player.hp = player.max_hp;
        if (player.hp < 0)
            player.hp = 0;
        */
    }
    //定义技能战斗属性
    public void fset(Animation fanm, int fvalue1, int fvalue2)
    {
        this.fanm = fanm;
        this.fvalue1 = fvalue1;
        this.fvalue2 = fvalue2;
        this.canfuse = 1;
        if (fanm != null)
            fanm.load();

    }
    //判断技能是否可用
    public bool check_fuse(int mp)
    {
        if (canfuse != 1)                //能否在战斗中使用
            return false;
        if (mp<this .mp)            //mp是否可用
            return false;
        return true;
    }




}