using System.Drawing;
using rpg;

public class Item
{
    //item数组
    public static Item[] item;
    public int num = 0;                       //物品数量
    public string name = "";               //物品名字
    public string description = "";         //物品描述
    public Bitmap bitmap;                   //物品图片
    public int isdepletion = 1;                //是否为可消耗类       
    public int value1=0;                              //作为装备
    public int value2 = 0;                           //value1 装备类型1-att 2-def                       //作为药品时                            
    public int value3 = 0;                           //value2-5 攻击 防御 速度 运气                 
    public int value4 = 0;
    public int value5 = 0;
    public int cost = 100;

    //战斗使用
    public int canfuse = 0;                     //能否在战斗中使用
    public int fvalue1 = 0;                     //攻击值参数
    public int fvalue2 = 0;
    public Animation fanm;

    public void set(string name, string description, string bitmap_path, int isdepletion, int value1, int value2, int value3, int value4, int value5)
    {
        this.name = name;
        this.description = description;

        if(bitmap_path!=null&&bitmap_path!="")
        {
            bitmap=new Bitmap(bitmap_path);
            bitmap.SetResolution(96,96);
        }
        this.isdepletion=isdepletion;
        this.value1=value1;
        this.value2=value2;
        this.value3=value3;
        this.value4=value4;
        this.value5=value5;
    }
    public delegate void Use_event(Item item);
    public event Use_event use_event;
    public void use()
    {
        if (num < 0)                       //是否有这物品
            return;
        if (isdepletion != 0)                    //是否为消耗品
            num--;
        if (use_event != null)                   //   使用事件
            use_event(this);
    }

    //物品增加减少方法
    public static void add_item(int index, int num)
    {
        if (item == null) return;                 //异常处理
        if (index < 0) return;
        if (index >= item.Length) return;
        if (item[index] == null) return;

        item[index].num += num;              //数量增减

        if (item[index].num < 0)                //异常数值处理
            item[index].num = 0;
    }

    //添加hp
    public static void add_hp(Item item)
    {
        Player player = Form1.player[Player.select_player];
        player.hp += item.value1;
        if (player.hp > player.max_hp)
            player.hp = player.max_hp;
        if (player.hp < 0)
            player.hp = 0;
    }
    //添加mp
    public static void add_mp(Item item)
    {
        Player player=Form1.player[Player.select_player];
        player.mp += item.value1;
        if (player.mp > player.max_mp)
            player.mp = player.max_mp;
    }

    //卸下装备的方法
    public static void unequip(int type)
    {
        //获取index
        int index;
        if (type == 1)
        {
            index = Form1.player[Player.select_player].equip_att;       //记录武器类装备
            Form1.player[Player.select_player].equip_att = -1;               //卸下装备
        }
        else if (type == 2)
        {
            index = Form1.player[Player.select_player].equip_def;        //记录防具类
            Form1.player[Player.select_player].equip_def=-1;
        }
        else 
            return;

        if (item == null) return;                         //一些异常
        if (index < 0) return;
        if (index >= item.Length) return;
        if (item[index] == null) return;

        add_item(index,1);                            //添加物品

    }
    
    //value1 装备类型 1-att  2-def
    //value2-5 攻击 防御 速度 运气的增减值
    public static void equip(Item item)
    {
        if (Item.item == null) return;
        if (item == null) return;
        int index = -1;
        for (int i = 0; i < Item.item.Length; i++)
        {
            if (Item.item[i] == null)
                continue;
            if (item.name == Item.item[i].name && item.description == Item.item[i].description)
            {
                index = i;
                break;
            }
        }
        if (index < 0) return;
        if (index >= Item.item.Length) return;
        if (Item.item[index] == null) return;

        unequip(item.value1);                     //卸下装备

        if (item.value1 == 1)                            //穿戴新装备
            Form1.player[Player.select_player].equip_att = index;
        else if (item.value1 == 2)
            Form1.player[Player.select_player].equip_def = index;
        else
            return;

    }

    public static void lpybook(Item item)
    {
        Message.show("","得此书者，称霸武林","",Message.Face.LEFT);
        Task.block();
        Message.show("","只待有缘人","",Message.Face.LEFT);
        Task.block();
    }

    //定义物品战斗属性
    public void fset(Animation fanm, int fvalue1, int fvalue2)
    {
        this.fanm = fanm;
        this.fvalue1 = fvalue1;
        this.fvalue2 = fvalue2;
        this.canfuse = 1;
        if (fanm != null)
            fanm.load();

    }
    //判断玩家是否拥有
    public bool check_fuse()
    {
        if (num <= 0)
            return false;
        if (canfuse != 1)                //能否在战斗中使用
            return false;
        if (isdepletion != 0)            //消耗则扣除
            num--;
        return true;

    }



}