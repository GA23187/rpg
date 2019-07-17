public class Story
{
    //陌生人
    public static int meet(int task_id, int step)
    {
        Task.talk("陌生人","少年，我看你骨骼惊奇，天资聪慧，今有鞋精作怪，你可愿意帮忙？","role/face4_2.png",Message.Face.RIGHT);
        Task.talk("主角","鞋精？","role/face2_1.png");
        Task.talk("陌生人","没错，自从这鞋精有了法力后，村里不得安宁，希望你可以出手相助！","role/face4_2.png",Message.Face.RIGHT);
        Task.talk("主角","好吧！","role/face2_1.png");
        Task.talk("陌生人", "这是我家祖传的短剑，也许可以帮助下你。", "role/face4_2.png", Message.Face.RIGHT);
        Task.tip("获得短剑X3");
        Task.add_item(2,3);
        return 0;
    }
    public static int aftermeet(int task_id, int step)
    {
        Task.talk("陌生人","村子就靠你了","role/face4_2.png",Message.Face.RIGHT);
        return 0;
    }
    public static int reward(int task_id, int step)
    {
        Task.talk("陌生人", "虽然打败鞋精，但是我怕他会回来报仇，将来可能会更难对付。我这有本祖传秘籍，记载了些法术，我自己看不懂，倒不如送给你。若鞋精再来，还可以助你", "role/face4_2.png", Message.Face.RIGHT);
        Task.tip("获得《九阳真经》");
        Task.add_item(5,1);
        return 0;
    }
    public static int afterreward(int task_id, int step)
    {
        Task.talk("陌生人","若鞋精再来，望少侠出手。","role/face4_2.png",Message.Face.RIGHT);
        return 0;
    }
    public static int shoe(int task_id, int step)
    {
        Task.tip("一双破鞋");
        return 0;
    }
    public static int shoefight(int task_id, int step)
    {
        if (step == 0)
        {
            Task.talk("鞋子怪", "(生气)mmp", "");
            Player.status = Player.Status.WALK;
            Task.fight(new int[] { -1, 1, -1 }, "fight/f_scene.png", 0, 0, 1, -1, 10);
            Task.block();
            return 1;
        }
        else
        {
            if (Fight.iswin == 1)
            {
                Task.tip("捡起破鞋");
                Task.set_npc_pos(6, -1000, -1000);
                Task.p[0] = 2;
                return 0;
            }
            else
            {
                return 0;
            }
        }
    }

    public static int save(int task_id, int step)
    {
        Task.talk("女孩","我可以保存游戏哦！","");
        Save.show(0);
        Task.block();
        return 0;
    }
    public static int shop(int task_id, int step)
    {
        Shop.show(new int[] { 0, 1, 2, 3, 4, -1, -1 });  //商人所卖物品
        Task.block();
        return 0;
    }
    public static int play_anm(int task_id, int step)
    {
        Task.play_npc_anm(4,0);
        return 0;
    }
    public static int tip(int task_id, int step)
    {
        Task.tip("我会动！");
        return 0;
    }
}